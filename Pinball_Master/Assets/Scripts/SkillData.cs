using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "SkillData", order = 0)]
public class SkillData : ScriptableObject
{
    public Sprite icon;
    public float damage;
    public int count;
    public float cooltime;
}