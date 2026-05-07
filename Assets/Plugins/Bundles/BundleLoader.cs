using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using UnityEngine;
using System;

namespace Plugins.Bundles
{
    public static class BundleLoader
    {
        public static async Task PreloadScenesAsync(string[] addresses, Action<float> onProgress)
        {
            var sizeHandle = Addressables.GetDownloadSizeAsync(addresses);
            
            await sizeHandle.Task;

            Debug.Log($"Bundles Size: {sizeHandle.Result} bytes");

            var handle = Addressables.DownloadDependenciesAsync(
                addresses,
                Addressables.MergeMode.Union
            );

            while (!handle.IsDone)
            {
                var status = handle.GetDownloadStatus();

                onProgress?.Invoke(status.Percent);

                await Task.Yield();
            }

            onProgress?.Invoke(1f);

            Addressables.Release(handle);
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