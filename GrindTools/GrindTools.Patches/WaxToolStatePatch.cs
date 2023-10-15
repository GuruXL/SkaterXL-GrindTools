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
using System.Reflection;
using System;

namespace GrindTools.Patches
{

    [HarmonyPatch(typeof(WaxToolState), "Update")]
    public static class WaxToolStatePatch
    {
        private static IMapEditorSelectable _HightlightedObj;
        private static SplineComputer _splineComp;
        private static RaycastHit _hitInfo;
        public static IMapEditorSelectable HightlightedObj
        {
            get { return _HightlightedObj; }
            set { _HightlightedObj = value; }
        }
        public static SplineComputer splineComp
        {
            get { return _splineComp; }
            set { _splineComp = value; }
        }
        public static RaycastHit hitInfo
        {
            get { return _hitInfo; }
        }
        [HarmonyPrefix]
        static bool Prefix(WaxToolState __instance)
        {
            CinemachineVirtualCamera cam = Main.controller.waxToolCam;
            _HightlightedObj = null;
            _splineComp = null;
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            float maxDistance = 100f;

            if (Physics.Raycast(ray, out _hitInfo, maxDistance))
            {
                _HightlightedObj = _hitInfo.collider.GetComponentInParent<IMapEditorSelectable>();
                _splineComp = _hitInfo.collider.GetComponentInParent<SplineComputer>();
                if (_splineComp == null)
                    maxDistance = _hitInfo.distance + 1f;
            }
            if (_splineComp == null && Physics.SphereCast(ray, 0.2f, out _hitInfo, maxDistance, LayerUtility.GrindableMask))
            {
                _splineComp = _hitInfo.collider.GetComponentInParent<SplineComputer>();
                if (_HightlightedObj == null)
                    _HightlightedObj = _hitInfo.collider.GetComponentInParent<IMapEditorSelectable>();
            }

            if (RewiredInput.PrimaryPlayer.GetButtonDown(13)) // sets waxWhole Spline to always true; temp fix for issues with outlines when waxwholespline is false.
            {
                return false;
            }
            else if (RewiredInput.PrimaryPlayer.GetButtonTimedPressDown("Left Stick Button", 0.25f))
            {
                Main.settings.waxWholeSpline = !Main.settings.waxWholeSpline;
                var waxWholeSpline = Traverse.Create(__instance).Field("waxWholeSpline");
                waxWholeSpline.SetValue(Main.settings.waxWholeSpline);
                UISounds.Instance.PlayOneShotSelectionChange();
                return false;
            }
            return true; // Execute the original method
        }
    }
}



















































