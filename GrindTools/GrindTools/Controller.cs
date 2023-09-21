using UnityEngine;
using MapEditor;
using GameManagement;
using Cinemachine;
using System.Collections.Generic;
using System;
using ModIO.UI;
using GrindTools.UI;

namespace GrindTools
{
    public class Controller : MonoBehaviour
    {
        private Transform statesObj;
        private Transform grindtoolObj;
        private Transform waxToolObj;
        private Transform speedFactorText;
        private Transform grind_ControlsUI;
        private Transform wax_ControlsUI;

        //public MapEditorController editorController;
        //private MapEditorGameState editorGameState;
        public GrindSplineToolState grindToolState;
        public WaxToolState waxToolState;
        //public SimpleMode simplePlacerState;

        public CinemachineVirtualCamera grindToolCam;
        public CinemachineVirtualCamera waxToolCam;
        public OutlineManager outline;

        public void Awake()
        {
            GetObjects();
            GetComponents();
            DisableDefaultUI();
        }
        private void OnGUI()
        {
            if (MapEditorController.Instance.CurrentState == grindToolState)
            {
                ControlsUI.Instance.Show("Grind");
            }
            else if (MapEditorController.Instance.CurrentState == waxToolState)
            {
                ControlsUI.Instance.Show("Wax");
            }
        }
        private void GetObjects()
        {
            statesObj = GameStateMachine.Instance.MapEditorObject.transform.Find("States");
            grindtoolObj = statesObj?.Find("GrindSpline Tool");
            waxToolObj = statesObj?.Find("Wax Tool");
            grind_ControlsUI = grindtoolObj?.Find("Controls UI");
            wax_ControlsUI = waxToolObj?.Find("Controls UI");
            speedFactorText = MapEditorController.Instance.speedFactorText.transform.parent;
        }      
        private void GetComponents()
        {
            grindToolState = grindtoolObj?.GetComponent<GrindSplineToolState>();
            waxToolState = waxToolObj?.GetComponent<WaxToolState>();
            grindToolCam = grindtoolObj?.GetComponentInChildren<CinemachineVirtualCamera>();
            waxToolCam = waxToolObj?.GetComponentInChildren<CinemachineVirtualCamera>();
            outline = GameStateMachine.Instance.gameObject.GetComponentInChildren<OutlineManager>();
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
        public void ToggleState(string options)
        {
            switch (options)
            {
                case "Grind": // Grind tool
                    MapEditorController.Instance.ChangeState(grindToolState);
                    if (MapEditorController.Instance.CurrentState == grindToolState)
                    {
                        MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $"Grind Tool Active", 1f);
                    }
                    else
                    {
                        MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Grind Tool State Transition Failed", 1f);
                    }
                    break;

                case "Wax": // Wax Tool
                    MapEditorController.Instance.ChangeState(waxToolState);
                    if (MapEditorController.Instance.CurrentState == waxToolState)
                    {
                        MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $"Wax Tool Active", 1f);
                    }
                    else
                    {
                        MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Wax Tool State Transition Failed", 1f);
                    }
                    break;
            }
        }
        public void ToggleSpeedText(bool state)
        {
            speedFactorText.gameObject.SetActive(state);
        }
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
    }
}
