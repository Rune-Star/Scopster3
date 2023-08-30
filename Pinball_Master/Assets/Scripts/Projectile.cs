using System;
using UnityEngine;

namespace UnityUtils.Projectile
{
    [Serializable]
    public class ProjectilePrefabs
    {
        public GameObject bulletPrefab;
        public GameObject group;
    }

    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour
    {
        protected Rigidbody2D _rigid;
        public Rigidbody2D Rigid => _rigid;

        [SerializeField] public float speed;

        void Awake() { _rigid = GetComponent<Rigidbody2D>(); }

        protected void Fire() { }

        [Obsolete]
        public static Projectile Create(Transform parent)
        {
            return Instantiate(GameManager.Instance.projectilePrefabs.bulletPrefab, parent)
                .GetComponent<Projectile>();
        }
    }
}