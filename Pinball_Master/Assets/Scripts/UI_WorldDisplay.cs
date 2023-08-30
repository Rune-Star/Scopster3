using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UI;
using Utils.UI;

namespace Utils
{
    public class UI_WorldDisplay : MonoBehaviour
    {
        int index;

        [SerializeField] TextMeshProUGUI worldName_text;
        [SerializeField] TextMeshProUGUI worldNumber_text;
        [SerializeField] TextMeshProUGUI stageCount_text;

        Button _button;

        void Awake() { _button = GetComponent<Button>(); }

        public void Init(WorldData data)
        {
            worldName_text.text = data.worldName;
            worldNumber_text.text = $"World {(index + 1).ToString()}";
            stageCount_text.text = $"{data.stageCount.ToString()} Stage";

            _button.onClick.AddListener(() => { UIManager.Instance.worldListUI.onSelectedWorld?.Invoke(this, index); });
        }
        public void SetIndex(int newIndex) { index = newIndex; }
    }
}