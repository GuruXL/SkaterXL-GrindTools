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
        /*
        public void ApplyRedMat()
        {
            if (AssetLoader.RedMat != null)
            {
                if (Main.controller.grindToolState.visualisationMR.material = AssetLoader.RedMat)
                    return;
                ApplyMaterials(Main.controller.grindToolState.visualisationMR, AssetLoader.RedMat);
            }
        }
        public void ApplyBlueMat()
        {
            if (AssetLoader.BlueMat != null)
            {
                if (Main.controller.grindToolState.visualisationMR.material = AssetLoader.BlueMat)
                    return;
                ApplyMaterials(Main.controller.grindToolState.visualisationMR, AssetLoader.BlueMat);
            }
        }
        public void ApplyGreenMat()
        {
            if (AssetLoader.GreenMat != null)
            {
                if (Main.controller.grindToolState.visualisationMR.material = AssetLoader.GreenMat)
                    return;
                ApplyMaterials(Main.controller.grindToolState.visualisationMR, AssetLoader.GreenMat);
            }
        }
        */
    }
}