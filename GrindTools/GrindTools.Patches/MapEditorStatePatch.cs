using HarmonyLib;
using UnityEngine;
using MapEditor;
using GrindTools.Utils;

namespace GrindTools.Patches
{
    public class MapEditorStatePatch
    {
        
        [HarmonyPatch(typeof(MapEditorState), "OnDpadLeft")]
        public static class OnDpadLeftPatch
        {
            public static void Postfix(MapEditorState __instance)
            {
                var selectedSpline = Traverse.Create(__instance).Field("selectedSpline").GetValue<MapEditorSplineObject>();

                if (selectedSpline != null && selectedSpline.nodes.Count <= 1)
                {
                    Object.Destroy(selectedSpline.gameObject);
                }
            }
        }
        
        [HarmonyPatch(typeof(MapEditorState), "OnDpadRight")]
        public static class OnDpadRightPatch
        {
            public static void Postfix(MapEditorState __instance)
            {
                var selectedSpline = Traverse.Create(__instance).Field("selectedSpline").GetValue<MapEditorSplineObject>();

                if (selectedSpline != null && selectedSpline.nodes.Count <= 1)
                {
                    Object.Destroy(selectedSpline.gameObject);
                }
            }
        }  
    }   
}