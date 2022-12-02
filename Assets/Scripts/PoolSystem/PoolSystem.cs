using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace TopDownShooter
{
    public class PoolSystem : MonoBehaviour
    {
        public static PoolSystem Instance;

        [SerializeField] private ProjectileStartTransform startTransform;
        [SerializeField] private RiffleBulletProjectile riffleBulletPrefab;
        public IObjectPool<RiffleBulletProjectile> riffleBulletPool;

        [SerializeField] private GameObject bulletDecalPrefab;
        public IObjectPool<GameObject> bulletDecalPool;

        private void Awake()
        {
            Instance = this;

            InitRiffleBulletPool();
            InitBulletDecalPool();
        }

        private void InitRiffleBulletPool()
        {
            riffleBulletPool = new ObjectPool<RiffleBulletProjectile>(() =>
            {
                RiffleBulletProjectile projectile = Instantiate<RiffleBulletProjectile>(riffleBulletPrefab, startTransform.Position, startTransform.Rotation);
                projectile.onHit += () => riffleBulletPool.Release(projectile);
                return projectile;
            },
            projectile =>
            {
                projectile.gameObject.SetActive(true);
            },
            projectile =>
            {
                projectile.gameObject.SetActive(false);
            },
            projectile =>
            {
                Destroy(projectile.gameObject);
            },
            false, 30, 100);
        }

        private void InitBulletDecalPool()
        {
            bulletDecalPool = new ObjectPool<GameObject>(() =>
            {
                GameObject decal = Instantiate(bulletDecalPrefab, Vector3.zero, Quaternion.identity);
                return decal;
            },
            decal =>
            {
                decal.gameObject.SetActive(true);
            },
            decal =>
            {
                decal.gameObject.SetActive(false);
            },
            decal =>
            {
                Destroy(decal);
            },
            false, 60, 3000);
        }
    }
}

