using UnityEngine;

namespace Utils.Ability_System
{
    [System.Serializable]
    public class Skill
    {
        public string name;
        public Sprite icon;
        public float damage;
        public int count;
        public float cooltime;

        public void Init(SkillData skillData)
        {
            this.name = skillData.name;
            this.icon = skillData.icon;
            this.damage = skillData.damage;
            this.count = skillData.count;
            this.cooltime = skillData.cooltime;
        }
    }
}