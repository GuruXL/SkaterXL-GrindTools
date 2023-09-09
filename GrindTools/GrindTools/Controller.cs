using UnityEngine;
using MapEditor;
using GameManagement;
using Cinemachine;
using System.Collections.Generic;
using System;
using ModIO.UI;

namespace GrindTools
{
    public class Controller : MonoBehaviour
    {
        public Transform statesObj;
        public Transform grindtoolObj;
        public Transform waxToolObj;
        public Transform speedFactorText;
        public Transform grind_ControlsUI;
        public Transform wax_ControlsUI;

        public MapEditorController editorController;
        public MapEditorGameState editorGameState;
        public GrindSplineToolState grindToolState;
        public WaxToolState waxToolState;       

        public CinemachineVirtualCamera grindToolCam;
        public CinemachineVirtualCamera waxToolCam;
        public OutlineManager outline;
        public void Awake()
        {
            GetMapEditor();
            GetObjects();
            GetComponents();
            DisableDefaultUI();
        }

        private float delay = 0.2f;
        private float lastDelay = 0f;
        public void Update()
        {
            if (Time.time - lastDelay >= delay)
            {
                SetCamFov();
            }
        }

        private void GetMapEditor()
        {
            editorController = GameStateMachine.Instance.MapEditorObject.GetComponentInChildren<MapEditorController>();
            editorGameState = GameStateMachine.Instance.MapEditorObject.GetComponentInChildren<MapEditorGameState>();
        }
        private void GetObjects()
        {
            statesObj = GameStateMachine.Instance.MapEditorObject.transform.Find("States");
            grindtoolObj = statesObj?.Find("GrindSpline Tool");
            waxToolObj = statesObj?.Find("Wax Tool");
            speedFactorText = editorController.speedFactorText.transform.parent;
            grind_ControlsUI = grindtoolObj?.Find("Controls UI");
            wax_ControlsUI = waxToolObj?.Find("Controls UI");
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
                    //grindToolState.Enter(grindToolState);
                    editorController.ChangeState(grindToolState);
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $"Grind Tool Active", 1f);
                    break;

                case "Wax": // Wax Tool
                    //waxToolState.Enter(waxToolState);
                    editorController.ChangeState(waxToolState);
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Info, $"Wax Tool Active", 1f);
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
        public void DisableDefaultUI()
        {
            grind_ControlsUI.gameObject.SetActive(false);
            wax_ControlsUI.gameObject.SetActive(false);
        }
        public void DeletePlacedSplines()
        {
            if (editorController.placedObjectsParent.childCount <= 0)
                return;
            MapEditorSplineObject[] placedSplines = editorController.placedObjectsParent.GetComponentsInChildren<MapEditorSplineObject>();
            if (placedSplines != null)
            {
                foreach (MapEditorSplineObject splines in placedSplines)
                {
                    Destroy(splines.gameObject);
                }
            }
        }
        public void OutlinePlacedSplines(bool state)
        {
            if (editorController.placedObjectsParent.childCount <= 0)
                return;
            MapEditorSplineObject[] placedSplines = editorController.placedObjectsParent.GetComponentsInChildren<MapEditorSplineObject>();
            if (placedSplines != null)
            {
                foreach (MapEditorSplineObject splines in placedSplines)
                {
                    switch (state)
                    {
                        case true:
                            outline.AddOutlineTo(splines.gameObject, state);
                            break;
                        case false:
                            outline.RemoveOutlineOn(splines.gameObject);
                            break;
                    }
                }
            }
        }
    }
}
