using UnityEngine;

namespace Utils.Ability_System
{
    [System.Serializable]
    public abstract class Ability
    {
        public string name;
        public string desc;
        public int level;
        public int maxLevel;
        public int[] requirePoint;

        public abstract void OnLevelUp();
    }

    [System.Serializable]
    public class StatAbility : Ability
    {
        public PropertyAttribute attribute;

        public StatAbility()
        {
            attribute = new PropertyAttribute
            {
                bonusAttackDamage = 12,
                bonusAttackSpeed = 12
            };
        }

        public override void OnLevelUp()
        {
            GameManager.Instance.player.Modify(attribute);//
        }
    }
}