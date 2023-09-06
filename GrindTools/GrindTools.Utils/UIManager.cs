using UnityEngine;
using MapEditor;
using GameManagement;
using TMPro;

namespace GrindTools.Utils
{
    public class UIManager : MonoBehaviour
    {
        Transform mapEditorUI;
        Transform speedFactorText;
        Transform grind_ControlsUI;
        Transform wax_ControlsUI;
        Transform simple_ControlsUI;

        Transform newControlsUI;

        private void Start()
        {
            GetUIObjects();
            //CreateIndicatorClone();
            SetUpUIObjects();
            DisableUIObjects();
        }
        private void GetUIObjects()
        {
            //mapEditorUI = GameStateMachine.Instance.MapEditorObject.transform.Find("MapEditorUI");
            mapEditorUI = Main.controller.editorController.ModeSelectionUI.transform.parent;
            speedFactorText = Main.controller.editorController.speedFactorText.transform.parent;

            // Get ControlsUI parents
            grind_ControlsUI = Main.controller.grindtoolObj.Find("Controls UI");
            wax_ControlsUI = Main.controller.waxToolObj.Find("Controls UI");
            simple_ControlsUI = Main.controller.editorController.SimplePlacerState.gameObject.transform.Find("Controls UI");

        }
        private void SetUpUIObjects()
        {
            CreateGrindUI();
        }

        private void CreateGrindUI()
        {
            newControlsUI = Instantiate(simple_ControlsUI);
            newControlsUI.transform.SetParent(Main.controller.grindtoolObj);
            grind_ControlsUI.gameObject.SetActive(false);
            newControlsUI.gameObject.SetActive(true);

            // contine ui modifications here
        }

        private void CreateWaxUI()
        {
            // contine ui modifications here
        }

        public void ToggleSpeedText(bool state)
        {
            speedFactorText.gameObject.SetActive(state);
        }
       
        private void DisableUIObjects()
        {
            //Grind Tool UI itmes
            var header = grind_ControlsUI.FindChildRecursively("HEADER Obstacle");
            var frictionUI = grind_ControlsUI.FindChildRecursively("Select");

            //Wax tool UI items
            var undo = wax_ControlsUI.FindChildRecursively("Undo Redo");

            header.gameObject.SetActive(false);
            frictionUI.gameObject.SetActive(false);
            undo.gameObject.SetActive(false);
        }
    }
}
