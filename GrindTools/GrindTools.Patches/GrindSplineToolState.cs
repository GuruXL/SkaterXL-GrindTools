using HarmonyLib;
using UnityEngine;
using MapEditor;
using GrindTools.Utils;

namespace GrindTools.Patches
{
    [HarmonyPatch(typeof(GrindSplineToolState), "CheckRaycasts")]
    public static class GrindSplineToolStatePatch
    {
        public static void Postfix(GrindSplineToolState __instance)
        {
            var currentNode = Traverse.Create(__instance).Field("currentNode").GetValue<GrindableEdgeNode>();
            var screenRayDidHit = Traverse.Create(__instance).Field("screenRayDidHit").GetValue<bool>();
            var selectedSpline = Traverse.Create(__instance).Field("selectedSpline").GetValue<MapEditorSplineObject>();

            if (screenRayDidHit)
            {
                if (__instance.IsNodeValid(currentNode))
                {
                    if (selectedSpline == null)
                    {
                        MatUtil.Instance.ApplyGreenMat();
                        return;
                    }
                    else if (selectedSpline != null && selectedSpline.nodes.Count <= 0)
                    {
                        Object.Destroy(selectedSpline);
                    }

                    MatUtil.Instance.ApplyBlueMat();
                    return;
                }
                else
                {
                    MatUtil.Instance.ApplyRedMat();
                }
            }
        }
    }
}