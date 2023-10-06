using UnityEngine;
using System.Collections.Generic;

namespace GrindTools.Utils
{
    public class MatUtil
    {
        public static MatUtil __instance { get; private set; }
        public static MatUtil Instance => __instance ?? (__instance = new MatUtil());

        private void ApplyMaterials(MeshRenderer visualisationMR, Material mat)
        {
            if (visualisationMR != null)
            {
                visualisationMR.material = mat;
            }
        }
        public void UpdateMaterial(Material mat)
        {
            if (mat != null)
            {
                if (Main.controller.grindToolState.visualisationMR.material = mat)
                    return;
                ApplyMaterials(Main.controller.grindToolState.visualisationMR, mat);
            }
        }
    }
}