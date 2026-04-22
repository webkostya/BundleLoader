using Plugins.Bundles;
using UnityEngine;

namespace Game.Scripts.Handlers
{
    public class SceneHandler : MonoBehaviour
    {
        public static SceneHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public static async void LoadSceneAsync(GameScene scene)
        {
            await BundleLoader.LoadSceneAsync(scene.ToString());
        }
    }
}