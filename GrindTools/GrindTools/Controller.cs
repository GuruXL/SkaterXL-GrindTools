using UnityEngine;
using MapEditor;
using GameManagement;
using Cinemachine;

namespace GrindTools
{
    public class Controller : MonoBehaviour
    {
        public Transform StatesObj;
        public Transform GrindtoolObj;
        public Transform WaxToolObj;

        public MapEditorController EditorController;
        public MapEditorGameState EditorGameState;
        public GrindSplineToolState GrindToolState;
        public WaxToolState WaxToolState;
        //public MeshRenderer NodeRenderer;

        public CinemachineVirtualCamera grindToolCam;
        public CinemachineVirtualCamera waxToolCam;

        public void Awake()
        {
            EditorController = GameStateMachine.Instance.MapEditorObject.GetComponentInChildren<MapEditorController>();
            EditorGameState = GameStateMachine.Instance.MapEditorObject.GetComponentInChildren<MapEditorGameState>();

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
            StatesObj = GameStateMachine.Instance.MapEditorObject.transform.Find("States");
            GrindtoolObj = StatesObj.Find("GrindSpline Tool");
            WaxToolObj = StatesObj.Find("Wax Tool");
        }      
        private void GetComponents()
        {
            GrindToolState = GrindtoolObj.GetComponent<GrindSplineToolState>();
            WaxToolState = WaxToolObj.GetComponent<WaxToolState>();

            grindToolCam = GrindtoolObj.GetComponentInChildren<CinemachineVirtualCamera>();
            waxToolCam = WaxToolObj.GetComponentInChildren<CinemachineVirtualCamera>();
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
                    EditorController.ChangeState(GrindToolState);
                    break;

                case "Wax": // Wax Tool
                    EditorController.ChangeState(WaxToolState);
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
    }
}
