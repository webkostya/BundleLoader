using Game.Scripts.Handlers;
using UnityEngine.UI;
using UnityEngine;

namespace Game.Scripts
{
    public class Progress : MonoBehaviour
    {
        [SerializeField]
        private Image progress;

        [SerializeField]
        private Text percent;

        private static BundleHandler BundleHandler => BundleHandler.Instance;

        private void Start()
        {
            BundleHandler.OnProgressLoaded += OnProgressLoaded;
        }

        private void OnProgressLoaded(float value)
        {
            progress.fillAmount = value;
            percent.text = $"{value * 100f}%";
        }

        private void OnDestroy()
        {
            BundleHandler.OnProgressLoaded -= OnProgressLoaded;
        }
    }
}