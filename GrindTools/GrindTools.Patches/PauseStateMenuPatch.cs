using UnityEngine.UI;
using GameManagement;
using HarmonyLib;
using UnityEngine;
using MapEditor;
using GrindTools.Utils;
using System;
using Object = UnityEngine.Object;
using TMPro;
using UnityEngine.EventSystems;

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
                grindToolsButton.onClick.AddListener(() => GrindToolButtonOnClick());  // Add new listener
                //CreateNewOnSubmit(grindToolsButton);
            }

            // remove DLC button :)
            if (PromotionController.Instance != null)
            {
                GameObject mainMenuBanner = Traverse.Create(PromotionController.Instance).Field("mainMenuBanner").GetValue<GameObject>();
                if (mainMenuBanner != null && mainMenuBanner.activeSelf)
                {
                    mainMenuBanner.SetActive(false);
                }
            }

            __instance.StateMachine.PauseObject.SetActive(false);
            __instance.StateMachine.PauseObject.SetActive(true);
        }

        // OnSubmit might not be needed
        /*
        public static void CustomOnSubmit(BaseEventData data)
        {
            Main.inputctrl.RequestGrindTool();
        }
        private static void CreateNewOnSubmit(MenuButton button)
        {
            EventTrigger eventTrigger = button.GetComponent<EventTrigger>();
            if (eventTrigger != null)
            {
                for (int i = eventTrigger.triggers.Count - 1; i >= 0; i--)
                {
                    if (eventTrigger.triggers[i].eventID == EventTriggerType.Submit)
                    {
                        eventTrigger.triggers.RemoveAt(i);
                    }
                }
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.Submit;
                entry.callback.AddListener(CustomOnSubmit); // Reference the custom function here
                eventTrigger.triggers.Add(entry);
            }
        }
        */
        public static async void GrindToolButtonOnClick()
        {
            await StateManager.Instance.RequestGrindTool();
        }
        public static void MapEditorButtonOnClick()
        {
            if (MapEditorController.Instance.initialState != MapEditorController.Instance.SimplePlacerState)
            {
                MapEditorController.Instance.initialState = MapEditorController.Instance.SimplePlacerState;
            }
            GameStateMachine.Instance.RequestMapEditorState();
            MapEditorController.Instance.ChangeState(MapEditorController.Instance.SimplePlacerState);
        }
        public static void DestroyGrindToolButton()
        {
            if (grindToolsButton != null)
            {
                Object.Destroy(grindToolsButton.gameObject);
                grindToolsButton = null;

                mapEditorButton.onClick.RemoveListener(MapEditorButtonOnClick); // Stops multiple instances of the customOnClick being created if a new GrindToolButton is created.
            }
        }
    }
}
