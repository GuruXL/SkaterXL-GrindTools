using HarmonyLib;
using MapEditor;
using SkaterXL.Map;
using SkaterXL.Core;
using UnityEngine;
using ModIO.UI;
using Cinemachine;
using Rewired;
using Object = UnityEngine.Object;

namespace GrindTools.Patches
{
    [HarmonyPatch(typeof(WaxToolState), "Update")]
    public static class DeleteSplinePatch
    {
        [HarmonyPostfix]
        public static void Postfix(WaxToolState __instance)
        {
            CinemachineVirtualCamera cam = Main.controller.waxToolCam;
            IMapEditorSelectable HightlightedObj = null;
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);

            // Reusing the 'ray' variable here
            if (Physics.Raycast(ray, out RaycastHit hitInfo, -cam.transform.localPosition.z, LayerUtility.MapEditorSelectionMask))
            {
                HightlightedObj = hitInfo.collider.GetComponentInParent<IMapEditorSelectable>();
            }

            if (HightlightedObj != null)
            {
                if (Main.inputctrl.player.GetButton("LB"))
                {
                    AccessTools.Method(typeof(WaxToolState), "ShowInfo").Invoke(__instance, new object[] { "Warning: Spline Deletion is Permanent" });

                    if (Main.inputctrl.player.GetButtonUp(13))
                    {
                        //MapEditorController.Instance.DeleteObstacle(HightlightedObj);
                        Object.Destroy(HightlightedObj.gameObject);
                        AccessTools.Method(typeof(WaxToolState), "ShowInfo").Invoke(__instance, new object[] { "Spline Deleted" });
                        MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Spline Deleted", 2.5f);
                    }
                }
            }
        }
    }
}