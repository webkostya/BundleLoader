using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using System;

namespace Plugins.Bundles
{
    public static class BundleLoader
    {
        public static async Task PreloadSceneAsync(string address, Action<float> onProgress)
        {
            var handle = Addressables.DownloadDependenciesAsync(address);

            while (!handle.IsDone)
            {
                onProgress?.Invoke(handle.PercentComplete);
                await Task.Yield();
            }

            onProgress?.Invoke(1f);

            Addressables.Release(handle);
        }

        public static async Task PreloadScenesAsync(string[] addresses, Action<float> onProgress)
        {
            var progresses = new float[addresses.Length];

            for (var i = 0; i < addresses.Length; i++)
            {
                var index = i;

                await PreloadSceneAsync(addresses[index], progress =>
                {
                    progresses[index] = progress;

                    var total = progresses.Sum();

                    onProgress?.Invoke(total / addresses.Length);
                });
            }
        }

        public static async Task LoadSceneAsync(string address)
        {
            var handle = Addressables.LoadSceneAsync(address);

            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Ошибка загрузки сцены {address}: {handle.OperationException}");
                Addressables.Release(handle);
            }
        }
    }
}