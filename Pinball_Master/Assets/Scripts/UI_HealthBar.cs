using UnityEditor;
using UnityEngine;

namespace Utils.UI
{
    [AddComponentMenu("UI/Utility/" + nameof(UI_HealthBar))]
    public class UI_HealthBar : UI_ProgressBar
    {
        public Creature target;

        new void Start()
        {
            if (target == null) return;

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
        void Init(Creature creature)
        {
            target = creature;
            maxValue = target.health.maxHp;
            target.TakeDamageEvent += Fill;
            Start();
        }

        public static UI_HealthBar Create(Creature target, Transform parent)
        {
            var asset = Resources.Load<UI_HealthBar>("Prefabs/UI_HealthBar");
            var bar = Instantiate(asset, parent);

            bar.Init(target);
            bar.name = "Health Bar";

            return bar;
        }
    }
}