using GameManagement;
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
                    Main.Logger.Log($"{name} State Transition Failed");
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"{name} State Transition Failed", 1f);
                }
                else
                {
                    Main.Logger.Log($"{name} Active");
                    //MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $"{name} Active", 1f);
                }
            }
            catch (Exception ex)
            {
                Main.Logger.Error($"Error Switching Modes: {ex}");
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Error Switching Modes: {ex.Message}", 1f);
            }
        }
        public async Task LoadMapEditorState(MapEditorState state)
        {
            try
            {
                PlayerController.Main.DisableGameplay();
                PlayerController.Main.HidePin(true);
                GameStateMachine.Instance.StartLoading(false, null, $"Loading {state.name}");
                GameStateMachine.Instance.MapEditorObject.SetActive(true);
                await MapEditorController.Instance.ChangeState(state);
                GameStateMachine.Instance.StopLoading();
                Main.controller.AllowRespawn(false);
                //Main.controller.ToggleSpeedText(true);
                ToggleMenuUI(false);
            }
            catch (Exception ex)
            {
                Main.Logger.Error($"Error Loading {state.name}: {ex.Message}");
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Error Loading {state.name}: {ex.Message}", 1f);
            }
        }
        private void ToggleMenuUI(bool state)
        {
            GameStateMachine.Instance.PauseObject.SetActive(state);
            GameStateMachine.Instance.SemiTransparentLayer.SetActive(state);
        }
        public async Task InitializeMapEditor(MapEditorState initialState)
        {
            try
            {
                MapEditorController.Instance.initialState = initialState;
                GameStateMachine.Instance.StartLoading(false, null, "Loading Map Editor");
                GameStateMachine.Instance.MapEditorObject.SetActive(true);
                //GameStateMachine.Instance.RequestMapEditorState();
                //GameStateMachine.Instance.RequestTransitionTo(typeof(MapEditorGameState)); // disable this line if transtions not function properly.
                await MapEditorController.Instance.ChangeState(MapEditorController.Instance.SimplePlacerState);
                GameStateMachine.Instance.StopLoading();
                ToggleMenuUI(false);
            }
            catch (Exception ex)
            {
                Main.Logger.Error($"Error Loading Map Editor: {ex.Message}");
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Error Loading Map Editor: {ex.Message}", 1f);
            }
        }

        public async Task RequestMEState(MapEditorState state)
        {
            try
            {
                if (MapEditorController.Instance.initialState == null)
                {
                    await InitializeMapEditor(state);
                    await LoadMapEditorState(state);
                }
                else
                {
                    await LoadMapEditorState(state);
                }

                MapEditorState currentstate = MapEditorController.Instance.CurrentState;
                if (currentstate != null && currentstate == state)
                {
                    //MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $"{state.name} Active", 0.5f);
                    Main.Logger.Log($"{state.name} Active");
                }
                else
                {
                    //MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"{state.name} Transition Failed", 1f);
                    Main.Logger.Log($"{state.name} Transition Failed");
                }
            }
            catch (Exception ex)
            {
                Main.Logger.Error($"error while requesting{state.name}: {ex.Message}");
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"{state.name} Error: {ex.Message}", 1f);
            }
        }
        public void ResetToPlayState()
        {
            try
            {
                Main.controller.AllowRespawn(true);
                PlayerController.Main.HidePin(false);
                GameStateMachine.Instance.MapEditorObject.SetActive(false);
                PlayerController.Main.EnableGameplay();
                //Main.controller.ToggleSpeedText(false);
                GameStateMachine.Instance.RequestPlayState();
            }
            catch (Exception ex)
            {
                Main.Logger.Error($"Error Reseting to PlayState: {ex.Message}");
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Error Reseting to PlayState: {ex.Message}", 1f);
            }
        }
    }
}