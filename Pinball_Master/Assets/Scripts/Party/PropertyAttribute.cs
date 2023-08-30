[System.Serializable]
public class PropertyAttribute
{
    public float movementSpeed;

    public float maxHealth;
    public float bonusAttackDamage;
    public float bonusAttackSpeed;

    public void Modify(Creature.Stat stat)
    {
        stat.attackDamage = stat.attackDamage + bonusAttackDamage;
        stat.attackSpeed = stat.attackSpeed * (1 + bonusAttackSpeed);
    }
}