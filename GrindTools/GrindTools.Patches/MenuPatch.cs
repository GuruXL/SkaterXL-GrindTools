﻿using System;
using GameManagement;
using UnityEngine;
using HarmonyLib;

namespace GrindTools.Patches
{
	[HarmonyPatch(typeof(PauseState))]
	[HarmonyPatch("OnEnter")]
	public class MenuPatch
	{
		private static void Postfix(ref PauseState __instance)
		{
			__instance.MapEditorButton.gameObject.SetActive(true);
			__instance.MapEditorButton.GreyedOut = false;
			__instance.MapEditorButton.enabled = true;
			__instance.MapEditorButton.interactable = true;
			GameStateMachine.Instance.PauseObject.SetActive(false);
			GameStateMachine.Instance.PauseObject.SetActive(true);
		}
	}
}
