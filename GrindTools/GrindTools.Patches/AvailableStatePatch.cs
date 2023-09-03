using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MapEditor;

namespace GrindTools.Patches
{
    [HarmonyPatch(typeof(MapEditorController), "UpdateAvailableStates")]
    public static class AvailableStatePatch
    {
        public static void Postfix(MapEditorController __instance)
        {
            // Use reflection to get the private field
            FieldInfo field = __instance.GetType().GetField("availableMapEditorStates", BindingFlags.NonPublic | BindingFlags.Instance);

            // Get the current list from the private field
            List<MapEditorState> availableMapEditorStates = (List<MapEditorState>)field.GetValue(__instance);

            // Modify the list
            availableMapEditorStates.Add(Main.controller.GrindToolState);
            availableMapEditorStates.Add(Main.controller.WaxToolState);

            // set the modified list back to the private field
            field.SetValue(__instance, availableMapEditorStates);
        }
    }
}
