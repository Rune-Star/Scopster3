using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utils
{
    public class InGameUI : MonoBehaviour
    {
        public SceneAsset _sceneAsset;
        Button _button;

        void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(GoHome);
        }

        public void GoHome()
        {
            SceneLoadManager.LoadScene("SampleScene");
        }
    }
}