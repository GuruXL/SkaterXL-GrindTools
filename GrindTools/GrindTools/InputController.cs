using UnityEngine;
using MapEditor;
using GameManagement;
using ModIO.UI;

namespace GrindTools
{
    public class InputController : MonoBehaviour
    {

        public void Update()
        {
            if (GameStateMachine.Instance.CurrentState.GetType() == typeof(MapEditorGameState) ||
                GameStateMachine.Instance.CurrentState.GetType() == typeof(GrindSplineToolState) ||
                GameStateMachine.Instance.CurrentState.GetType() == typeof(WaxToolState))
            {
                Main.controller.AllowRespawn(false);
                ToggleActiveState();
            }
            else
            {
                Main.controller.AllowRespawn(true);
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
                //Main.Controller.ToggleTool("Grind");
                MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $" Grind Tool Active", 2f);
            }

            if (Main.controller.EditorController.CurrentState.GetType() == typeof(GrindSplineToolState))
            {
                if (PlayerController.Instance.inputController.player.GetButtonDown("Y"))
                {
                    //Main.Controller.ToggleTool("Wax");
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $" Wax Tool Active", 2f);
                }

                if (PlayerController.Instance.inputController.player.GetButtonShortPressDown(70)) // Undo
                {
                    Main.controller.GrindToolState.mapEditor.Undo();
                }

                if (PlayerController.Instance.inputController.player.GetButtonShortPressDown(69)) // Redo
                {
                    Main.controller.GrindToolState.mapEditor.Redo();
                }
            }
            else if (Main.controller.EditorController.CurrentState.GetType() == typeof(WaxToolState))
            {
                if (PlayerController.Instance.inputController.player.GetButtonDown("Y"))
                {
                    //Main.Controller.ToggleTool("Grind");
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $" Grind Tool Active", 2f);
                }
            }
        }
        private void Active()
        {
            if (!Main.controller.IsPlayState())
            {
                GameStateMachine.Instance.RequestPlayState();
            }
            //Main.Controller.ToggleMapEditor(1);
        }

        private void Deactive()
        {
            //Main.Controller.ToggleMapEditor(2);
            //Main.Controller.RestStates();
        }
    }
}
