using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityModManagerNet;

namespace GrindTools
{
    [Serializable]
    public class Settings : UnityModManager.ModSettings, IDrawable
    {
        public static Settings Instance { get; set; }

        // ----- Start Set KeyBindings ------
        public KeyBinding Hotkey = new KeyBinding { keyCode = KeyCode.S };

        private static readonly KeyCode[] keyCodes = Enum.GetValues(typeof(KeyCode))
           .Cast<KeyCode>()
           .Where(k => ((int)k < (int)KeyCode.Mouse0))
           .ToArray();

        // Get Key on KeyPress
        public static KeyCode? GetCurrentKeyDown()
        {
            if (!Input.anyKeyDown)
            {
                return null;
            }

            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKey(keyCodes[i]))
                {
                    return keyCodes[i];
                }
            }
            return null;
        }

       
        // ----- End Set KeyBindings ------

        public Color BGColor = new Color(0.0f, 0.0f, 0.0f);

        //----- Toggles ----------
        public bool HotKeyToggle = false;

        //------floats---------
        //public float MoveSpeed = 25f;

        public void resetToggles()
        {

        }
        public void OnChange()
        {
            throw new NotImplementedException();
        }
        public override void Save(UnityModManager.ModEntry modEntry)
        {
            //Save(this, modEntry);
        }
    }
}
