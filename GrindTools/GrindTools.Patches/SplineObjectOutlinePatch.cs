using GameManagement;
using HarmonyLib;
using MapEditor;

namespace GrindTools.Patches
{
    [HarmonyPatch(typeof(MapEditorSplineObject))]
    [HarmonyPatch("get_outlineColliders")]
    public class SplineObjectOutlinePatch
    {
        private static void Postfix(ref bool __result) => __result = true;
    }
}
