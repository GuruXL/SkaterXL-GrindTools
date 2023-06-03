using HarmonyLib;
using System.Reflection;
using RapidGUI;
using UnityEngine;
using UnityModManagerNet;
using ModIO.UI;

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
        //public static UI UIctrl;
        public static InputController Inputctrl;
        public static Controller Controller;

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
            if (GUILayout.Button("Delete All Placed Objects", RGUIStyle.button, GUILayout.Width(256)))
            {
                Controller.EditorController.DeleteAllObstacles();
            }
            GUILayout.EndVertical();
        }
        private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            //settings.Save(modEntry);
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
                    //UIctrl = ScriptManager.AddComponent<UI>();
                    Inputctrl = ScriptManager.AddComponent<InputController>();
                    Controller = ScriptManager.AddComponent<Controller>();
                    Object.DontDestroyOnLoad(ScriptManager);
                }
                else
                {
                    harmonyInstance.UnpatchAll(harmonyInstance.Id);
                    Object.Destroy(ScriptManager);
                }
                flag = true;
            }
            return flag;
        }
        public static bool Unload(UnityModManager.ModEntry modEntry)
        {
            Logger.Log(nameof(Unload));
            return true;
        }

        public static UnityModManager.ModEntry.ModLogger Logger => modEntry.Logger;
    }
}
