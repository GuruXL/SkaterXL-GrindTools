using UnityEngine;
using System.Collections.Generic;

namespace GrindTools.utils
{
    public class MatUtil : MonoBehaviour
    {
        public Material newMaterial;
        public MeshRenderer visualiztionMR;

        private void Start()
        {
            visualiztionMR = Main.controller.GrindToolState.visualisationMR;
            CreateMats();
        }

        public void CreateMats()
        {

        }

        private void ApplyMaterials(MeshRenderer visualisationMR, int textureIndex)
        {
            if (visualisationMR != null)
            {
                // Assign the new material back to the MeshRenderer
                visualisationMR.material = newMaterial;
            }
        }
    }
}