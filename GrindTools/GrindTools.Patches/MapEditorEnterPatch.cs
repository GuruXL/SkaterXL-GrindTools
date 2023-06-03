using System;
using GameManagement;
using UnityEngine;
using HarmonyLib;

namespace GrindTools.Patches
{
	[HarmonyPatch(typeof(MapEditorGameState))]
	[HarmonyPatch("OnEnter")]
	public class MapEditorEnterPatch
	{
		private static void Postfix(ref MapEditorGameState __instance)
		{
			__instance.OnEnter(GameStateMachine.Instance.LastState);
            if (!__instance.IsCurrentState)
            {
				GameStateMachine.Instance.RequestMapEditorState();
            }
		}
	}
}
