using HarmonyLib;
using UnityEngine;
using MapEditor;
using GrindTools.Utils;

namespace GrindTools.Patches
{

    [HarmonyPatch(typeof(MapEditorController), "Update")]
    public static class MapEditorControllerPatch
    {
        public static void Prefix(ref MapEditorController __instance)
        {
            __instance.initialState = MapEditorController.Instance.SimplePlacerState;
        }
    }
}