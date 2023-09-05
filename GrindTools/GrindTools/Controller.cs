using UnityEngine;
using MapEditor;
using GameManagement;
using Cinemachine;
using System.Collections.Generic;
using System;

namespace GrindTools
{
    public class Controller : MonoBehaviour
    {
        public Transform statesObj;
        public Transform grindtoolObj;
        public Transform waxToolObj;

        public MapEditorController editorController;
        public MapEditorGameState editorGameState;
        public GrindSplineToolState grindToolState;
        public WaxToolState waxToolState;       
        //public MeshRenderer NodeRenderer;

        public CinemachineVirtualCamera grindToolCam;
        public CinemachineVirtualCamera waxToolCam;
        public OutlineManager outline;
        public void Awake()
        {
            editorController = GameStateMachine.Instance.MapEditorObject.GetComponentInChildren<MapEditorController>();
            editorGameState = GameStateMachine.Instance.MapEditorObject.GetComponentInChildren<MapEditorGameState>();
            outline = GameStateMachine.Instance.gameObject.GetComponentInChildren<OutlineManager>();

            GetObjects();
            GetComponents();
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

        private void GetObjects()
        {
            statesObj = GameStateMachine.Instance.MapEditorObject.transform.Find("States");
            grindtoolObj = statesObj.Find("GrindSpline Tool");
            waxToolObj = statesObj.Find("Wax Tool");
        }      
        private void GetComponents()
        {
            grindToolState = grindtoolObj.GetComponent<GrindSplineToolState>();
            waxToolState = waxToolObj.GetComponent<WaxToolState>();

            grindToolCam = grindtoolObj.GetComponentInChildren<CinemachineVirtualCamera>();
            waxToolCam = waxToolObj.GetComponentInChildren<CinemachineVirtualCamera>();
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
                    grindToolState.Enter(grindToolState);
                    editorController.ChangeState(grindToolState);
                    break;

                case "Wax": // Wax Tool
                    waxToolState.Enter(waxToolState);
                    editorController.ChangeState(waxToolState);
                    break;
            }
        }
        public void SetCamFov()
        {
            if (Main.settings.CamFOV == grindToolCam.m_Lens.FieldOfView)
                return;

            grindToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
            waxToolCam.m_Lens.FieldOfView = Main.settings.CamFOV;
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
