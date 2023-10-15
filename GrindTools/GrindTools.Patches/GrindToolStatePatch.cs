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
        [HarmonyPrefix]
        static void Prefix(GrindSplineToolState __instance)
        {
            if (!Main.settings.capColliders)
                return;

            UpdateScale(ref __instance.width, ref __instance.height);
            AccessTools.Method(typeof(GrindSplineToolState), "UpdateSizes").Invoke(__instance, new object[] { __instance.width, __instance.height });
        }

        private static void UpdateScale(ref float width, ref float height)  // Parameters by reference
        {
            if (width != height)
            {
                width = height;
            }
            else if (height != width)
            {
                height = width;
            }
        }
    }
}

















































