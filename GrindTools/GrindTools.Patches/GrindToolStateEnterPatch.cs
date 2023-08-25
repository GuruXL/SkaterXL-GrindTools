using System;
using GameManagement;
using UnityEngine;
using HarmonyLib;
using MapEditor;
using Cinemachine;

namespace GrindTools.Patches
{
	[HarmonyPatch(typeof(GrindSplineToolState))]
	[HarmonyPatch("Enter")]
	public class GrindToolStateEnterPatch
	{
		private static void Postfix(ref GrindSplineToolState __instance)
		{
			__instance.virtualCamera.gameObject.transform.position = PlayerController.Instance.cameraController.transform.position + new Vector3(0, 4, 0);
			Main.Logger.Log("Grind Tool State Enter postfix has Run");
		}
	}
}
