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
            //gets the private field
            FieldInfo field = __instance.GetType().GetField("availableMapEditorStates", BindingFlags.NonPublic | BindingFlags.Instance);

            // Gets the current list
            List<MapEditorState> availableMapEditorStates = (List<MapEditorState>)field.GetValue(__instance);

            // Modifys the list
            if (!availableMapEditorStates.Contains(Main.controller.grindToolState)) { availableMapEditorStates.Add(Main.controller.grindToolState); }
            if (!availableMapEditorStates.Contains(Main.controller.waxToolState)) { availableMapEditorStates.Add(Main.controller.waxToolState); }
            if (!availableMapEditorStates.Contains(MapEditorController.Instance.SimplePlacerState)) { availableMapEditorStates.Add(MapEditorController.Instance.SimplePlacerState); }
            if (!availableMapEditorStates.Contains(MapEditorController.Instance.FineMovementState)) { availableMapEditorStates.Add(MapEditorController.Instance.FineMovementState); }

            // sets the modified list back to the private field
            field.SetValue(__instance, availableMapEditorStates);
        }
    }
}
