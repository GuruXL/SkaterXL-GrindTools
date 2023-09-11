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
        public Player player { get; private set; }

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
            MapEditorController.Instance.ExitMapEditor();
            GameStateMachine.Instance.RequestTransitionBackToPlayState();
            Main.controller.ToggleSpeedText(false);
            if (CheckRaycastsPatch.selectedSpline != null)
            {
                Destroy(CheckRaycastsPatch.selectedSpline.gameObject);
            }
        }
        public async void RequestGrindTool()
        {
            if (MapEditorController.Instance.initialState != MapEditorController.Instance.SimplePlacerState)
            {
                MapEditorController.Instance.initialState = MapEditorController.Instance.SimplePlacerState;
                await MapEditorController.Instance.ChangeState<SimpleMode>();
            }
            GameStateMachine.Instance.RequestTransitionTo(typeof(MapEditorGameState));
            Main.controller.ToggleState("Grind");
            Main.controller.AllowRespawn(false);
            Main.controller.ToggleSpeedText(true);
        }
    }
}
