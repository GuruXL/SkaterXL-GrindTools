using HarmonyLib;
using System.Reflection;
using RapidGUI;
using UnityEngine;
using UnityModManagerNet;
using ModIO.UI;
using GrindTools.Utils;
using GrindTools.Patches;
using GrindTools.UI;

namespace GrindTools
{
    [EnableReloading]
    internal static class Main
    {
        public static bool enabled;
        public static Settings settings;
        public static Harmony harmonyInstance;
        public static string modId = "GrindTools";
        public static UnityModManager.ModEntry modEntry;
        public static GameObject ScriptManager;
        public static Controller controller;
        public static InputController inputctrl;
        //public static UIManager uiManager;
        //public static MatUtil matUtil;
        public static ControlsOverlay overlay;

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            settings = UnityModManager.ModSettings.Load<Settings>(modEntry);
            modEntry.OnGUI = OnGUI;
            modEntry.OnSaveGUI = new System.Action<UnityModManager.ModEntry>(OnSaveGUI);
            modEntry.OnToggle = new System.Func<UnityModManager.ModEntry, bool, bool>(OnToggle);
            modEntry.OnUnload = new System.Func<UnityModManager.ModEntry, bool>(Unload);
            Main.modEntry = modEntry;
            Logger.Log(nameof(Load));

            return true;
        }
        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            
            GUILayout.BeginVertical(GUILayout.Width(256));
            if (GUILayout.Button("Delete All Placed Splines", RGUIStyle.button, GUILayout.Width(256)))
            {
                controller.DeletePlacedSplines();
                MessageSystem.QueueMessage(MessageDisplayData.Type.Warning, $"Custom Splines Deleted", 2.5f);
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical(GUILayout.Width(256));
            if (RGUI.Button(settings.OutlineSplines, "Outline Placed Splines"))
            {
                settings.OutlineSplines = !settings.OutlineSplines;
                controller.OutlinePlacedSplines(settings.OutlineSplines);
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical(GUILayout.Width(256));
            settings.CamFOV = RGUI.SliderFloat(settings.CamFOV, 40f, 120f, 80f, "Cam Fov");
            GUILayout.EndVertical();
        }
        private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            settings.Save(modEntry);
        }
        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            bool flag;
            if (enabled == value)
            {
                flag = true;
            }
            else
            {
                enabled = value;
                if (enabled)
                {
                    harmonyInstance = new Harmony((modEntry.Info).Id);
                    harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
                    ScriptManager = new GameObject("GrindTools");
                    Object.DontDestroyOnLoad(ScriptManager);
                    controller = ScriptManager.AddComponent<Controller>();
                    inputctrl = ScriptManager.AddComponent<InputController>();
                    //uiManager = ScriptManager.AddComponent<UIManager>();
                    //matUtil = ScriptManager.AddComponent<MatUtil>();
                    overlay = ScriptManager.AddComponent<ControlsOverlay>();

                    AssetLoader.LoadBundles();
                }
                else
                {
                    harmonyInstance.UnpatchAll(harmonyInstance.Id);
                    AssetLoader.UnloadAssetBundle();
                    Object.Destroy(ScriptManager);
                }
                flag = true;
            }
            return flag;
        }
        public static bool Unload(UnityModManager.ModEntry modEntry)
        {
            harmonyInstance.UnpatchAll(harmonyInstance.Id);
            AssetLoader.UnloadAssetBundle();
            Object.Destroy(ScriptManager);
            Logger.Log(nameof(Unload));
            return true;
        }

        public static UnityModManager.ModEntry.ModLogger Logger => modEntry.Logger;
    }
}
