using HarmonyLib;
using UnityEngine;
using MapEditor;
using GrindTools.Utils;
using ModIO.UI;

namespace GrindTools.Patches
{

    [HarmonyPatch(typeof(GrindSplineToolState), "Update")]
    public static class GrindSplineToolStatePatch
    {
        //private static int currentCount = 0;
        // used to keep track of newly placed splines
        private static void Postfix(GrindSplineToolState __instance)
        {
            if (MapEditorController.Instance.CurrentState.GetType() != typeof(GrindSplineToolState))
                return;

            CheckForInput();
        }
        /*
        private static void CheckForNewSplines()
        {
            var parent = MapEditorController.Instance.placedObjectsParent;
            if (parent == null) 
                return;

            int childCount = parent.childCount;
            if (childCount == 0)
            {
                Main.eventListener.activeSplineCount = 0;
                return;
            }

            if (childCount == Main.eventListener.activeSplineCount)
                return;

            if (childCount > Main.eventListener.activeSplineCount)
            {
                Transform lastChild = parent.GetChild(childCount - 1);
                if (lastChild.GetComponent<MapEditorSplineObject>() != null)
                {
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Success, $"New Spline Created", 2f);
                }
                Main.eventListener.activeSplineCount = childCount;
            }
            else
            {
                Main.eventListener.activeSplineCount = childCount;
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Spline Creation Failed", 2f);
            }
        }
        */
        private static void CheckForInput()
        {
            if (Main.inputctrl.player.GetButton("LB"))
            {
                if (Main.inputctrl.player.GetButtonUp(13))
                {
                    if (CheckRaycastsPatch.GetSelectedSpline() != null)
                    {
                        CheckRaycastsPatch.SetSelectedSplineNull();

                        if (CheckRaycastsPatch.GetSelectedSpline() == null)
                        {
                            UISounds.Instance.PlayOneShotSelectionChange();
                            MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, $"Active Spline Removed", 1f);
                        }
                        else
                        {
                            MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Failed To Remove Active Spline", 1f);
                        }
                    }
                    else
                    {
                        MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"No Active Spline", 1f);
                    }
                }
            }
        }
    }   
}