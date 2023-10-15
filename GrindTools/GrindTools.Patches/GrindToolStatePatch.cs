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
        private static bool hasUpdated;

        [HarmonyPrefix]
        static void Prefix(GrindSplineToolState __instance)
        {
            if (!Main.settings.capColliders)
                return;

            UpdateScale(__instance,ref __instance.width, ref __instance.height);
            //AccessTools.Method(typeof(GrindSplineToolState), "UpdateSizes").Invoke(__instance, new object[] { __instance.width, __instance.height });
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

















































