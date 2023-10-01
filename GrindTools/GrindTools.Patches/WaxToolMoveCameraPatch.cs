using GameManagement;
using MapEditor;
using HarmonyLib;
using System;
using Rewired;

namespace GrindTools.Patches
{
    [HarmonyPatch(typeof(WaxToolState))]
    [HarmonyPatch("MoveCamera")]
    public class WaxToolMoveCameraPatch
    {
        private static bool Prefix()
        {
            bool rbPressed = PlayerController.Main.input.GetButton(7);
            return !rbPressed;
        }
    }
}