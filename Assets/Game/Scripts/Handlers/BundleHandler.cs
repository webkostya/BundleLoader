using Plugins.Bundles;
using UnityEngine;
using System;

namespace Game.Scripts.Handlers
{
    public class BundleHandler : MonoBehaviour
    {
        public event Action<float> OnProgressLoaded;

        public static BundleHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private async void Start()
        {
            var scenes = Enum.GetNames(typeof(GameScene));

            await BundleLoader.PreloadScenesAsync(scenes, InvokeProgress);

            SceneHandler.LoadSceneAsync(GameScene.Demo1);
        }

        private void InvokeProgress(float progress)
        {
            OnProgressLoaded?.Invoke(Mathf.RoundToInt(progress));
        }
    }
}