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
                // Assign the new material back to the MeshRenderer
                visualisationMR.material = mat;
            }
        }
        public void ApplyRedMat()
        {
            if (AssetLoader.RedMatfrombundle != null)
            {
                ApplyMaterials(Main.controller.GrindToolState.visualisationMR, AssetLoader.RedMatfrombundle);
            }
        }
        public void ApplyBlueMat()
        {
            if (AssetLoader.BlueMatfrombundle != null)
            {
                ApplyMaterials(Main.controller.GrindToolState.visualisationMR, AssetLoader.BlueMatfrombundle);
            }
        }
        public void ApplyGreenMat()
        {
            if (AssetLoader.GreenMatfrombundle != null)
            {
                ApplyMaterials(Main.controller.GrindToolState.visualisationMR, AssetLoader.GreenMatfrombundle);
            }
        }
    }
}