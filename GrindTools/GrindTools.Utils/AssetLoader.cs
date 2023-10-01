using System.Reflection;
using UnityEngine;
using System.IO;
using System.Collections;
using System;
using ModIO.UI;

namespace GrindTools.Utils
{
    public static class AssetLoader
    {
        public static Material RedMat;
        public static Material BlueMat;
        public static Material GreenMat;
        public static AssetBundle assetBundle;
        public static Texture2D loadingTexture = new Texture2D(1028, 1028);

        public static void LoadBundles()
        {
            // Check if a type from the Unity assembly has been loaded
            Type unityObjectType = Type.GetType("UnityEngine.Object, UnityEngine");

            if (unityObjectType != null)
            {
                PlayerController.Main.StartCoroutine(LoadAssetBundle());     
            }
        }   
        private static IEnumerator LoadAssetBundle()
        {
            byte[] assetBundleData = ResourceExtractor.ExtractResources("GrindTools.Resources.mats");
            if (assetBundleData == null)
            {
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Failed to EXTRACT GrindTools Asset Bundle", 2.5f);
                yield break;
            }
            AssetBundleCreateRequest abCreateRequest = AssetBundle.LoadFromMemoryAsync(assetBundleData);
            yield return abCreateRequest;

            assetBundle = abCreateRequest.assetBundle;
            if (assetBundle == null)
            {
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"Failed to LOAD GrindTools Asset Bundle", 2.5f);
                yield break;
            }
            yield return PlayerController.Main.StartCoroutine(LoadPrefabs());
        }
        private static IEnumerator LoadPrefabs()
        {
            if (assetBundle == null)
            {
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"GrindTools Asset bundles are not loaded!", 2.5f);
                yield break;
            }
            yield return PlayerController.Main.StartCoroutine(LoadAssetFromBundle());
        }
        private static IEnumerator LoadAssetFromBundle()
        {
            RedMat = assetBundle.LoadAsset<Material>("RedMat");
            BlueMat = assetBundle.LoadAsset<Material>("BlueMat");
            GreenMat = assetBundle.LoadAsset<Material>("GreenMat");
            loadTexture();
            yield return null;
        }
        public static void UnloadAssetBundle()
        {
            if (assetBundle != null)
            {
                assetBundle.Unload(true);
                assetBundle = null;
            }
        }
        private static void loadTexture()
        {
            try
            {
                byte[] Data = ResourceExtractor.ExtractResources("GrindTools.Resources.LoadingTexture.png");
                if (Data != null) ImageConversion.LoadImage(loadingTexture, Data);
               
            }
            catch (Exception ex)  // Catch any exception
            {
                Main.Logger.Log($"An error occurred while loading texture: {ex.Message}");
            }
        }

        private static void OnDestroy()
        {
            UnloadAssetBundle();
        }
    }
}
