using HarmonyLib;
using UnityEngine;
using GameManagement;
using MapEditor;
using GrindTools.Utils;

namespace GrindTools.Patches
{
    // this patch fixes a bug when the grind tool was the inital state
    [HarmonyPatch(typeof(MapEditorController), "Update")]
    public static class MapEditorControllerPatch
    {
        public static void Prefix(MapEditorController __instance)
        {
            
            if(__instance.initialState != MapEditorController.Instance.SimplePlacerState)
            {
                GameStateMachine.Instance.RequestMapEditorState();
                __instance.initialState = MapEditorController.Instance.SimplePlacerState;
            }
            
        }
    }
    
}