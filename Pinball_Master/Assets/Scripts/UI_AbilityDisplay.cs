using System;
using System.Text;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Utils.UI;

namespace Utils.Ability_System
{
    public class UI_AbilityDisplay : UI_Behaviour
    {
        [Header("UI")][SerializeField] TextMeshProUGUI title_text;
        [SerializeField] TextMeshProUGUI info_text;
        UI_Button button;

        AbilityTree.TreeNode treeNode;
        StringBuilder stringBuilder;

        public Action<AbilityTree> OnClicked { get; private set; }
        new void Awake() { button = GetComponentInChildren<UI_Button>(); }

        void OnEnable()
        {
            OnClicked += tree =>
            {
                if (!treeNode.unlocked) {
                    tree.UnlockSkill(treeNode);
                    if (treeNode.IsCompletedLastLevel) {
                        button.SetActive(false);
                        SetText();
                    } else if (treeNode.unlocked) {
                        button.SetButtonText("Upgrade");
                        SetText();
                    }
                } else if (!treeNode.IsCompletedLastLevel) {
                    tree.UpgradeAbility(treeNode);
                    if (treeNode.IsCompletedLastLevel) button.SetActive(false);
                    SetText();
                }
            };
        }

        public void Init([NotNull] AbilityTree.TreeNode treeNode)
        {
            this.treeNode = treeNode;

            button.RemoveAllListeners();
            button.AddListener(() =>
            {
                OnClicked?.Invoke(UIManager.Instance.abilityTreeUI!.AbilityTree);
                //onClicked?.Invoke(this.treeNode);
            });
            button.SetActive(true);

            SetText();
        }

        void SetText()
        {
            title_text.text
                = $"{treeNode.ability.name}<size=24>[Level{treeNode.Level.ToString()}/{treeNode.MaxLevel.ToString()}]</size>";
            info_text.text = treeNode.ToString();
        }
    }
}