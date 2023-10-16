using System;
using UnityModManagerNet;

namespace GrindTools
{
    [Serializable]
    public class Settings : UnityModManager.ModSettings, IDrawable
    {
        public float CamFOV = 70f;
        public bool capColliders = false;
        public bool waxWholeSpline = true;
        public void OnChange()
        {
            throw new NotImplementedException();
        }
        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }
    }
}