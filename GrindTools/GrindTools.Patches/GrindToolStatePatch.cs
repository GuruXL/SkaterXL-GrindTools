using HarmonyLib;
using MapEditor;
using Object = UnityEngine.Object;
using System.Reflection;
using System;
using UnityEngine;

namespace GrindTools.Patches
{
    [HarmonyPatch(typeof(GrindSplineToolState), "Update")]
    public static class GrindToolStatePatch
    {
        private static bool hasUpdated = true;
       
        [HarmonyPostfix]
        static void Postfix(GrindSplineToolState __instance)
        {
            if (Main.settings.capColliders)
            {
                UpdateScale(__instance, ref __instance.width, ref __instance.height);
                //var didMove = Traverse.Create(__instance).Field("didMove");
                //didMove.SetValue(true);
                //__instance.MoveCamera();
                //AccessTools.Method(typeof(GrindSplineToolState), "UpdateSizes").Invoke(__instance, new object[] { __instance.width, __instance.height });
            }

            if (RewiredInput.PrimaryPlayer.GetButtonDown(0))
            {
                UISounds.Instance.PlayOneShotSelectionChange();
            }
        }

        private static void UpdateScale(GrindSplineToolState __instance, ref float width, ref float height)  // Parameters by reference
        {
            if (width != height)
            {
                width = height;
                hasUpdated = false;
            }
            else if (height != width)
            {
                height = width;
                hasUpdated = false;
            }

            if (!hasUpdated)
            {
                AccessTools.Method(typeof(GrindSplineToolState), "UpdateSizes").Invoke(__instance, new object[] { __instance.width, __instance.height });
                hasUpdated = true;
            }
        }
    }
}