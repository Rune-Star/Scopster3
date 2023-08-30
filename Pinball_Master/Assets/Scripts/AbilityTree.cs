using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Utils.UI;

namespace Utils.Ability_System
{
    public class AbilityTree : MonoBehaviour
    {
        public class TreeNode
        {
            public Ability ability;
            public bool unlocked;
            public (TreeNode node, int requireLevel) requireNode;

            public Func<bool> requireCondition =>
                () => requireNode.node == null || requireNode.node.Level >= requireNode.requireLevel;

            public int Level { get => ability.level; set => ability.level = value; }
            public int MaxLevel { get => ability.maxLevel; }
            public bool IsCompletedLastLevel { get => Level == MaxLevel; }
            public int RequirePoint { get => ability.requirePoint[ability.level]; }
            public TreeNode Parent { get; set; }
            public List<TreeNode> Children { get; set; } = new List<TreeNode>();

            public TreeNode() { }
            public TreeNode(Ability ability) { this.ability = ability; }

            public override string ToString()
            {
                var stringBuilder = new StringBuilder().AppendLine(ability.desc);
                if (!IsCompletedLastLevel)
                    stringBuilder.AppendLine($"Point: [{RequirePoint.ToString()}]");

                if (requireNode.node != null && !unlocked)
                    stringBuilder.AppendLine(
                        $"Require: [Level {requireNode.requireLevel.ToString()}] {requireNode.node.ability.name}");

                return stringBuilder.ToString();
            }
        }

        public List<TreeNode> AbilityNodes { get; } = new List<TreeNode>();
        public int Height { get; private set; }

        public int AbilityPoint { get; private set; } = 10000; //
        int ConsumedPoint { get; set; }

        public void Init(TreeNode root)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);
            AbilityNodes.Add(root);

            while (stack.Count > 0) {
                var curNode = stack.Pop();
                foreach (var node in curNode.Children) {
                    AbilityNodes.Add(node);
                    node.Parent = curNode;
                    print(curNode.ability.name);
                    stack.Push(node);
                }
            }

            Height = GetHeight(root);
        }

        int GetHeight(TreeNode root)
        {
            int h = 0;

            foreach (var child in root.Children) {
                int newHeight = GetHeight(child) + 1;
                if (h < newHeight) h = newHeight;
            }

            return h;
        }

        [Conditional("UNITY_EDITOR")]
        public void PrintTree()
        {
            foreach (var node in AbilityNodes) {
                print(node.ability.name);
            }
        }

        void UsePoint(int value)
        {
            AbilityPoint -= value;
            ConsumedPoint += value;
        }
        void ResetPoint()
        {
            AbilityPoint += ConsumedPoint;
            ConsumedPoint = 0;
        }

        bool CanUnlockSkill(TreeNode treeNode)
        {
            if (treeNode.requireCondition != null && !treeNode.requireCondition()) {
                print($"Can't unlock {treeNode.ability.name} yet" +
                      $"Unlock {treeNode.requireCondition} first!");
                return false;
            } else if (AbilityPoint < treeNode.RequirePoint) {
                print(
                    $"You're not have enough point, this required {treeNode.RequirePoint.ToString()}");
                return false;
            }

            return true;
        }
        public void UnlockSkill(TreeNode treeNode)
        {
            if (CanUnlockSkill(treeNode)) {
                UsePoint(treeNode.RequirePoint);
                treeNode.unlocked = true;
                treeNode.Level = 1;
                treeNode.ability.OnLevelUp();

                print($"{treeNode.ability.name} Unlocked!");
            }
        }
        bool CanUpgradeAbility(TreeNode treeNode)
        {
            if (AbilityPoint < treeNode.RequirePoint ||
                treeNode.IsCompletedLastLevel) {
                print($"Can't Upgrade {treeNode.ability.name}");
                return false;
            }

            return true;
        }
        public void UpgradeAbility(TreeNode treeNode)
        {
            if (CanUpgradeAbility(treeNode)) {
                UsePoint(treeNode.RequirePoint);
                treeNode.Level++;
                treeNode.ability.OnLevelUp();
            }
        }

        void AnyUnlockAndUpgradeAbility(TreeNode root)
        {
            int flag = 0;
            flag += CanUnlockSkill(root) ? root.RequirePoint : 0;

            while (flag < AbilityPoint)
            {
                bool unlockedOrUpgraded = false;

                AnyUnlockAndUpgradeSubtree(root, ref flag, ref unlockedOrUpgraded);

                if (!unlockedOrUpgraded)
                    break;
            }

            print($"Consumed {flag} Points");
        }

        void AnyUnlockAndUpgradeSubtree(TreeNode node, ref int flag, ref bool unlockedOrUpgraded)
        {
            if (CanUnlockSkill(node))
            {
                int unlockCost = node.RequirePoint;
                if (flag + unlockCost <= AbilityPoint)
                {
                    flag += unlockCost;
                    UIManager.Instance.abilityTreeUI.Invoke(node);
                    unlockedOrUpgraded = true;
                }
            }

            if (!node.IsCompletedLastLevel)
            {
                for (int level = node.Level + 1; level <= node.MaxLevel; level++)
                {
                    if (CanUpgradeAbility(node))
                    {
                        int upgradeCost = node.ability.requirePoint[level - 1];
                        if (flag + upgradeCost <= AbilityPoint)
                        {
                            flag += upgradeCost;
                            UIManager.Instance.abilityTreeUI.Invoke(node);
                            unlockedOrUpgraded = true;
                        }
                    }
                }
            }

            foreach (var childNode in node.Children)
            {
                AnyUnlockAndUpgradeSubtree(childNode, ref flag, ref unlockedOrUpgraded);
            }
        }


        public void A() { AnyUnlockAndUpgradeAbility(AbilityNodes[0]); }

        public void ResetAllAbility()
        {
            foreach (var node in AbilityNodes) {
                node.unlocked = false;
                node.Level = 0;
            }

            ResetPoint();
            UIManager.Instance.abilityTreeUI.ResetAllDisplay();
        }

        // UI_AbilityTree UISkillTree;
        // void Awake()
        // {
        //     UISkillTree.OnButtonClicked += UnlockSkill;
        // }
    }
}