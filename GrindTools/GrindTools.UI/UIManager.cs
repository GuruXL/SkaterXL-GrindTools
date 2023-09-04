using UnityEngine;
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

        Transform indicatorsObj;
        Transform indicators_Clone;
        Transform indicator_Panel;
        Transform panel_x;
        Transform panel_y;
        Transform panel_a;
        Transform indicator_Label;
        Transform indicator_Label2;

        RectTransform indicator_Panel_Rect;
        RectTransform indicator_Label_Rect;
        RectTransform indicator_Label_Rect2;

        TextMeshProUGUI label1_text;
        TextMeshProUGUI label2_text;

        private void Start()
        {
            GetUIObjects();
            CreateIndicatorClone();
            DisableUIObjects();
        }
        private void GetUIObjects()
        {
            mapEditorUI = GameStateMachine.Instance.MapEditorObject.transform.Find("MapEditorUI");
            indicatorsObj = mapEditorUI.transform.Find("Indicators");
            speedFactorText = mapEditorUI.transform.Find("Speed Factor Canvas");

            grind_ControlsUI = Main.controller.GrindtoolObj.Find("Controls UI");
            wax_ControlsUI = Main.controller.WaxToolObj.Find("Controls UI");
        }

        private void CreateIndicatorClone()
        {
            if (indicators_Clone == null)
            {
                indicators_Clone = Instantiate(indicatorsObj);
                indicators_Clone.SetParent(mapEditorUI);

                indicator_Panel = indicators_Clone.transform.Find("Panel");
                indicator_Label = indicators_Clone.transform.Find("Space Label");

                indicator_Label2 = Instantiate(indicator_Label);
                indicator_Label2.SetParent(indicators_Clone);
                indicator_Label2.localScale = new Vector3(1, 1, 1);

                panel_x = indicator_Panel.Find("X");
                panel_y = indicator_Panel.Find("Y");
                panel_a = indicator_Panel.Find("Z");

                panel_y.gameObject.SetActive(false);

                indicator_Panel_Rect = indicator_Panel.GetComponent<RectTransform>();
                indicator_Label_Rect = indicator_Label.GetComponent<RectTransform>();
                indicator_Label_Rect2 = indicator_Label2.GetComponent<RectTransform>();

                label1_text = indicator_Label.gameObject.GetComponent<TextMeshProUGUI>();
                label1_text.autoSizeTextContainer = true;
                label2_text = indicator_Label2.gameObject.GetComponent<TextMeshProUGUI>();
                label2_text.autoSizeTextContainer = true;

                PosIndicators();
                SetLabelText(label1_text, "Apply Spline");
                SetLabelText(label2_text, "Add Spline Points");
            }
        }
        private void PosIndicators()
        {
            if (indicator_Panel_Rect != null && indicator_Label_Rect != null)
            {
                indicator_Panel_Rect.anchoredPosition = new Vector2(1350, -450);
                indicator_Label_Rect.anchoredPosition = new Vector2(1420, -460);
                indicator_Label_Rect2.anchoredPosition = new Vector2(1420, -520);
            }
        }
        public void ToggleSpeedText(bool state)
        {
            speedFactorText.gameObject.SetActive(state);
        }
        public void ToggleIndicators(bool state)
        {
            indicators_Clone.gameObject.SetActive(state);
        }
        public void SetLabelText(TextMeshProUGUI label, string text)
        {
            label.text = text;
        }
        private void DisableUIObjects()
        {
            var header = grind_ControlsUI.FindChildRecursively("HEADER Obstacle");
            var frictionUI = grind_ControlsUI.FindChildRecursively("Select");
            var copingUI = grind_ControlsUI.FindChildRecursively("Clone Object");

            header.gameObject.SetActive(false);
            frictionUI.gameObject.SetActive(false);
            copingUI.gameObject.SetActive(false);
        }
    }
}
