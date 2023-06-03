using System.Collections.Generic;
using UnityEngine;
using MapEditor;
using GameManagement;
using SkaterXL.Gameplay;
using SkaterXL.Map;

namespace GrindTools
{
    class Controller : MonoBehaviour
    {     
        private Transform MasterPrefab;
        //private Transform Gameplay;
        //private Transform MapEditor;
        private Transform States;
        private Transform Grindtool;
        private Transform WaxTool;
        private Transform PlaceTool;
        private Transform grindCam;
        private Transform waxCam;
        //private Transform Node;

        private bool objectsFound;
        List<Transform> ObjectList = new List<Transform>();

        public MapEditorController EditorController;
        public MapEditorGameState EditorState;
        public GrindSplineToolState GrindToolState;
        public WaxToolState WaxToolState;
        //private MultiplayerMenuController menuController;
        //MeshRenderer NodeRenderer;
        //GameplayController GamePlayCtrl;
        RespawnController Respawn;


        public void Start()
        {
            //MasterPrefab = PlayerController.Instance.skaterController.transform.parent.transform.parent;
            MasterPrefab = PlayerController.Instances[0].transform.parent;
            if (MasterPrefab != null)
            {
                GetObjects();

                if (CheckObjectsFound(ObjectList))
                {
                    objectsFound = true;
                }
                else
                {
                    objectsFound = false;
                }
            }
            if (objectsFound)
            {
                GetToolComponents();
            }

            SwapToMapState();

            //UpdateMenu();
        }

        public void Update()
        {
            //if (GameStateMachine.Instance.CurrentState.GetType() == typeof(GrindSplineToolState) || GameStateMachine.Instance.CurrentState.GetType() == typeof(WaxToolState))
            //{
            //    UpdateSettings();
            //}

            OutlineSpines();
        }

