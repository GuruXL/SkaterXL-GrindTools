using HarmonyLib;
using MapEditor;
using Object = UnityEngine.Object;
using System.Reflection;
using System;

namespace GrindTools.Patches
{
    [HarmonyPatch(typeof(GrindSplineToolState), "Update")]
    public static class GrindToolStatePatch
    {
        [HarmonyPostfix]
        static bool Postfix(GrindSplineToolState __instance)
        {   
            return true; // Execute the original method
        }
    }
}



















































