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

namespace GrindTools
{
    public class InputController : MonoBehaviour
    {
        public Player player { get; private set; }
        Transform placedParent = MapEditorController.Instance.placedObjectsParent;
       
        public void Awake()
        {
            player = PlayerController.Main.input;
            if(Main.controller.grindToolCam != null ) Main.controller.grindToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
            if (Main.controller.waxToolCam != null) Main.controller.waxToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
        }
        public void Update()
        {
            var currentState = MapEditorController.Instance.CurrentState?.GetType();
            if (currentState == typeof(GrindSplineToolState) || currentState == typeof(WaxToolState))
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
            else if (!GameStateMachine.Instance.CurrentState.IsGameplay() && currentState == typeof(SimpleMode))
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
            else if (player.GetButtonDown("B"))
            {
                StateManager.Instance.ResetToPlayState();
            }
        }    
        private async void SwapToolStates()
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
    }
}
