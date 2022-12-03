using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace TopDownShooter
{
    public class PoolSystem : MonoBehaviour
    {
        public static PoolSystem Instance;

        [SerializeField] private RiffleBulletProjectile riffleBulletPrefab;
        public IObjectPool<RiffleBulletProjectile> riffleBulletPool;

        [SerializeField] private GameObject bulletDecalPrefab;
        public IObjectPool<GameObject> bulletDecalPool;

        [SerializeField] private BFX_BloodSettings[] bloodPrefabs;
        private int counter = 0;
        public IObjectPool<BFX_BloodSettings> bloodPool;

        private void Awake()
        {
            Instance = this;

            InitRiffleBulletPool();
            InitBulletDecalPool();
            InitBloodFXPools();
        }

        private void InitRiffleBulletPool()
        {
            riffleBulletPool = new ObjectPool<RiffleBulletProjectile>(() =>
            {
                RiffleBulletProjectile projectile = Instantiate<RiffleBulletProjectile>(riffleBulletPrefab, Vector3.zero, Quaternion.identity);
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
            false, 10, 50);
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

        private void InitBloodFXPools()
        {
            bloodPool = new ObjectPool<BFX_BloodSettings>(() =>
            {
                if (counter >= bloodPrefabs.Length - 1)
                {
                    counter = 0;
                }
                counter++;
                return Instantiate<BFX_BloodSettings>(bloodPrefabs[counter], Vector3.zero, Quaternion.identity);
            },
            blood =>
            {
                blood.gameObject.SetActive(true);
            },
            blood =>
            {
                blood.gameObject.SetActive(false);
            },
            blood =>
            {
                Destroy(blood);
            },
            false, 20, 500);
        }
    }
}

