using UnityEngine;
using MapEditor;
using RapidGUI;
using UnityEngine.SceneManagement;
using GameManagement;
using UnityEngine.UI;
using ModIO.UI;

namespace GrindTools
{
    public class InputController : MonoBehaviour
    {
        public static InputController Instance { get; private set; }

        public void Update()
        {
            if (GameStateMachine.Instance.CurrentState.GetType() == typeof(MapEditorGameState) ||
                GameStateMachine.Instance.CurrentState.GetType() == typeof(GrindSplineToolState) ||
                GameStateMachine.Instance.CurrentState.GetType() == typeof(WaxToolState))
            {
                Main.Controller.AllowRespawn(2);
                ToggleActiveState();
            }
            else
            {
                Main.Controller.AllowRespawn(1);
                ToggleEditorState();
            }
        }

        void ToggleEditorState()
        {
            if (GameStateMachine.Instance.CurrentState.GetType() != typeof(MapEditorGameState) ||
                GameStateMachine.Instance.CurrentState.GetType() != typeof(GrindSplineToolState) ||
                GameStateMachine.Instance.CurrentState.GetType() != typeof(WaxToolState))
            {
                if (PlayerController.Instance.inputController.player.GetButtonLongPressDown("Y"))
                {
                    Active();
                }
            }

            if (GameStateMachine.Instance.CurrentState.GetType() == typeof(MapEditorGameState) ||
                GameStateMachine.Instance.CurrentState.GetType() == typeof(GrindSplineToolState) ||
                GameStateMachine.Instance.CurrentState.GetType() == typeof(WaxToolState))
            {
                if (PlayerController.Instance.inputController.player.GetButtonDown("B"))
                {
                    Deactive();
                }
            }
        }

        void ToggleActiveState()
        {
            if (PlayerController.Instance.inputController.player.GetButtonLongPressDown(13))
            {
                Main.Controller.ToggleTool("Grind");
                MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $" Grind Tool Active", 2f);
            }

            if (Main.Controller.EditorController.CurrentState.GetType() == typeof(GrindSplineToolState))
            {
                if (PlayerController.Instance.inputController.player.GetButtonDown("Y"))
                {
                    Main.Controller.ToggleTool("Wax");
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $" Wax Tool Active", 2f);
                }

                if (PlayerController.Instance.inputController.player.GetButtonShortPressDown(70)) // Undo
                {
                    Main.Controller.GrindToolState.mapEditor.Undo();
                }

                if (PlayerController.Instance.inputController.player.GetButtonShortPressDown(69)) // Redo
                {
                    Main.Controller.GrindToolState.mapEditor.Redo();
                }
            }
            else if (Main.Controller.EditorController.CurrentState.GetType() == typeof(WaxToolState))
            {
                if (PlayerController.Instance.inputController.player.GetButtonDown("Y"))
                {
                    Main.Controller.ToggleTool("Grind");
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $" Grind Tool Active", 2f);
                }
            }
        }
        private void Active()
        {
            if (!Main.Controller.IsPlayState())
            {
                GameStateMachine.Instance.RequestPlayState();
            }
            Main.Controller.ToggleMapEditor(1);
        }

        private void Deactive()
        {
            Main.Controller.ToggleMapEditor(2);
            Main.Controller.RestStates();
        }
    }
}
