using System.Reflection;
using UnityEngine;
using System.IO;
using GameManagement;
using System.Collections;
using System;
using ModIO.UI;
using System.Threading.Tasks;
using MapEditor;
using GrindTools.Data;
using GrindTools.Patches;

namespace GrindTools.Utils
{
    public class StateManager
    {
        public static StateManager __instance { get; private set; }
        public static StateManager Instance => __instance ?? (__instance = new StateManager());

        public async Task ToggleState(ToolStates toolState)
        {
            try
            {
                MapEditorState targetState = null;
                string name = "";

                switch (toolState)
                {
                    case ToolStates.Grind:
                        targetState = Main.controller.grindToolState;
                        name = "Grind Tool";
                        break;
                    case ToolStates.Wax:
                        targetState = Main.controller.waxToolState;
                        name = "Wax Tool";
                        break;
                }

                await MapEditorController.Instance.ChangeState(targetState);

                if (MapEditorController.Instance.CurrentState == targetState)
                {
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $"{name} Active", 1f);
                }
                else
                {
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"{name} State Transition Failed", 1f);  
                }
            }
            catch (Exception ex)
            {
                Main.Logger.Error($"Error toggling to state: {ex}");
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Error toggling tool: {ex.Message}", 1f);
            }
        }
        public async Task RequestGrindTool()
        {
            try
            {
                if (MapEditorController.Instance.initialState == null)
                {
                    //MapEditorController.Instance.initialState = Main.controller.grindToolState;
                    MapEditorController.Instance.initialState = MapEditorController.Instance.SimplePlacerState;
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Initial state is null", 1f);
                }

                await MapEditorController.Instance.ChangeState(Main.controller.grindToolState);
                GameStateMachine.Instance.StopLoading();

                Main.controller.AllowRespawn(false);
                Main.controller.ToggleSpeedText(true);

                if (MapEditorController.Instance.CurrentState?.GetType() == typeof(GrindSplineToolState))
                {
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $"Grind Tool Active", 1f);
                }
                else
                {
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Grind Tool State Transition Failed", 1f);
                }
            }
            catch (Exception ex)
            {
                Main.Logger.Error($"An error occurred while requesting Grind Tool: {ex.Message}");
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Grind Tool Error: {ex.Message}", 1f);
            }
        }
        public async Task ResetToPlayState()
        {
            try
            {
                Main.controller.AllowRespawn(true);
                MapEditorController.Instance.ExitMapEditor();
                await GameStateMachine.Instance.RequestTransitionBackToPlayState();
                GameStateMachine.Instance.StopLoading();
                Main.controller.ToggleSpeedText(false);
                //Main.controller.DeleteSelectedSpline(); // delete if < 2 nodes
                CheckRaycastsPatch.SetSelectedSplineNull();
            }
            catch (Exception ex)
            {
                Main.Logger.Error($"An error occurred while Reseting to PlayState: {ex.Message}");
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Reset To PlayState Error: {ex.Message}", 1f);
            }
        }
    }
}
