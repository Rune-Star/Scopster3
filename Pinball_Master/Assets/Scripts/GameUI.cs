using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utils
{
    public class GameUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] Scrollbar scrollbar;

        const int SIZE = 5;
        float[] pos = new float[SIZE];
        float distance;
        float curPos;
        [SerializeField] float lerpTime;
        [SerializeField] GameObject buttonGroup;

        bool isDrag;

        void Awake()
        {
            InitButton();
        }

        void Start()
        {
            distance = 1f / (SIZE - 1);

            for (int i = 0; i < SIZE; i++) {
                pos[i] = distance * i;
            }
            
            scrollbar.value = pos[2];
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Begin Drag");
            curPos = GetPos();
        }
        public void OnDrag(PointerEventData eventData) => isDrag = false;
        public void OnEndDrag(PointerEventData eventData)
        {
            isDrag = false;
            var targetPos = GetPos();

            // if (curPos == GetPos()) {
            //     if (eventData.delta.x > 18 && curPos - distance >= 0) {
            //         targetPos = curPos - distance;
            //     }else if (eventData.delta.x < -18 && curPos + distance <= 1.01f) {
            //         targetPos = curPos + distance;
            //     }
            // }

            StartCoroutine(SmoothMove(targetPos));
        }

        float GetPos()
        {
            for (int i = 0; i < SIZE; i++) {
                if (scrollbar.value < pos[i] + distance * .5f && scrollbar.value > pos[i] - distance * .5f) {
                    return pos[i];
                }
            }

            return scrollbar.value;
        }

        Button[] buttons;
        IEnumerator SmoothMove(float endPos)
        {
            if (isDrag)
                yield break;

            var curTime = 0f;
            var startPos = scrollbar.value;

            while (curTime < lerpTime) {
                curTime += Time.deltaTime;
                var t = Mathf.Clamp01(curTime / lerpTime);

                scrollbar.value = Mathf.Lerp(startPos, endPos, t);

                startPos = scrollbar.value;

                yield return null;
            }
        }
        void InitButton()
        {
            buttons = buttonGroup.GetComponentsInChildren<Button>();

            for (int i = 0; i < buttons.Length; i++) {
                int i1 = i;
                buttons[i].onClick.AddListener(() => StartCoroutine(SmoothMove(pos[i1])));
                //buttons[i].onClick.AddListener(() => StartCoroutine(SmoothMove(pos[i])));
            }
        }
    }
}