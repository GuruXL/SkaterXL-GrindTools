using HarmonyLib;
using MapEditor;
using GameManagement;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace GrindTools.Patches
{
    [HarmonyPatch(typeof(GameStateMachine), "RequestTransitionTo")]
    [HarmonyPatch(new Type[] { typeof(GameState), typeof(bool), typeof(Action<GameState>) })]  // Method signature
    public static class RequestTransitionToPatch
    {
        [HarmonyPrefix]
        public static void Prefix(GameStateMachine __instance, GameState requestedState, bool alwaysAddToNavStack, Action<GameState> transitionAction)
        {
            if (requestedState is MapEditorGameState)
            {
                var TraversedCurrentState = Traverse.Create(__instance).Field("CurrentState");
                var TraversedLastState = Traverse.Create(__instance).Field("LastState");
                //var TraversedStateNavStack = Traverse.Create(__instance).Field("stateNavigationStack");
                //var TraversedExcludedFromNav = Traverse.Create(__instance).Field("statesExcludedFromNavigationStack");
                var traversedOnGameStateChanged = Traverse.Create(__instance).Field("OnGameStateChanged");
                var traversedOnGameStateChangedRefs = Traverse.Create(__instance).Field("OnGameStateChangedRefs");

                GameState currentState = TraversedCurrentState.GetValue<GameState>();
                __instance.CurrentState.OnExit(requestedState);

                /*
                if (requestedState.ClearNavStackOnEnter)
                    TraversedStateNavStack.GetValue<Stack<GameState>>().Clear();
                else if (__instance.CurrentState == requestedState)
                    Debug.Log(("Transition from GameState to itself: " + requestedState.name));
                else if (TraversedStateNavStack.GetValue<Stack<GameState>>().Count > 0 && TraversedStateNavStack.GetValue<Stack<GameState>>().Peek() == requestedState && !alwaysAddToNavStack)
                {
                    Debug.Log("Going back a state");
                    TraversedStateNavStack.GetValue<Stack<GameState>>().Pop();
                }
                else if (!TraversedExcludedFromNav.GetValue<List<Type>>().Contains(currentState.GetType()))
                    TraversedStateNavStack.GetValue<Stack<GameState>>().Push(currentState);
                */
                if (transitionAction != null)
                {
                    try
                    {
                        transitionAction(requestedState);
                    }
                    catch (Exception ex)
                    {
                        Main.Logger.Error(string.Format("Error in transition Action: {0}", ex));
                    }
                }

                TraversedLastState.SetValue(__instance.CurrentState);
                TraversedCurrentState.SetValue(requestedState);
                __instance.CurrentState.OnEnter(currentState);

                try
                {
                    GameStateMachine.OnGameStateChangedEvent gameStateChanged = traversedOnGameStateChanged.GetValue<GameStateMachine.OnGameStateChangedEvent>();
                    if (gameStateChanged != null)
                    {
                        gameStateChanged?.Invoke(currentState != null ? currentState.GetType() : null, requestedState.GetType());
                    }
                }
                catch (Exception ex)
                {
                    Main.Logger.LogException(ex);
                }

                try
                {
                    GameStateMachine.OnGameStateChangedRefsEvent stateChangedRefs = traversedOnGameStateChangedRefs.GetValue<GameStateMachine.OnGameStateChangedRefsEvent>();
                    if (stateChangedRefs != null)
                    {
                        stateChangedRefs?.Invoke(currentState, __instance.CurrentState);
                    }
                }
                catch (Exception ex)
                {
                    Main.Logger.LogException(ex);
                }

                Main.Logger.Log(("GameState Transition " + __instance.LastState.GetType().Name + " -> " + __instance.CurrentState.GetType().Name));
            }
        }
    }
}