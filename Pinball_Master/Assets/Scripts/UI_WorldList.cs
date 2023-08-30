using System;
using UnityEngine;
using UI;

namespace Utils.UI
{
    public class UI_WorldList : MonoBehaviour
    {
        [SerializeField] GameObject canvas;

        [SerializeField] UI_WorldDisplay prefab;
        [SerializeField] GameObject content;

        //List<WorldDisplay> _worldDisplayList = new();

        UI_WorldDisplay _selectedDisplay;
        public Action<UI_WorldDisplay, int> onSelectedWorld;

        int i;

        void Start() { Draw(); }

        void OnEnable() =>
            onSelectedWorld += (display, i) =>
            {
                _selectedDisplay = display;

                GameManager.Instance.SetSelectedWorld(GameManager.Instance.playerWorldDataList._playerWorldData[i]);
                UIManager.Instance.mainUI.Init();
                TogglePanel();
            };

        void OnDisable() { onSelectedWorld = null; }

        public void Draw()
        {
            for (var index = 0; index < GameManager.Instance.worldDataList.Count; index++) {
                var worldDisplay = CreateDisplay(GameManager.Instance.worldDataList[index]);
                //_worldDisplayList.Add(worldDisplay);
                worldDisplay.SetIndex(index);
            }
        }

        UI_WorldDisplay CreateDisplay(WorldData data)
        {
            var worldDisplay = Instantiate(prefab, Vector3.zero, Quaternion.identity, content.transform);
            worldDisplay.SetIndex(i++);
            worldDisplay.Init(data);

            return worldDisplay;
        }
        public void SetLayoutVertical() { }

        public void TogglePanel()
        {
            canvas.SetActive(!canvas.activeSelf);
            // if (_selectedDisplay != null) {
            //     var rect0 = _selectedDisplay.GetComponent<RectTransform>();
            //     var rect = content.GetComponent<RectTransform>();
            //     var pos = rect.anchoredPosition;
            //
            //     var a = Screen.height / rect0.localPosition.y * rect.rect.height;
            //     rect.anchoredPosition = new Vector2(pos.x, -a);
            //     Debug.Log(-a);
            // }

            UIManager.Instance.ToggelMainUI();
        }
    }
}