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
        private static bool IsWaxWholeSpline;

        [HarmonyPrefix]
        static bool Prefix(WaxToolState __instance)
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

            // Replace with your own condition or button check
            //if (RewiredInput.PrimaryPlayer.GetButtonTimedPressDown("X", 0.5f))
            if (RewiredInput.PrimaryPlayer.GetButtonDown(13))
            {
                return false;
            }
            else if (RewiredInput.PrimaryPlayer.GetButtonTimedPressDown(13, 0.5f))
            {
                var waxWholeSpline = Traverse.Create(__instance).Field("waxWholeSpline");
                IsWaxWholeSpline = waxWholeSpline.GetValue<bool>();
                waxWholeSpline.SetValue(!IsWaxWholeSpline); // toggle the value
                AccessTools.Method(typeof(WaxToolState), "ShowInfo").Invoke(__instance, new object[] { $"wax whole spline: {IsWaxWholeSpline}" });
                return false; // Skip the original method
            }
            return true; // Execute the original method
        }
        /*
        [HarmonyPostfix]
        private static void Postfix(WaxToolState __instance)
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
        */
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
        public static bool GetWaxWholeSpline()
        {
            return IsWaxWholeSpline;
        }
    }
}



















































