using HarmonyLib;
using MapEditor;
using SkaterXL.Map;
using SkaterXL.Core;
using Dreamteck.Splines;
using UnityEngine;
using ModIO.UI;
using Cinemachine;
using Rewired;
using GrindTools.Data;
using Object = UnityEngine.Object;

namespace GrindTools.Patches
{

    [HarmonyPatch(typeof(WaxToolState), "Update")]
    public static class WaxToolStatePatch
    {
        private static IMapEditorSelectable HightlightedObj;
        private static SplineComputer splineComp;
        private static RaycastHit hitInfo;

        [HarmonyPostfix]
        public static void Postfix(WaxToolState __instance)
        {
            CinemachineVirtualCamera cam = Main.controller.waxToolCam;
            HightlightedObj = null;
            splineComp = null;
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            float maxDistance = 100f;

            if (Physics.Raycast(ray, out hitInfo, maxDistance))
            {
                HightlightedObj = hitInfo.collider.GetComponentInParent<IMapEditorSelectable>();
                splineComp = hitInfo.collider.GetComponentInParent<SplineComputer>();
                if (splineComp == null)
                    maxDistance = hitInfo.distance + 1f;
            }
            if (splineComp == null && Physics.SphereCast(ray, 0.2f, out hitInfo, maxDistance, LayerUtility.GrindableMask))
            {
                splineComp = hitInfo.collider.GetComponentInParent<SplineComputer>();
            }
        }
        public static SplineComputer GetSplineComp()
        {
            return splineComp;
        }
        public static IMapEditorSelectable GetHighlightedObj()
        {
            return HightlightedObj;
        }
        public static RaycastHit GetRayHitInfo()
        {
            return hitInfo;
        }
    }
}



















































