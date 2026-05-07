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

        [SerializeField]
        private float smoothSpeed = 5f;

        private float _selfValue;
        private float _destValue;

        private static BundleHandler BundleHandler => BundleHandler.Instance;

        private void Start()
        {
            BundleHandler.OnProgressLoaded += OnProgressLoaded;
        }

        private void Update()
        {
            _selfValue = Mathf.Lerp(_selfValue, _destValue, Time.deltaTime * smoothSpeed);

            if (_destValue >= 1f && Mathf.Abs(_selfValue - 1f) < 0.001f)
            {
                _selfValue = 1f;
            }

            progress.fillAmount = _selfValue;

            percent.text = $"{Mathf.RoundToInt(_selfValue * 100f)}%";
        }

        private void OnProgressLoaded(float value)
        {
            _destValue = value;
        }

        private void OnDestroy()
        {
            BundleHandler.OnProgressLoaded -= OnProgressLoaded;
        }
    }
}