using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils.Ability_System;
using Random = UnityEngine.Random;

public abstract class Creature : MonoBehaviour
{
    #region VARIABLE_CLASS

    [Serializable]
    public class Health
    {
        public float curHp;
        public float baseHp;
        public float maxHp;

        public void Calc(float damage, Stat stat) { curHp -= damage * stat.defense; }
    }

    [Serializable]
    public class Stat
    {
        public float attackDamage;
        public float attackSpeed;
        
        public float strength;
        public float defense;
        public float immunity;
    }

    [Serializable]
    public class StatAmp
    {
        public float strAmp = 10;
        public float defAmp = 10;
    }

    public Health health;
    public Stat stat;

    public StatAmp statAmp = new StatAmp
    {
        defAmp = 0.0002f
    };

    public float CurrentHp
    {
        get => health.curHp;
        set
        {
            if (value <= 0) {
                Invoke(nameof(OnDeath), GameManager.Instance.deathTerm);
                //OnDeath();
            }

            health.curHp = value;
        }
    }

    [SerializeField] protected float damage;

    #endregion

    #region EVENT

    public event Action<float> TakeDamageEvent;
    public event Action DeathEvent;
    void OnEnable() { DeathEvent += OnDeath; }
    void OnDisable() { DeathEvent -= OnDeath; }
    public virtual void OnTakeDamageEvent(float damage) { TakeDamageEvent?.Invoke(damage); }
    protected virtual void OnDeathEvent() { DeathEvent?.Invoke(); }

    #endregion

    protected const float INVULNERABLE_TERM = 2f;
    protected abstract void OnDeath();
    public virtual void TakeDamage(float damage)
    {
        var hp = CurrentHp - damage * (1 - Mathf.Clamp(stat.defense * statAmp.defAmp, 0, 0.35f));
        CurrentHp = hp;
        // StartCoroutine(CO_SetHp(hp));
        OnTakeDamageEvent(hp);
    }

    protected bool TryCheckDamage(out bool a)
    {
        a = false;
        if (a) { }

        return a;
    }

    IEnumerator CO_SetHp(float hp)
    {
        float curTime = 0;

        var invulnerableTerm = 0.5f;
        if (hp <= 0) invulnerableTerm = Mathf.Epsilon;

        while (curTime < invulnerableTerm) {
            curTime += Time.deltaTime;

            var t = Mathf.Clamp01(curTime / invulnerableTerm);
            CurrentHp = Mathf.Lerp(CurrentHp, hp, t);

            yield return null;
        }
    }
}
public partial class Player : Creature
{ 
    public List<Creature> targetEnemyList;
    public List<Creature> TargetEnemyList => targetEnemyList;
    public int count;

    public void AddTargetEnemy(Creature enemy)
    {
        targetEnemyList.Add(enemy);
    }
    public void RemoveTargetEnemy(Creature enemy)
    {
        targetEnemyList.Remove(enemy);
    }

    public void OnSkill(float damage, int count)
    {
        if (GameManager.Instance.player == null || targetEnemyList.Count <= 0) {
            return;
        }

        int[] spawnTileList = new int[count];
        Creature[] enemys;

        if (count < targetEnemyList.Count) {
            int randomIndex;
            enemys = new Creature[count];

            for (int i = 0; i < count;) {
                InfiniteLoopDetector.Run();

                randomIndex = Random.Range(0, targetEnemyList.Count);

                if (spawnTileList.Contains(randomIndex)) {
                    randomIndex = Random.Range(0, targetEnemyList.Count);
                } else {
                    spawnTileList[i] = randomIndex;
                    enemys[i] = targetEnemyList[randomIndex];
                    i++;
                }
            }
        } else {
            enemys = targetEnemyList.ToArray();
        }

        #region MyRegion

#if !UNITY_EDITOR
        for (var index = 0; index < spawnTileList.Capacity; index++) {
            var enemy = targetEnemyList[spawnTileList[index]];
            enemy.TakeDamage(damage);

            
            if (enemy == null) {
                spawnTileList.RemoveAt(index);

                for (int i = 0; i < --count;) {
                    randomTile = Random.Range(0, targetEnemyList.Count);

                    if (spawnTileList.Contains(randomTile)) {
                        randomTile = Random.Range(0, targetEnemyList.Count);
                    } else {
                        spawnTileList.Add(randomTile);
                        i++;
                    }
                }
            }
        }
#endif

        #endregion

        foreach (var enemy in enemys) {
            enemy.TakeDamage(damage);
        }
    }
    protected override void OnDeath() { }
    
    public void UseSkill(Skill skill)
    {
        OnSkill(skill.damage, skill.count);
    }
}