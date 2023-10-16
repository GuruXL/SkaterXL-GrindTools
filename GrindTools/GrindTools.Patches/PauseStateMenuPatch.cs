using UnityEngine.UI;
using GameManagement;
using HarmonyLib;
using UnityEngine;
using MapEditor;
using GrindTools.Utils;
using Object = UnityEngine.Object;
using UnityEngine.Events;

namespace GrindTools.Patches
{
    [HarmonyPatch(typeof(PauseState))]
    [HarmonyPatch("OnEnter")]
    public class PauseStateMenuPatch
    {
        private static MenuButton mapEditorButton;
        private static MenuButton grindToolsButton;

        private static void Postfix(ref PauseState __instance)
        {
            // Enable DIY map editor button
            __instance.MapEditorButton.gameObject.SetActive(true);
            __instance.MapEditorButton.GreyedOut = false;

            if (mapEditorButton == null)
            {
                mapEditorButton = __instance.MapEditorButton.gameObject.GetComponent<MenuButton>();
            }

            if (grindToolsButton == null)
            {
                // Add new listener to the original MapEditorButton -- Fix for issue with MapEditor Initial state being grindtool
                __instance.MapEditorButton.onClick.RemoveAllListeners();
                __instance.MapEditorButton.onClick.SetPersistentListenerState(0, UnityEventCallState.Off);
                __instance.MapEditorButton.onClick.AddListener(() => MapEditorButtonOnClick());

                GameObject originalButton = __instance.MapEditorButton.gameObject;
                GameObject newButton = Object.Instantiate(originalButton, originalButton.transform.parent);
                newButton.transform.SetSiblingIndex(originalButton.transform.GetSiblingIndex() + 1);
                newButton.name = "GrindToolsButton";

                grindToolsButton = newButton.GetComponent<MenuButton>();
                grindToolsButton.GreyedOut = false;
                grindToolsButton.GreyedOutInfoText = "Grind Tools";
                grindToolsButton.Label.SetText("Grind Tools");

                grindToolsButton.onClick.RemoveAllListeners();  // Remove existing listeners
                grindToolsButton.onClick.SetPersistentListenerState(0, UnityEventCallState.Off); // removes persistant listeners that are set in unity editor.
                grindToolsButton.onClick.AddListener(() => GrindToolButtonOnClick());  // Add new listener
            }

            __instance.StateMachine.PauseObject.SetActive(false);
            __instance.StateMachine.PauseObject.SetActive(true);

        }
        public static async void GrindToolButtonOnClick()
        {
            await StateManager.Instance.RequestMEState(Main.controller.grindToolState);
        }
        public static async void MapEditorButtonOnClick()
        {
            await StateManager.Instance.RequestMEState(MapEditorController.Instance.SimplePlacerState);
        }
        public static void DestroyGrindToolButton()
        {
            if (grindToolsButton != null)
            {
                Object.Destroy(grindToolsButton.gameObject);
                grindToolsButton = null;

                mapEditorButton.onClick.RemoveListener(MapEditorButtonOnClick); // remove so that multiple instances of the customOnClick will not be created if a new GrindToolButton is created.
            }
        }
    }
}