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
                GameStateMachine.Instance.CurrentState.GetType() != typeof(GrindSplineToolState)||
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
                //PlayerController.Instances[0].gameplay.inputController.rewiredPlayer.GetButtonDown("B")
                if (PlayerController.Instance.inputController.player.GetButtonLongPressDown("B"))
                {
                    Deactive();
                }
            }
        }

        void ToggleActiveState()
        {
            //PlayerController.Instances[0].gameplay.inputController.rewiredPlayer.GetButtonLongPressDown(13)
            if (PlayerController.Instance.inputController.player.GetButtonShortPressDown("Y"))
            {
                Main.Controller.ToggleTool("Grind");
                MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $" Grind Tool Active", 2f);
            }

            if (Main.Controller.EditorController.CurrentState.GetType() == typeof(GrindSplineToolState))
            {
                //PlayerController.Instances[0].gameplay.inputController.rewiredPlayer.GetButtonDown("Y")
                if (PlayerController.Instance.inputController.player.GetButtonShortPressDown("Y"))
                {
                    Main.Controller.ToggleTool("Wax");
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $" Wax Tool Active", 2f);
                }

                //PlayerController.Instances[0].gameplay.inputController.rewiredPlayer.GetButtonShortPressDown(70)
                if (PlayerController.Instance.inputController.player.GetButtonShortPressDown(70)) // Undo
                {
                    Main.Controller.GrindToolState.mapEditor.Undo();
                }

                //PlayerController.Instances[0].gameplay.inputController.rewiredPlayer.GetButtonShortPressDown(69)
                if (PlayerController.Instance.inputController.player.GetButtonShortPressDown(69)) // Redo
                {
                    Main.Controller.GrindToolState.mapEditor.Redo();
                }
            }
            else if (Main.Controller.EditorController.CurrentState.GetType() == typeof(WaxToolState))
            {
                //PlayerController.Instances[0].gameplay.inputController.rewiredPlayer.GetButtonDown("Y")
                if (PlayerController.Instance.inputController.player.GetButtonShortPressDown("Y"))
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
