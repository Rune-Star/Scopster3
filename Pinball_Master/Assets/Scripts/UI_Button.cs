using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Utils.UI
{
    [AddComponentMenu("UI/Utils/" + nameof(UI_Button))]
    public class UI_Button : UI_Behaviour
    {
        Button button;
        TextMeshProUGUI textUI;

        public OnClick onClick;

        new void Awake()
        {
            base.Awake();
            button = GetComponent<Button>();
            textUI = GetComponentInChildren<TextMeshProUGUI>();
            onClick.RemoveAllListeners();
        }

        public void AddListener(UnityAction call) { button.onClick.AddListener(call); }
        public void RemoveListener(UnityAction call) { button.onClick.RemoveListener(call); }
        public void RemoveAllListeners() { button.onClick.RemoveAllListeners(); }
        public void SetButtonText(string text) { this.textUI.text = text; }
        public void SetButtonInteractable(bool condition) { button.interactable = condition; }
        public void ToggleButtonInteractable() { button.interactable = !button.interactable; }

        [System.Serializable]
        public class OnClick : UnityEvent { }
    }
}