using System;
using System.Collections;
using System.Globalization;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Utils.UI
{
    [AddComponentMenu("UI/Utility/" + nameof(UI_ProgressBar))]
    public class UI_ProgressBar : UI_Behaviour
    {
        [SerializeField] protected RectTransform fillRect;
        [SerializeField] protected TextMeshProUGUI textObj;

        [SerializeField] float value;
        public float Value => value;

        [SerializeField] protected float maxValue;

        protected enum InitState { None, FadeIn, FadeOut };

        [SerializeField] protected InitState initState;

        float _curTime;
        [SerializeField] protected float lerpTerm = 2f;

        float _startValue;
        float _endValue;

        protected void Start()
        {
            if (!(maxValue > 0)) {
                maxValue = RectTransform.rect.width;
            }

            switch (initState) {
                case InitState.None:
                    break;
                case InitState.FadeIn:
                    fillRect.sizeDelta = new Vector2(0, 0);
                    Fill(maxValue);
                    break;
                case InitState.FadeOut:
                    fillRect.sizeDelta = new Vector2(RectTransform.rect.width, 0);
                    Fill(0);
                    break;
            }
        }

        IEnumerator CO_()
        {
            _curTime = 0;

            while (_curTime < lerpTerm) {
                _curTime += Time.deltaTime;
                var t = Mathf.Clamp01(_curTime / lerpTerm);

                var rectValue = Mathf.Lerp(_startValue, _endValue, t);
                fillRect.sizeDelta = new Vector2(rectValue, 0);
                // fillRect.sizeDelta = new Vector2(_endValue, 0);
                // fillRect.DOSizeDelta(new Vector2(_endValue, 0), lerpTerm);

                value = fillRect.sizeDelta.x * maxValue / RectTransform.rect.width;
                //value = Mathf.Lerp(startValue, endValue, t);
                if (textObj != null)
                    textObj.text = ((int) value).ToString();

                yield return null;
            }
        }

        /// <summary>
        /// <para> BarUI.Fill </para>
        /// </summary>
        /// <param name="fillValue"> fill</param>
        public virtual void Fill(float fillValue)
        {
            if (fillValue < 0) {
                fillValue = 0;
            }

            _startValue = fillRect.sizeDelta.x;
            _endValue = RectTransform.rect.width / maxValue * fillValue;

            //StopAllCoroutines();
            StartCoroutine(nameof(CO_));
        }
    }
}