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

                if (MapEditorController.Instance.CurrentState != targetState)
                {
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"{name} State Transition Failed", 1f);
                }
                else
                {
                    //MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $"{name} Active", 1f);
                }
            }
            catch (Exception ex)
            {
                Main.Logger.Error($"Error Switching Modes: {ex}");
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Error Switching Modes: {ex.Message}", 1f);
            }
        }

        private async Task LoadGrindToolState()
        {
            GameStateMachine.Instance.StartLoading(true, null, "Loading");
            await MapEditorController.Instance.ChangeState(Main.controller.grindToolState);
            GameStateMachine.Instance.StopLoading();
            Main.controller.AllowRespawn(false);
            Main.controller.ToggleSpeedText(true);
        }
        private async Task InitializeMapEditor()
        {
            MapEditorController.Instance.initialState = Main.controller.grindToolState;
            GameStateMachine.Instance.MapEditorObject.SetActive(true);
            GameStateMachine.Instance.RequestMapEditorState();
            await MapEditorController.Instance.ChangeState(MapEditorController.Instance.SimplePlacerState);
        }
        public async Task RequestGrindTool()
        {
            try
            {
                if (MapEditorController.Instance.initialState == null)
                {
                    await InitializeMapEditor();
                    await LoadGrindToolState();
                }
                else
                {
                    await LoadGrindToolState();
                }

                MapEditorState currentstate = MapEditorController.Instance.CurrentState;
                if (currentstate != null && currentstate is GrindSplineToolState)
                {
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $"Grind Tool Active", 0.5f);
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
        public void ResetToPlayState()
        {
            try
            {
                Main.controller.AllowRespawn(true);
                GameStateMachine.Instance.RequestPlayState();
                Main.controller.ToggleSpeedText(false);
            }
            catch (Exception ex)
            {
                Main.Logger.Error($"An error occurred while Reseting to PlayState: {ex.Message}");
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Reset To PlayState Error: {ex.Message}", 1f);
            }
        }
    }
}
