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
            bool rbPressed = PlayerController.Instance.inputController.player.GetButton(7);

            // Return false to stop the execution of the MoveCamera function if RB is pressed
            return !rbPressed;
        }
    }
}