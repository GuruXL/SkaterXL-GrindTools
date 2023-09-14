using HarmonyLib;
using UnityEngine;
using MapEditor;
using GrindTools.Utils;
using ModIO.UI;

namespace GrindTools.Patches
{
    [HarmonyPatch(typeof(GrindSplineToolState), "CheckRaycasts")]
    public static class CheckRaycastsPatch
    {
        private static MapEditorSplineObject selectedSpline;
        private static void Postfix(GrindSplineToolState __instance)
        {
            
            var currentNode = Traverse.Create(__instance).Field("currentNode").GetValue<GrindableEdgeNode>();
            var screenRayDidHit = Traverse.Create(__instance).Field("screenRayDidHit").GetValue<bool>();
            selectedSpline = Traverse.Create(__instance).Field("selectedSpline").GetValue<MapEditorSplineObject>();

            if (screenRayDidHit)
            {
                if (__instance.IsNodeValid(currentNode))
                {
                    if (selectedSpline == null)
                    {
                        // Green
                        MatUtil.Instance.UpdateMaterial(AssetLoader.GreenMat);
                        return;
                    }
                    else if (selectedSpline.nodes.Count >= 1)
                    {
                        // Blue
                        MatUtil.Instance.UpdateMaterial(AssetLoader.BlueMat);
                        return;
                    }
                    else // if selectedSpline is not null but has 0 nodes, destroy and set null
                    {
                        SetSelectedSplineNull();
                    }
                }
                else
                {
                    // Red
                    MatUtil.Instance.UpdateMaterial(AssetLoader.RedMat);
                }
            }
            
        }

        public static MapEditorSplineObject GetSelectedSpline()
        {
            return selectedSpline;
        }

        public static void SetSelectedSplineNull()
        {
            if (selectedSpline != null)
            {
                Object.Destroy(selectedSpline.gameObject);
                selectedSpline = null;
            }
        }
    }
}
