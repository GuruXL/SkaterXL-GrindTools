using UnityEngine;
using MapEditor;
using GameManagement;
using ModIO.UI;
using Rewired;
using Dreamteck.Splines;
using System;
using System.Collections.Generic;
using GrindTools.Patches;
using GrindTools.Data;
using GrindTools.Utils;
using System.Collections;
using System.Threading.Tasks;
using HarmonyLib;
using SkaterXL.Map;

namespace GrindTools
{
    public class InputController : MonoBehaviour
    {
        public Player player { get; private set; }
        //Transform placedParent = MapEditorController.Instance.placedObjectsParent;

        public void Awake()
        {
            player = PlayerController.Instance.inputController.player;
            if (Main.controller.grindToolCam != null) Main.controller.grindToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
            if (Main.controller.waxToolCam != null) Main.controller.waxToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
        }
        public void Update()
        {
            if (GameStateMachine.Instance.CurrentState == null || GameStateMachine.Instance.CurrentState.IsGameplay())
                return;

            var currentState = MapEditorController.Instance.CurrentState.GetType();
            if (currentState != null && currentState == typeof(GrindSplineToolState))
            {
                if (player.GetButton("LB")) // LB pressed
                {
                    CheckActiveSplineDeleteInput();
                }
                else if (player.GetButton(7)) // RB pressed
                {
                    UpdateFOV();
                }
                else
                {
                    ToolStateInput();
                }
            }
            else if (currentState != null && currentState == typeof(WaxToolState))
            {
                if (player.GetButton("LB")) // LB pressed
                {
                    CheckDeleteInput(Main.controller.waxToolState, WaxToolStatePatch.GetHighlightedObj(), WaxToolStatePatch.GetRayHitInfo());
                }
                else if (player.GetButton(7)) // RB pressed
                {
                    UpdateFOV();
                }
                else if (player.GetButtonDown(0)) // A button
                {
                    SwapGrindTags(Main.controller.waxToolState, WaxToolStatePatch.GetSplineComp());
                }
                else
                {
                    ToolStateInput();
                }
            }
            else if (currentState != null && currentState == typeof(SimpleMode))
            {
                CheckForInput();
            }
        }

        private async void CheckForInput()
        {
            if (player.GetButtonUp(13))
            {
                await StateManager.Instance.RequestGrindTool();
            }
            else if (player.GetButtonDown("B") || player.GetButtonDown("Start"))
            {
                StateManager.Instance.ResetToPlayState();
            }
        }
        private async void ToolStateInput()
        {
            switch (MapEditorController.Instance.CurrentState)
            {
                case GrindSplineToolState grindToolState:
                    if (player.GetButtonDown("Y"))
                    {
                        await StateManager.Instance.ToggleState(ToolStates.Wax);
                        UISounds.Instance.PlayOneShotSelectMinor();
                    }
                    else if (player.GetButtonDown("B") || player.GetButtonDown("Start"))
                    {
                        StateManager.Instance.ResetToPlayState();
                    }
                    break;
                case WaxToolState waxToolState:
                    if (player.GetButtonDown("Y"))
                    {
                        await StateManager.Instance.ToggleState(ToolStates.Grind);
                        UISounds.Instance.PlayOneShotSelectMinor();
                    }
                    else if (player.GetButtonDown("B") || player.GetButtonDown("Start"))
                    {
                        StateManager.Instance.ResetToPlayState();
                    }
                    break;
            }
        }

        private float changeSpeed = 20.0f;
        private void UpdateFOV()
        {
            Vector2 rightstick = Main.inputctrl.player.GetAxis2D("RightStickX", "RightStickY");
            Main.settings.CamFOV -= rightstick.y * changeSpeed * Time.unscaledDeltaTime;
            Main.settings.CamFOV = Mathf.Clamp(Main.settings.CamFOV, 40.0f, 120.0f);
            //Main.settings.CamFOV = Mathf.Max(0f, Main.settings.CamFOV + rightstick.y * Time.unscaledDeltaTime * changeSpeed);

            if (Main.settings.CamFOV == Main.controller.grindToolCam.m_Lens.FieldOfView)
                return;
            Main.controller.grindToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
            Main.controller.waxToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
        }

        private void CheckDeleteInput(WaxToolState __instance, IMapEditorSelectable HightlightedObj, RaycastHit hitInfo)
        {
            if (hitInfo.collider.GetComponentInParent<IMapEditorSelectable>() == null)
            {
                ShowWaxInfo(__instance, "Cannot Delete Map Splines");
                return;
            }
            ShowWaxInfo(__instance, "Warning: Spline Deletion is Permanent");

            if (player.GetButtonUp(13))
            {
                Destroy(HightlightedObj.gameObject);
                ShowWaxInfo(__instance, "Spline Deleted");
                UISounds.Instance.PlayOneShotSelectionChange();
                MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, $"Spline Deleted", 0.5f);
            }
        }

        private void CheckActiveSplineDeleteInput()
        {
            if (player.GetButtonUp(13))
            {
                if (CheckRaycastsPatch.GetSelectedSpline() != null)
                {
                    CheckRaycastsPatch.SetSelectedSplineNull();

                    if (CheckRaycastsPatch.GetSelectedSpline() == null)
                    {
                        UISounds.Instance.PlayOneShotSelectionChange();
                        MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, $"Active Spline Removed", 1f);
                    }
                    else
                    {
                        MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Failed To Remove Active Spline", 1f);
                    }
                }
                else
                {
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"No Active Spline", 1f);
                }
            }
        }
        public void SwapGrindTags(WaxToolState __instance, SplineComputer spline)
        {
            string concrete = "Grind_Concrete";
            string metal = "Grind_Metal";

            if (spline.gameObject.tag == concrete)
            {
                SetTagRecursively(spline.gameObject, metal);
                ShowWaxInfo(__instance, "Metal");
                return;
            }
            else if (spline.gameObject.tag == metal)
            {
                SetTagRecursively(spline.gameObject, concrete);
                ShowWaxInfo(__instance, "Concrete");
                return;
            }
            else // if tag is unknown or undefined the default is concrete so swap to metal
            {
                SetTagRecursively(spline.gameObject, metal);
                ShowWaxInfo(__instance, "Metal");
            }
        }
        public void SetTagRecursively(GameObject obj, string tag)
        {
            obj.tag = tag;

            if (obj.transform.childCount > 0)
            {
                foreach (Transform child in obj.transform)
                {
                    SetTagRecursively(child.gameObject, tag);
                }
            }
        }
        public void ShowWaxInfo(WaxToolState __instance, string text)
        {
            AccessTools.Method(typeof(WaxToolState), "ShowInfo").Invoke(__instance, new object[] { text });
        }
    }
}