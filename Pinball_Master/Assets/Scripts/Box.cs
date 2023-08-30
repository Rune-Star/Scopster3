using System.Collections.Generic;
using GameBoard;
using Utils.Server;
using UnityEngine;
using Utils.UI;

public enum BoxType { A, B, }

public abstract class Block : Creature, ITileObject
{
    [SerializeField] protected BoxType boxType;
    protected internal SpriteRenderer spr;
    public UI_HealthBar healthBar;

    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.sortingOrder = 1;
        var canvas = GetComponentInChildren<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.sortingOrder = 2;

        CurrentHp = health.maxHp;

        healthBar = UI_HealthBar.Create(this, transform.GetChild(0));
        healthBar.SetSize();
        healthBar.Fill(health.maxHp);
    }

    protected override void OnDeath()
    {
        GameManager.Instance.player.RemoveTargetEnemy(this);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet")) {
            TakeDamage(other.gameObject.GetComponent<Bullet>().damage);
            //Destroy(other.gameObject);
        }
    }
}

public class Box : Block { }

public class Boss : Box
{
    //protected override void OnDeath() { GameManager.Instance.player.RemoveRange(1, 10); }
}