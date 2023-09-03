using UnityEngine;
using MapEditor;
using GameManagement;

namespace GrindTools
{
    public class Controller : MonoBehaviour
    {
        //private Transform Gameplay;
        //private Transform MapEditor;
        private Transform StatesObj;
        private Transform GrindtoolObj;
        private Transform WaxToolObj;
        //private Transform PlaceTool;
        //private Transform grindCam;
        //private Transform waxCam;
        //private Transform Node;

        //private bool objectsFound;
        //List<Transform> ObjectList = new List<Transform>();

        public MapEditorController EditorController;
        public MapEditorGameState EditorGameState;
        public GrindSplineToolState GrindToolState;
        public WaxToolState WaxToolState;
        //private MultiplayerMenuController menuController;
        //public MeshRenderer NodeRenderer;

        public void Awake()
        {
            GetObjects();

            EditorController = GameStateMachine.Instance.MapEditorObject.GetComponentInChildren<MapEditorController>();
            EditorGameState = GameStateMachine.Instance.MapEditorObject.GetComponentInChildren<MapEditorGameState>();

            GrindToolState = GrindtoolObj.GetComponent<GrindSplineToolState>();
            WaxToolState = WaxToolObj.GetComponent<WaxToolState>();
        }

        private void GetObjects()
        {
            StatesObj = GameStateMachine.Instance.MapEditorObject.transform.Find("States");
            GrindtoolObj = StatesObj.Find("GrindSpline Tool");
            WaxToolObj = StatesObj.Find("Wax Tool");
        }
        
        public bool IsPlayState()
        {

            if (GameStateMachine.Instance.CurrentState.GetType() != typeof(PlayState))
            {
                return false;
            }
            else
            {
                return true;
            }
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
        public void EnableMapEditorState()
        {
            if (!Main.controller.IsPlayState())
            {
                GameStateMachine.Instance.RequestPlayState();
            }
            GameStateMachine.Instance.RequestMapEditorState();
        }
        */

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

        /*
        public void RestStates()
        {
            PlaceTool.gameObject.SetActive(true);
            Grindtool.gameObject.SetActive(false);
            WaxTool.gameObject.SetActive(false);
        }
        */
        //---------- Settings ----------
    }
}
