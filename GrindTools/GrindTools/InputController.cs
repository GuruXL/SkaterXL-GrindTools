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
                WaitForInput();
            }
            else if (!GameStateMachine.Instance.allowRespawn)
            {
                Main.controller.AllowRespawn(true);
            }
        }

        private void WaitForInput()
        {
            if (PlayerController.Instance.inputController.player.GetButtonLongPressDown(13))
            {
                Main.controller.ToggleState("Grind");
                MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $" Grind Tool Active", 2f);
            }

            if (Main.controller.EditorController.CurrentState.GetType() == typeof(GrindSplineToolState))
            {
                if (PlayerController.Instance.inputController.player.GetButtonDown("Y"))
                {
                    Main.controller.ToggleState("Wax");
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $" Wax Tool Active", 2f);
                }

            }
            else if (Main.controller.EditorController.CurrentState.GetType() == typeof(WaxToolState))
            {
                if (PlayerController.Instance.inputController.player.GetButtonDown("Y"))
                {
                    Main.controller.ToggleState("Grind");
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $" Grind Tool Active", 2f);
                }
            }
        }
        
    }
}
