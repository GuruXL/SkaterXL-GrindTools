using GameManagement;
using MapEditor;
using HarmonyLib;
using System;
using UnityEngine;
using ModIO.UI;
using GrindTools.Utils;

namespace GrindTools.Listeners
{
    [HarmonyPatch(typeof(MapEditorController), "CreateNewSpline")]
    public static class CreateSplineListener
    {
        public static event Action<MapEditorSplineObject> OnSplineCreated;

        public static void Postfix(MapEditorSplineObject __result)
        {
            OnSplineCreated?.Invoke(__result);
        }

        public static void IsSplineCreated(MapEditorSplineObject __result)
        {
            CheckForNewSplines(__result);
        }

        private static void CheckForNewSplines(MapEditorSplineObject __result)
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
                if (__result != null)
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
    }  
}
