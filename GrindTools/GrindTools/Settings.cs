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
        public Color BGColor = new Color(0.0f, 0.0f, 0.0f);
        public float CamFOV = 70f;

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
