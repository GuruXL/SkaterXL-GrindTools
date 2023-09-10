using UnityEngine;
using MapEditor;
using GameManagement;
using ModIO.UI;
using Rewired;
using System;
using System.Collections.Generic;
using GrindTools.Patches;

namespace GrindTools
{
    public class InputController : MonoBehaviour
    {
        private Player player;
        private float delay = 0.2f;
        private float lastDelay = 0f;

        public void Awake()
        {
            player = PlayerController.Instance.inputController.player;
            Main.controller.grindToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
            Main.controller.waxToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
        }

        public void Update()
        {
            // Check if enough time has passed since the last input check
            if (Time.time - lastDelay >= delay)
            {
                var currentState = Main.controller.editorController.CurrentState;
                Type currentType = currentState?.GetType();

                if (currentType == typeof(GrindSplineToolState) || currentType == typeof(WaxToolState))
                {
                    bool rbPRessed = player.GetButton(7);
                    if (rbPRessed)
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
            switch (Main.controller.editorController.CurrentState)
            {
                case GrindSplineToolState grindToolState:
                    if (player.GetButtonDown("Y"))
                    {
                        //Main.controller.grindToolState.Exit(Main.controller.grindToolState);
                        Main.controller.ToggleState("Wax");

                        if (CheckRaycastsPatch.selectedSpline != null)
                        {
                            Destroy(CheckRaycastsPatch.selectedSpline.gameObject);

                        }
                    }
                    else if (player.GetButtonDown("B"))
                    {
                        ResetToPlayState();
                    }
                    else if (CheckRaycastsPatch.selectedSpline != null)
                    {
                        if (CheckRaycastsPatch.selectedSpline.nodes.Count >= 2 && player.GetButtonDown("X"))
                        {
                            MessageSystem.QueueMessage(MessageDisplayData.Type.Success, $"New Spline Created", 2.5f);
                        }
                    }
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
        public float changeSpeed = 20.0f;
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
            Main.controller.editorController.ExitMapEditor();
            GameStateMachine.Instance.RequestTransitionBackToPlayState();
            Main.controller.ToggleSpeedText(false);
            if (CheckRaycastsPatch.selectedSpline != null)
            {
                Destroy(CheckRaycastsPatch.selectedSpline.gameObject);
            }
        }
        public void RequestGrindTool()
        {
            //GameStateMachine.Instance.RequestTransitionTo(typeof(MapEditorGameState), false, null);
            //Main.controller.editorController.ChangeState(Main.controller.editorController.SimplePlacerState);
            Main.controller.ToggleState("Grind");
            Main.controller.AllowRespawn(false);
            Main.controller.ToggleSpeedText(true);
        }
    }
}
