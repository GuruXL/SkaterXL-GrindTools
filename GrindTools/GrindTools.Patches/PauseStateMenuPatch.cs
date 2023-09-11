﻿using UnityEngine.UI;
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
    public class PauseStateMenuPatch
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
        public static void GrindToolButtonOnClick()
        {
            //GameStateMachine.Instance.RequestMapEditorState();
            Main.inputctrl.RequestGrindTool();
        }
        public static void DestroyGrindToolButton()
        {
            if (grindToolsButton != null)
            {
                Object.Destroy(grindToolsButton.gameObject);
                grindToolsButton = null;
            }
        }
    }
}
