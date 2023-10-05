using UnityEngine;
using MapEditor;
using SkaterXL.Map;
using GameManagement;
using Cinemachine;
using System.Collections.Generic;
using System;
using ModIO.UI;
using GrindTools.UI;
using GrindTools.Data;
using GrindTools.Utils;
using GrindTools.Patches;
using System.Threading.Tasks;
using TMPro;

namespace GrindTools
{
    public class Controller : MonoBehaviour
    {
        private Transform statesObj;
        private Transform grindtoolObj;
        private Transform waxToolObj;
        //private Transform speedFactorText;
        private Transform grind_ControlsUI;
        private Transform wax_ControlsUI;

        //public Transform grind_popupUI;
        //public CanvasGroup grind_popupcanvas;
        //public TMP_Text grind_popuptext;

        //public MapEditorController editorController;
        //private MapEditorGameState editorGameState;
        public GrindSplineToolState grindToolState;
        public WaxToolState waxToolState;
        //public SimpleMode simplePlacerState;

        public CinemachineVirtualCamera grindToolCam;
        public CinemachineVirtualCamera waxToolCam;

        private int activeSplineCount = 0;

        public void Awake()
        {
            GetObjects();
            GetComponents();
            DisableDefaultUI();
        }
        private void Update()
        {
            MapEditorState editorState = MapEditorController.Instance.CurrentState;
            if (editorState == null || !(editorState is MapEditorState))
                return;

            CheckForNewSplines();
        }
        private void OnGUI()
        {
            switch (MapEditorController.Instance.CurrentState)
            {
                case GrindSplineToolState grindtoolstate:
                    ControlsUI.Instance.Show(ToolStates.Grind);
                    break;
                case WaxToolState waxtoolstate:
                    ControlsUI.Instance.Show(ToolStates.Wax);
                    break;
            }
        }
        private void GetObjects()
        {
            statesObj = GameStateMachine.Instance.MapEditorObject.transform.Find("States");
            grindtoolObj = statesObj?.Find("GrindSpline Tool");
            waxToolObj = statesObj?.Find("Wax Tool");
            grind_ControlsUI = grindtoolObj?.Find("Controls UI");
            wax_ControlsUI = waxToolObj?.Find("Controls UI");
            //grind_popupUI = grindtoolObj?.Find("WaxToolUI");
            //speedFactorText = MapEditorController.Instance.speedFactorText.transform.parent;
        }      
        private void GetComponents()
        {
            grindToolState = grindtoolObj?.GetComponent<GrindSplineToolState>();
            waxToolState = waxToolObj?.GetComponent<WaxToolState>();
            grindToolCam = grindtoolObj?.GetComponentInChildren<CinemachineVirtualCamera>();
            waxToolCam = waxToolObj?.GetComponentInChildren<CinemachineVirtualCamera>();

            //grind_popupcanvas = grind_popupUI.GetComponent<CanvasGroup>();
            //grind_popuptext = grind_popupUI.GetComponentInChildren<TMP_Text>();
        }
        public void AllowRespawn(bool state)
        {
            switch (state)
            {
                case true:
                    GameStateMachine.Instance.allowRespawn = true;
                    GameStateMachine.Instance.allowPinMovement = true;
                    break;
                case false:
                    GameStateMachine.Instance.allowRespawn = false;
                    GameStateMachine.Instance.allowPinMovement = false;
                    break;
            }
        }
        /*
        public void ToggleSpeedText(bool state)
        {
            speedFactorText.gameObject.SetActive(state);
        }
        */
        public void SetCamFov()
        {
            if (Main.settings.CamFOV == grindToolCam.m_Lens.FieldOfView)
                return;
            grindToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
            waxToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
        }
        private void DisableDefaultUI()
        {
            grind_ControlsUI.gameObject.SetActive(false);
            wax_ControlsUI.gameObject.SetActive(false);
        }
        private void CheckForNewSplines()
        {
            var parent = MapEditorController.Instance.placedObjectsParent;
            if (parent == null)
                return;

            int childCount = parent.childCount;
            if (childCount == 0)
            {
                activeSplineCount = 0;
                return;
            }

            if (childCount == activeSplineCount)
                return;

            if (childCount > activeSplineCount)
            {
                if (MapEditorController.Instance.CurrentState?.GetType() == typeof(SimpleMode))
                {
                    activeSplineCount = childCount;
                    return;
                }
                else
                {
                    Transform lastChild = parent.GetChild(childCount - 1);
                    if (lastChild.GetComponent<MapEditorSplineObject>() == null || !lastChild.gameObject.name.ToLower().Contains("spline"))
                    {
                        activeSplineCount = childCount;
                        return;
                    }
                    else
                    {
                        UISounds.Instance.PlayOneShotSelectMajor();
                        MessageSystem.QueueMessage(MessageDisplayData.Type.Success, $"New Spline Created", 2f);
                    }
                    activeSplineCount = childCount;
                }
            }
            else
            {
                activeSplineCount = childCount;
            }
        }
    }
}
