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
        [HarmonyPostfix]
        public static void Postfix(WaxToolState __instance)
        {
            CinemachineVirtualCamera cam = Main.controller.waxToolCam;
            IMapEditorSelectable HightlightedObj = null;
            SplineComputer splineComp = null;
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hitInfo;
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

            /*
            if (Physics.Raycast(ray, out RaycastHit hitInfo, -cam.transform.localPosition.z, LayerUtility.MapEditorSelectionMask))
            {
                HightlightedObj = hitInfo.collider.GetComponentInParent<IMapEditorSelectable>();
                splineComp = hitInfo.collider.GetComponentInParent<SplineComputer>();
            }
            */
            /*
            if (HightlightedObj != null)
            {
                CheckForDeleteInput(__instance, HightlightedObj);
            }
            */

            if (splineComp != null)
            {
                CheckForDeleteInput(__instance, HightlightedObj, hitInfo);
                SwapGrindTags(__instance, splineComp);
            }
        }
        /*
        private static void CheckForDeleteInput(WaxToolState __instance, SplineComputer spline)
        {
            if (Main.inputctrl.player.GetButton("LB"))
            {
                ShowInfo(__instance, "Warning: Spline Deletion is Permanent");

                if (Main.inputctrl.player.GetButtonUp(13))
                {
                    Object.Destroy(spline.gameObject);
                    ShowInfo(__instance, "Spline Deleted");
                    UISounds.Instance.PlayOneShotSelectionChange();
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, $"Spline Deleted", 1f);
                }
            }
        }
        */       
        private static void CheckForDeleteInput(WaxToolState __instance, IMapEditorSelectable HightlightedObj, RaycastHit hitInfo) 
        {
            if (Main.inputctrl.player.GetButton("LB"))
            {
                if (hitInfo.collider.GetComponentInParent<IMapEditorSelectable>() == null)
                {
                    ShowInfo(__instance, "Cannot Delete Map Splines");
                    return;
                }
                ShowInfo(__instance, "Warning: Spline Deletion is Permanent");

                if (Main.inputctrl.player.GetButtonUp(13))
                {
                    Object.Destroy(HightlightedObj.gameObject);
                    ShowInfo(__instance, "Spline Deleted");
                    UISounds.Instance.PlayOneShotSelectionChange();
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, $"Spline Deleted", 0.5f);
                }
            }
        }
        
        private static void SwapGrindTags(WaxToolState __instance, SplineComputer spline)
        {
            string concrete = "Grind_Concrete";
            string metal = "Grind_Metal";

            if (Main.inputctrl.player.GetButtonDown(0))
            {
                if (spline.gameObject.tag == concrete)
                {
                    SetTagRecursively(spline.gameObject, metal);
                    ShowInfo(__instance, "Metal");
                    return;
                }
                else if (spline.gameObject.tag == metal)
                {
                    SetTagRecursively(spline.gameObject, concrete);
                    ShowInfo(__instance, "Concrete");
                    return;
                }
                else // if tag is unknown or undefined the default is concrete so swap to metal
                {
                    SetTagRecursively(spline.gameObject, metal);
                    ShowInfo(__instance, "Metal");
                }
            }
        }
        private static void SetTagRecursively(GameObject obj, string tag)
        {
            obj.tag = tag;

            if (obj.transform.childCount > 0)
            {
                foreach (Transform child in obj.transform)
                {
                    SetTagRecursively(child.gameObject, tag);
                }
            }
        }
        private static void ShowInfo(WaxToolState __instance, string text)
        {
            AccessTools.Method(typeof(WaxToolState), "ShowInfo").Invoke(__instance, new object[] { text });
        }
    }
}