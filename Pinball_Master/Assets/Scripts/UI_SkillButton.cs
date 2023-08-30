using System.Collections;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Utils.Ability_System;
using Utils.UI;

namespace Utils.Ability_System
{
    [AddComponentMenu("Skill_System/UI/" + nameof(UI_SkillButton))]
    public class UI_SkillButton : UI_Button
    {
        public SkillData skillData;
        public Skill skill;

        Image iconImage;
        Image coolDownImage;

        float coolDownTime;
        float curCoolDown = 0;

        Player player;

        [SerializeField] TMP_Text actionText;

        void Start()
        {
            skill.Init(skillData);

            //iconImage = GetComponent<Image>();
            iconImage = transform.GetChild(0).GetComponent<Image>();
            coolDownImage = transform.GetChild(1).GetComponent<Image>();

            iconImage.sprite = skill.icon;
            coolDownImage.sprite = GetComponent<Image>().sprite;

            coolDownImage.fillAmount = 0;

            player = GameManager.Instance.player;
            AddListener(OnClicked);
        }

        void OnClicked()
        {
            bool canUseSkill;
            if (!TryUseSkill(out canUseSkill)) {
                StringBuilder stringBuilder = new StringBuilder()
                    .Append($"<color=yellow>[{skill.name}]</color>")
                    .Append("<color=#37CFC9> Skill cooldown remaining:</color>")
                    .Append($"<color=green> {curCoolDown:F1}</color>")
                    .Append("<color=#37CFC9> seconds.</color>");

                SetDisplayText(stringBuilder.ToString());

                return;
            }

            player.UseSkill(skill);

            StartCoroutine(nameof(StartCooldown));
        }

        bool TryUseSkill(out bool value)
        {
            if (curCoolDown > 0) {
                value = false;
                return false;
            }

            value = true;
            return true;
        }

        IEnumerator StartCooldown()
        {
            coolDownTime = skill.cooltime;
            curCoolDown = coolDownTime;
            coolDownImage.fillAmount = 1f;

            while (curCoolDown > 0) {
                yield return null;

                curCoolDown -= Time.deltaTime;
                coolDownImage.fillAmount = curCoolDown / coolDownTime;

                if (Mathf.Approximately(curCoolDown, 0)) {
                    coolDownImage.fillAmount = 0f;
                    SetDisplayText($"Skill Cooldown ended.");
                    break;
                }
            }
        }

        void SetDisplayText(string text) => actionText.text = text;

        static UI_SkillButton Create(SkillData skillData, Transform parent)
        {
            var asset = Resources.Load<UI_SkillButton>("Prefabs/UI_SkillButton");
            var obj = Instantiate(asset, parent);

            obj.skill.Init(skillData);
            return obj;
        }

        #region InputAction

#if !UNITY_EDITOR
        void ESkill(InputAction.CallbackContext callback)
        {
            if (!callback.performed)
                return;
            OnClicked();
        }

        void QSkill(InputAction.CallbackContext callback)
        {
            if (!callback.performed)
                return;

            OnClicked();
        }

#endif

        #endregion
    }
}