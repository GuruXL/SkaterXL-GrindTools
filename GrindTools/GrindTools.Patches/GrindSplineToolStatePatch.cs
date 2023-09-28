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
            //CheckForNewSplines();
            RemoveNodes();
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
                currentCount = 0;
                return;
            }

            if (childCount == currentCount)
                return;

            if (childCount > currentCount)
            {
                Transform lastChild = parent.GetChild(childCount - 1);
                if (lastChild.GetComponent<MapEditorSplineObject>() != null)
                {
                    MessageSystem.QueueMessage(MessageDisplayData.Type.Success, $"New Spline Created", 2f);
                }
                currentCount = childCount;
            }
            else
            {
                currentCount = childCount;
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Spline Creation Failed", 2f);
            }
        }
        */
        private static void RemoveNodes()
        {
            if (Main.inputctrl.player.GetButton("LB"))
            {
                if (Main.inputctrl.player.GetButtonUp(13))
                {
                    if (CheckRaycastsPatch.GetSelectedSpline() != null)
                    {
                        CheckRaycastsPatch.SetSelectedSplineNull();
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