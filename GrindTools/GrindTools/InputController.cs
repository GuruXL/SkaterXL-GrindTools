using UnityEngine;
using MapEditor;
using GameManagement;
using ModIO.UI;
using Rewired;
using Dreamteck.Splines;
using System;
using System.Collections.Generic;
using GrindTools.Patches;
using System.Collections;

namespace GrindTools
{
    public class InputController : MonoBehaviour
    {
        public Player player { get; private set; }
        Transform placedParent = MapEditorController.Instance.placedObjectsParent;

        public void Awake()
        {
            player = PlayerController.Instance.inputController.player;
            if(Main.controller.grindToolCam != null ) Main.controller.grindToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
            if (Main.controller.waxToolCam != null) Main.controller.waxToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
        }

        public void Update()
        {
            var currentState = MapEditorController.Instance.CurrentState;
            if (currentState == Main.controller.grindToolState || currentState == Main.controller.waxToolState)
            {
                bool RBPressed = player.GetButton(7);
                if (RBPressed)
                {
                    UpdateFOV();
                }
                else
                {
                    SwapToolStates();
                }
            }
            else if (GameStateMachine.Instance.CurrentState.GetType() == typeof(MapEditorGameState))
            {
                CheckForInput();
            }
        }
        private void CheckForInput()
        {
            if (player.GetButtonUp(13))
            {
                RequestGrindTool();
            }
            else if (player.GetButtonDown("B"))
            {
                ResetToPlayState();
            }
        }    
        private void SwapToolStates()
        {
            switch (MapEditorController.Instance.CurrentState)
            {
                case GrindSplineToolState grindToolState:
                    if (player.GetButtonDown("Y"))
                    {
                        //Main.controller.grindToolState.Exit(Main.controller.grindToolState);
                        Main.controller.ToggleState("Wax");
                        DeleteSelectedSpline(1); // delete if <=1 node
                       
                    }
                    else if (player.GetButtonDown("B"))
                    {
                        ResetToPlayState();
                    }
                    /*
                    else if (CheckRaycastsPatch.GetSelectedSpline() != null)
                    {
                        if (CheckRaycastsPatch.GetSelectedSpline().nodes.Count >= 2 && player.GetButtonDown("X"))
                        {
                            MessageSystem.QueueMessage(MessageDisplayData.Type.Success, $"New Spline Created", 2.5f);
                        }
                    }
                    */
                    break;
                case WaxToolState waxToolState:
                    if (player.GetButtonDown("Y"))
                    {
                        //Main.controller.waxToolState.Exit(Main.controller.waxToolState);
                        Main.controller.ToggleState("Grind");
                    }
                    else if (player.GetButtonDown("B"))
                    {
                        ResetToPlayState();
                    }
                    break;
            }
        }
        private float changeSpeed = 20.0f;
        private void UpdateFOV()
        {
            Vector2 rightstick = player.GetAxis2D("RightStickX", "RightStickY");
            Main.settings.CamFOV -= rightstick.y * changeSpeed * Time.unscaledDeltaTime;
            Main.settings.CamFOV = Mathf.Clamp(Main.settings.CamFOV, 40.0f, 120.0f);
            //Main.settings.CamFOV = Mathf.Max(0f, Main.settings.CamFOV + rightstick.y * Time.unscaledDeltaTime * changeSpeed);

            if (Main.settings.CamFOV == Main.controller.grindToolCam.m_Lens.FieldOfView)
                return;
            Main.controller.grindToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
            Main.controller.waxToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
        }
        private void ResetToPlayState()
        {
            Main.controller.AllowRespawn(true);
            MapEditorController.Instance.ExitMapEditor();
            GameStateMachine.Instance.RequestTransitionBackToPlayState();
            Main.controller.ToggleSpeedText(false);
            DeleteSelectedSpline(2); // delete if <=2 nodes
        }
        public void RequestGrindTool()
        {
            //GameStateMachine.Instance.RequestMapEditorState();
            MapEditorController.Instance.ChangeState(Main.controller.grindToolState);
            MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $"Grind Tool Active", 1f);
            Main.controller.AllowRespawn(false);
            Main.controller.ToggleSpeedText(true);
        }
        private void DeleteSelectedSpline(int nodeCount)
        {
            if (CheckRaycastsPatch.GetSelectedSpline() != null && CheckRaycastsPatch.GetSelectedSpline().nodes.Count <= nodeCount)
            {
                Destroy(CheckRaycastsPatch.GetSelectedSpline().gameObject);
            }
        }
    }
}
