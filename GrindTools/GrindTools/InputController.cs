using UnityEngine;
using MapEditor;
using GameManagement;
using ModIO.UI;
using Rewired;
using System;
using System.Collections.Generic;

namespace GrindTools
{
    public class InputController : MonoBehaviour
    {
        Player player;

        public void Awake()
        {
            player = PlayerController.Instance.inputController.player;
        }

        public void Update()
        {
            if (Main.controller.EditorController.CurrentState.GetType() == typeof(GrindSplineToolState) ||
                Main.controller.EditorController.CurrentState.GetType() == typeof(WaxToolState))
            {
                SwapToolStates();
            }
            else
            {
                CheckForInput();
            }
        }

        private void CheckForInput()
        {
            if (GameStateMachine.Instance.CurrentState.GetType() != typeof(MapEditorGameState))
                return;

            if (player.GetButtonUp(13))
            {
                Main.controller.ToggleState("Grind");
                Main.controller.AllowRespawn(false);
                Main.uiManager.ToggleSpeedText(true);
                Main.uiManager.ToggleIndicators(true);
                MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $" Grind Tool Active", 2f);
            }
            else if (player.GetButtonDown("B"))
            {
                ResetToPlayState();
            }
        }    
        private void SwapToolStates()
        {
            switch (Main.controller.EditorController.CurrentState)
            {
                case GrindSplineToolState grindToolState:
                    if (player.GetButtonDown("Y"))
                    {
                        Main.controller.ToggleState("Wax");
                        Main.uiManager.ToggleIndicators(false);
                        MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $" Wax Tool Active", 2f);
                    }
                    else if (player.GetButtonDown("B"))
                    {
                        ResetToPlayState();
                    }
                    break;
                case WaxToolState waxToolState:
                    if (player.GetButtonDown("Y"))
                    {
                        Main.controller.ToggleState("Grind");
                        Main.uiManager.ToggleIndicators(true);
                        MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $" Grind Tool Active", 2f);
                    }
                    else if (player.GetButtonDown("B"))
                    {
                        ResetToPlayState();
                    }
                    break;
            }
        }

        void ResetToPlayState()
        {
            Main.controller.AllowRespawn(true);
            GameStateMachine.Instance.RequestPlayState();
            Main.uiManager.ToggleSpeedText(false);
            Main.uiManager.ToggleIndicators(false);
        }
    }
}