        public bool CheckObjectsFound(List<Transform> List)
        {
            int i = 0;
            foreach (Transform Item in List)
            {
                if (Item != null)
                {
                    Main.Logger.Log(Item.name + " Found");
                    i++;
                }
            }
            if (i.Equals(List.Count))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GetObjects()
        {
            //Gameplay = MasterPrefab.Find("GamePlay");
            //Gameplay = MasterPrefab.FindChildRecursively("GamePlayNew");
            //MapEditor = MasterPrefab.Find("MapEditor");
            States = GameStateMachine.Instance.MapEditorObject.transform.Find("States");
            Grindtool = States.Find("GrindSpline Tool");
            grindCam = Grindtool.Find("FreeLookCamera");
            //Node = Grindtool.Find("CurrentNodeVisualization");
            WaxTool = States.Find("Wax Tool");
            waxCam = WaxTool.Find("FreeLookCamera");
            PlaceTool = States.Find("Simple Place");

            //ObjectList.Add(Gameplay);
            //ObjectList.Add(MapEditor);
            ObjectList.Add(States);
            ObjectList.Add(Grindtool);
            ObjectList.Add(grindCam);
            //ObjectList.Add(Node);
            ObjectList.Add(WaxTool);
            ObjectList.Add(waxCam);
            ObjectList.Add(PlaceTool);
        }

        public void GetToolComponents()
        {
            EditorController = GameStateMachine.Instance.MapEditorObject.GetComponent<MapEditorController>();
            EditorState = GameStateMachine.Instance.MapEditorObject.GetComponent<MapEditorGameState>();
            GrindToolState = Grindtool.GetComponent<GrindSplineToolState>();
            WaxToolState = WaxTool.GetComponent<WaxToolState>();

            //GamePlayCtrl = Gameplay.GetComponent<GameplayController>();
            Respawn = GameStateMachine.Instance.MainPlayer.gameplay.GetComponent<RespawnController>();
            //Main.Inputctrl.Input = Gameplay.GetComponent<InputController>();

            //NodeRenderer = Node.GetComponent<MeshRenderer>();
        }

        public bool IsPlayState()
        {
            
            if (GameStateMachine.Instance.CurrentState.GetType() == typeof(PlayState))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AllowRespawn(int options)
        {
            switch (options)
            {
                case 1:
                    GameStateMachine.Instance.allowRespawn = true;
                    GameStateMachine.Instance.allowPinMovement = true;
                    break;
                case 2:
                    GameStateMachine.Instance.allowRespawn = false;
                    GameStateMachine.Instance.allowPinMovement = false;
                    break;
            }
        }

        // -------- Menu Settings ----------
        void UpdateMenu()
        {

        }

        void SwapToMapState()
        {

        }

        //---------- Map Editor ----------
        public void ToggleMapEditor(int options)
        {
            switch (options)
            {
                case 1: // toggle On
                    if (objectsFound)
                    {
                        //GameStateMachine.Instance.RequestMapEditorState();
                        //GameStateMachine.Instance.RequestTransitionTo(typeof(MapEditorGameState), false, null);
                        GameStateMachine.Instance.RequestTransitionTo<MapEditorGameState>();
                        GameStateMachine.Instance.RequestMapEditorState();
                        AllowRespawn(2);
                        GameStateMachine.Instance.MapEditorObject.SetActive(true);
                        GameStateMachine.Instance.MainPlayer.gameObject.SetActive(false);
                        GameStateMachine.Instance.MainPlayer.DisableGameplay();
                    }
                    break;

                case 2: // toggle Off
                    if (objectsFound)
                    {
                        AllowRespawn(1);
                        GameStateMachine.Instance.MapEditorObject.SetActive(false);
                        GameStateMachine.Instance.MainPlayer.gameObject.SetActive(true);
                        GameStateMachine.Instance.MainPlayer.EnableGameplay();
                        GameStateMachine.Instance.RequestPlayState();
                        EditorController.ExitMapEditor();
                        Respawn.Respawn();

                    }
                    break;
            }
        }

        //---------- Grind Tool ----------
        public void ToggleTool(string options)
        {
            switch (options)
            {
                case "Grind": // Grind tool
                    if (objectsFound)
                    {
                        //EditorController.ChangeState(GrindToolState);
                        MapEditorController.Instance.ChangeState<GrindSplineToolState>();
                        Grindtool.gameObject.SetActive(true);
                        PlaceTool.gameObject.SetActive(false);
                        WaxTool.gameObject.SetActive(false);
                        //grindCam.position = PlayerController.Instance.skaterController.respawn.spawnPoint.position + new Vector3(0, 4, 0);
                        grindCam.position = Respawn.spawnPoint.position + new Vector3(0, 4, 0);
                    }
                    break;

                case "Wax": // Wax Tool
                    if (objectsFound)
                    {
                        //EditorController.ChangeState(WaxToolState);
                        MapEditorController.Instance.ChangeState<WaxToolState>();
                        WaxTool.gameObject.SetActive(true);
                        PlaceTool.gameObject.SetActive(false);
                        Grindtool.gameObject.SetActive(false);
                        //waxCam.position = PlayerController.Instance.skaterController.respawn.spawnPoint.position + new Vector3(0, 4, 0);
                        waxCam.position = Respawn.spawnPoint.position + new Vector3(0, 4, 0);
                    }
                    break;
            }
        }

        public void RestStates()
        {
            PlaceTool.gameObject.SetActive(true);
            Grindtool.gameObject.SetActive(false);     
            WaxTool.gameObject.SetActive(false);
            MapEditorController.Instance.ChangeState<SimpleMode>();
        }

        //---------- Settings ----------
        void OutlineSpines()
        {

            List<Transform> list = Main.settings.GetChildren(GrindToolState.mapEditor.placedObjectsParent, true);

            if (list != null)
            {
                foreach (Transform obj in list)
                {
                    if (obj.name.Equals("Spline Prefab(Clone)"))
                    {
                        OutlineManager.Instance.AddOutlineTo(obj.gameObject, true);
                    }
                }
            }
        }
    }
}
