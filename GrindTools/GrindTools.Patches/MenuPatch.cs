using UnityEngine.UI;  // Necessary for the Button component
using GameManagement;
using HarmonyLib;
using UnityEngine;
using System;
using Object = UnityEngine.Object;
using TMPro;

namespace GrindTools.Patches
{
    [HarmonyPatch(typeof(PauseState))]
    [HarmonyPatch("OnEnter")]
    public class MenuPatch
    {
        public static MenuButton grindToolsButton;

        private static void Postfix(ref PauseState __instance)
        {
            // Enable DIY map editor button
            __instance.MapEditorButton.gameObject.SetActive(true);
            __instance.MapEditorButton.GreyedOut = false;

            if (grindToolsButton == null)
            {
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
            }           

            __instance.StateMachine.PauseObject.SetActive(false);
            __instance.StateMachine.PauseObject.SetActive(true);
        }

        public static void GrindToolButtonOnClick()
        {
            //GameStateMachine.Instance.RequestMapEditorState();
            Main.inputctrl.RequestGrindTool();
        }
    }
}
