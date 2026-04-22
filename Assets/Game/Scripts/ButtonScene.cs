using Game.Scripts.Handlers;
using UnityEngine.UI;
using UnityEngine;

namespace Game.Scripts
{
    public class ButtonScene : MonoBehaviour
    {
        [SerializeField]
        private GameScene gameScene;

        private void Awake()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(InvokeListener);
        }

        private void InvokeListener()
        {
            SceneHandler.LoadSceneAsync(gameScene);
        }
    }
}