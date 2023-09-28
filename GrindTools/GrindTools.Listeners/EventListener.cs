using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrindTools.Listeners
{
    public class EventListener : MonoBehaviour
    {
        public int activeSplineCount = 0;
        public void Start()
        {
            CreateSplineListener.OnSplineCreated += CreateSplineListener.IsSplineCreated;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        public void OnDestroy()
        {
            CreateSplineListener.OnSplineCreated -= CreateSplineListener.IsSplineCreated;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != "LoadingScene" || scene.name != "Skate Shop")
            {
                activeSplineCount = 0;
            }
        }
    }
}
