using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Utils.Ability_System
{
    public class UI_AbilityTree : MonoBehaviour
    {
        [SerializeField] AbilityTree ability_tree;
        Dictionary<AbilityTree.TreeNode, UI_AbilityDisplay> abilityDisplays = new();

        [SerializeField] TextMeshProUGUI abilityPoint_text;
        public AbilityTree AbilityTree => ability_tree;
        void Awake()
        {
            #region Initializer

            AbilityTree.TreeNode root = new AbilityTree.TreeNode(new StatAbility
                { name = "Root", maxLevel = 1, requirePoint = new[] { 10 } });

            AbilityTree.TreeNode child0 = new AbilityTree.TreeNode
            {
                ability = new StatAbility
                    { name = "Attack", maxLevel = 5, requirePoint = new[] { 2, 2, 2, 2, 2 } }
            };

            child0.Children.Add(new AbilityTree.TreeNode()
            {
                ability = new StatAbility { name = "Damage", maxLevel = 3, requirePoint = new[] { 5, 5, 5 } }
            });
            child0.Children.Add(new AbilityTree.TreeNode()
            {
                ability = new StatAbility() { name = "Speed", maxLevel = 3, requirePoint = new[] { 5, 5, 5 } }
            });
            child0.Children.Add(new AbilityTree.TreeNode()
            {
                ability = new StatAbility() { name = "Reach", maxLevel = 3, requirePoint = new[] { 5, 5, 5 } }
            });
            root.Children.Add(child0);

            AbilityTree.TreeNode child1 = new AbilityTree.TreeNode
            {
                ability = new StatAbility
                    { name = "DEF", maxLevel = 5, requirePoint = new[] { 2, 2, 2, 2, 2 } }
            };
            child1.Children.Add(new AbilityTree.TreeNode()
            {
                ability = new StatAbility() { name = "Defense", maxLevel = 3, requirePoint = new[] { 5, 5, 5 } }
            });
            child1.Children.Add(new AbilityTree.TreeNode()
            {
                ability = new StatAbility() { name = "Resistance", maxLevel = 3, requirePoint = new[] { 5, 5, 5 } }
            });
            child1.Children.Add(new AbilityTree.TreeNode()
            {
                ability = new StatAbility() { name = "Recovery", maxLevel = 3, requirePoint = new[] { 5, 5, 5 } }
            });
            root.Children.Add(child1);

            AbilityTree.TreeNode child2 = new AbilityTree.TreeNode
            {
                ability = new StatAbility
                    { name = "Support Type", maxLevel = 5, requirePoint = new[] { 2, 2, 2, 2, 2 } }
            };
            child2.Children.Add(new AbilityTree.TreeNode()
            {
                ability = new StatAbility() { name = "Buff", maxLevel = 3, requirePoint = new[] { 5, 5, 5 } }
            });
            child2.Children.Add(new AbilityTree.TreeNode()
            {
                ability = new StatAbility() { name = "Heal", maxLevel = 3, requirePoint = new[] { 5, 5, 5 } }
            });
            child2.Children.Add(new AbilityTree.TreeNode()
            {
                ability = new StatAbility() { name = "Summon", maxLevel = 3, requirePoint = new[] { 5, 5, 5 } }
            });
            root.Children.Add(child2);

            ability_tree.Init(root);

            #endregion

            root.requireNode = (null, 0);
            child0.requireNode = (child0.Parent, 1);
            child0.Children[0].requireNode = (child0, 1);
            child0.Children[1].requireNode = (child0, 2);
            child0.Children[2].requireNode = (child0, 3);

            child1.requireNode = (child1.Parent, 1);
            child1.Children[0].requireNode = (child1, 1);
            child1.Children[1].requireNode = (child1, 2);
            child1.Children[2].requireNode = (child1, 3);

            child2.requireNode = (child2.Parent, 1);
            child2.Children[0].requireNode = (child2, 1);
            child2.Children[1].requireNode = (child2, 2);
            child2.Children[2].requireNode = (child2, 3);

            int i = 0;
            var displays = GetComponentsInChildren<UI_AbilityDisplay>();
            for (var index = 0; index < ability_tree.AbilityNodes.Count; index++) {
                var key = ability_tree.AbilityNodes[index];
                abilityDisplays.Add(key, displays[index]);
                abilityDisplays[key].Init(ability_tree.AbilityNodes[i++]);
            }
        }

        void Update() { abilityPoint_text.text = ability_tree.AbilityPoint.ToString(); }

        void OnEnable()
        {
            //OnButtonClicked += skillTree.UnlockSkill;
        }

        public event Action<AbilityTree.TreeNode> OnButtonClicked;
        void OnButtonClickedEvent(AbilityTree.TreeNode obj) { OnButtonClicked?.Invoke(obj); }

        public void Invoke(AbilityTree.TreeNode treeNode)
        {
            foreach (var v in abilityDisplays) {
                v.Value.OnClicked.Invoke(ability_tree);
            }
        }
        public void InvokeAll()
        {
            foreach (var display in abilityDisplays) {
                display.Value.OnClicked.Invoke(ability_tree);
            }
        }

        public void ResetAllDisplay()
        {
            int i = 0;
            foreach (var display in abilityDisplays) {
                display.Value.Init(ability_tree.AbilityNodes[i++]);
            }
        }
    }
}