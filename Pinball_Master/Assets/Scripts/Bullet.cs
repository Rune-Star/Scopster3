using UnityEngine;
using UnityUtils.Projectile;


public class Bullet : Projectile
{
    public float damage;

    void Start()
    {
        //_rigid.MovePosition(Vector2.one * speed);
        Invoke(nameof(OnDead), 10);
    }

    public void OnDead() { Destroy(gameObject); }

    public static Projectile Create(Transform parent, Vector3 pos, Quaternion rotation)
    {
        var obj = Instantiate(GameManager.Instance.projectilePrefabs.bulletPrefab, pos, rotation)
            .GetComponent<Projectile>();
        obj.transform.SetParent(parent);

        return obj;
    }

    //void Update() { transform.Translate(Vector3.up * speed * Time.deltaTime); }
}