using UnityEngine;
using Utils.Ability_System;

namespace Utils.UI
{
    public class UIManager : MonoBehaviour
    {
        public UI_WorldList worldListUI;

        public GameObject mainCanvas;
        [SerializeField] GameObject stageListUI;

        public MainUI mainUI;

        public UI_AbilityTree abilityTreeUI;
        public static UIManager Instance { get; private set; }

        void Awake() { Instance = this; }

        void Start() { mainUI?.Init(); }

        public void ToggleStageListUI() { worldListUI.TogglePanel(); }
        public void ToggelMainUI() { mainCanvas.SetActive(!mainCanvas.activeSelf); }
    }

    /// <summary>
    ///     모든 UI 오브젝트가 상속받는 클래스
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasRenderer))]
    public abstract class UI_Behaviour : MonoBehaviour
    {
        public RectTransform RectTransform { get; private set; }

        protected void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public void SetActive(bool value) { gameObject.SetActive(value); }

        public void SetSize()
        {
            RectTransform.pivot = new Vector2(0.5f, 0);
            RectTransform.anchorMin = new Vector2(0, 0);
            RectTransform.anchorMax = new Vector2(1, 0);
            RectTransform.localPosition = new Vector3(0, -0.5f, 0);
            RectTransform.sizeDelta = new Vector2(0, 0.2f);
        }
    }
}