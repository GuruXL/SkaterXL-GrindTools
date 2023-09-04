using UnityEngine;
using System.Collections.Generic;

namespace GrindTools.utils
{
    public class MatUtil : MonoBehaviour
    {
        public MeshRenderer VisualNode;

        private void Start()
        {
            VisualNode = Main.controller.GrindToolState.visualisationMR;
        }
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
                ApplyMaterials(VisualNode, AssetLoader.RedMatfrombundle);
            }
        }
        public void ApplyBlueMat()
        {
            if (AssetLoader.BlueMatfrombundle != null)
            {
                ApplyMaterials(VisualNode, AssetLoader.BlueMatfrombundle);
            }
        }
        public void ApplyGreenMat()
        {
            if (AssetLoader.GreenMatfrombundle != null)
            {
                ApplyMaterials(VisualNode, AssetLoader.GreenMatfrombundle);
            }
        }
    }
}