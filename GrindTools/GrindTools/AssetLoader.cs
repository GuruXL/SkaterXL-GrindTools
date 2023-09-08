using System.Reflection;
using UnityEngine;
using System.IO;
using System.Collections;
using System;
using ModIO.UI;

namespace GrindTools
{
    public static class AssetLoader
    {
        public static Material RedMat;
        public static Material BlueMat;
        public static Material GreenMat;
        public static AssetBundle assetBundle;

        public static void LoadBundles()
        {
            // Check if a type from the Unity assembly has been loaded
            Type unityObjectType = Type.GetType("UnityEngine.Object, UnityEngine");

            if (unityObjectType != null)
            {
                PlayerController.Instance.StartCoroutine(LoadAssetBundle());     
            }
        }   
        private static byte[] ExtractResources(string filename)
        {
            using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename))
            {
                if (manifestResourceStream == null)
                    return null;

                byte[] buffer = new byte[manifestResourceStream.Length];
                manifestResourceStream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }
        private static IEnumerator LoadAssetBundle()
        {
            byte[] assetBundleData = ExtractResources("GrindTools.Resources.mats");
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
            yield return PlayerController.Instance.StartCoroutine(LoadPrefabs());
        }
        private static IEnumerator LoadPrefabs()
        {
            if (assetBundle == null)
            {
                MessageSystem.QueueMessage(MessageDisplayData.Type.Error, $"GrindTools Asset bundles are not loaded!", 2.5f);
                yield break;
            }
            yield return PlayerController.Instance.StartCoroutine(LoadAssetFromBundle());
        }
        private static IEnumerator LoadAssetFromBundle()
        {
            RedMat = assetBundle.LoadAsset<Material>("RedMat");
            BlueMat = assetBundle.LoadAsset<Material>("BlueMat");
            GreenMat = assetBundle.LoadAsset<Material>("GreenMat");
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
        private static void OnDestroy()
        {
            UnloadAssetBundle();
        }
    }
}
