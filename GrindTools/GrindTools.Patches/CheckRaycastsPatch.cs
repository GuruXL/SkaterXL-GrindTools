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

            // maybe change this to custom ray cast instead of highjacking this one
            if (screenRayDidHit)
            {
                if (__instance.IsNodeValid(currentNode))
                {
                    if (selectedSpline == null)
                    {
                        // Green
                        MatUtil.Instance.UpdateMaterial(AssetLoader.GreenMat);
                        //MatUtil.Instance.ApplyGreenMat();
                        return;
                    }
                    // Blue
                    MatUtil.Instance.UpdateMaterial(AssetLoader.BlueMat);
                    //MatUtil.Instance.ApplyBlueMat();
                    return;
                }
                else
                {
                    // Red
                    MatUtil.Instance.UpdateMaterial(AssetLoader.RedMat);
                    //MatUtil.Instance.ApplyRedMat();

                }
            }
            
        }

        public static MapEditorSplineObject GetSelectedSpline()
        {
            return selectedSpline;
        }
    }
}
