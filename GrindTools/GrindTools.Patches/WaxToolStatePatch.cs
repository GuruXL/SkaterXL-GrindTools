using HarmonyLib;
using MapEditor;
using SkaterXL.Map;
using SkaterXL.Core;
using Dreamteck.Splines;
using UnityEngine;
using ModIO.UI;
using Cinemachine;
using Rewired;
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

            // Reusing the 'ray' variable here
            if (Physics.Raycast(ray, out RaycastHit hitInfo, -cam.transform.localPosition.z, LayerUtility.MapEditorSelectionMask))
            {
                HightlightedObj = hitInfo.collider.GetComponentInParent<IMapEditorSelectable>();
                splineComp = hitInfo.collider.GetComponentInParent<SplineComputer>();
            }

            if (HightlightedObj != null)
            {
                CheckForDeleteInput(__instance, HightlightedObj);
            }

            if (splineComp != null)
            {
                SwapGrindTags(__instance, splineComp);
            }
        }
        private static void CheckForDeleteInput(WaxToolState __instance, IMapEditorSelectable HightlightedObj)
        {
            if (Main.inputctrl.player.GetButton("LB"))
            {
                AccessTools.Method(typeof(WaxToolState), "ShowInfo").Invoke(__instance, new object[] { "Warning: Spline Deletion is Permanent" });

                if (Main.inputctrl.player.GetButtonUp(13))
                {
                    Object.Destroy(HightlightedObj.gameObject);
                    AccessTools.Method(typeof(WaxToolState), "ShowInfo").Invoke(__instance, new object[] { "Spline Deleted" });
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, $"Spline Deleted", 1f);
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
                    AccessTools.Method(typeof(WaxToolState), "ShowInfo").Invoke(__instance, new object[] { "Metal" });
                    return;
                }
                else if (spline.gameObject.tag == metal)
                {
                    SetTagRecursively(spline.gameObject, concrete);
                    AccessTools.Method(typeof(WaxToolState), "ShowInfo").Invoke(__instance, new object[] { "Concrete" });
                    return;
                }
                else // if tag is unknown or undefined the default is concrete in xl
                {
                    SetTagRecursively(spline.gameObject, metal);
                    AccessTools.Method(typeof(WaxToolState), "ShowInfo").Invoke(__instance, new object[] { "Metal" });
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

    }
}