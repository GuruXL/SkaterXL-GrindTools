using GameManagement;
using HarmonyLib;

namespace GrindTools.Patches
{
    [HarmonyPatch(typeof(GameStateMachine))]
    [HarmonyPatch("get_allowMapEditor")]
    public class AllowMapEditorPatch
    {
        private static void Postfix(ref bool __result) => __result = true;
    }
}
