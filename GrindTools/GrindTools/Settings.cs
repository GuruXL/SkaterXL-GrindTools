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
        // ----- End Set KeyBindings ------

        public Color BGColor = new Color(0.0f, 0.0f, 0.0f);

        //----- Toggles ----------
        public bool HotKeyToggle = false;

        //------floats---------
        public float MoveSpeed = 25f;

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
