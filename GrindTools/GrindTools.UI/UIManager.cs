using UnityEngine;
using UnityEngine.UI;
using MapEditor;
using GameManagement;
using TMPro;

namespace GrindTools.UI
{
    public class UIManager : MonoBehaviour
    {
        Transform mapEditorUI;
        Transform speedFactorText;
        Transform grind_ControlsUI;
        Transform wax_ControlsUI;
        Transform simple_ControlsUI;

        Transform newControlsUI;
        Transform infoPanel;

        private void Start()
        {
            GetUIObjects();
            CreateGrindUI();
            DisableUIObjects();
        }
        private void GetUIObjects()
        {
            //mapEditorUI = GameStateMachine.Instance.MapEditorObject.transform.Find("MapEditorUI");
            mapEditorUI = Main.controller.editorController.ModeSelectionUI.transform.parent;
            speedFactorText = Main.controller.editorController.speedFactorText.transform.parent;

            // Get Default ControlsUI parents
            grind_ControlsUI = Main.controller.grindtoolObj.Find("Controls UI");
            wax_ControlsUI = Main.controller.waxToolObj.Find("Controls UI");
            simple_ControlsUI = Main.controller.editorController.SimplePlacerState.gameObject.transform.Find("Controls UI");

            CloneControlsUI();
        }

        private void CloneControlsUI()
        {
            newControlsUI = Instantiate(simple_ControlsUI);
            newControlsUI.transform.SetParent(Main.controller.grindtoolObj);
            infoPanel = newControlsUI.Find("Info Panel");
            grind_ControlsUI.gameObject.SetActive(false);
            newControlsUI.gameObject.SetActive(true);
        }
        private void CreateGrindUI()
        {
            Transform aButton = infoPanel.Find("Place");
            Transform xButton = infoPanel.Find("Open Object Selection");
            Transform camHeader = infoPanel.Find("HEADER Camera");
            Transform undo = infoPanel.Find("Change Obstacle");
            Transform switchMode = infoPanel.Find("Switch Mode");

            infoPanel.gameObject.GetComponent<Image>().enabled = true;

            infoPanel.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(1298, 550);

            camHeader.gameObject.SetActive(true);

            var _switch = switchMode.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            _switch.color = new Color(0.364f, 0.364f, 0.364f);

            var Alabel = aButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            Alabel.text = "Add Spline Points";

            var Xlabel = xButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            Xlabel.text = "Create Spline";

            var undoLabel = undo.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            undoLabel.text = "Remove Spline Points";

            for (int i = 0; i < infoPanel.childCount; i++)
            {
                if (infoPanel.GetChild(i).name.Equals("Movement") || infoPanel.GetChild(i).name.Equals("Rotation"))
                {
                    infoPanel.GetChild(i).gameObject.SetActive(true);
                }
            }
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
            //var header = grind_ControlsUI.FindChildRecursively("HEADER Obstacle");
            //var frictionUI = grind_ControlsUI.FindChildRecursively("Select");

            //Wax tool UI items
            var undo = wax_ControlsUI.FindChildRecursively("Undo Redo");

            //header.gameObject.SetActive(false);
            //frictionUI.gameObject.SetActive(false);
            undo.gameObject.SetActive(false);
        }
    }
}
