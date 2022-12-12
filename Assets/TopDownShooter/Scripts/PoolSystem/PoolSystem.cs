using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static BFX_BloodSettings;
using Random = UnityEngine.Random;

namespace TopDownShooter
{
    public class PoolSystem : MonoBehaviour
    {
        public static PoolSystem Instance;

        //Riffle Bullets pool
        [SerializeField] private RiffleBulletProjectile riffleBulletPrefab;
        public IObjectPool<RiffleBulletProjectile> RiffleBulletPool;


        //Bullet decal pool
        [SerializeField] private GameObject bulletDecalPrefab;
        public IObjectPool<GameObject> BulletDecalPool;


        //Blood VFX/Decals pool
        [SerializeField] private BFX_BloodSettings[] bloodPrefabs;
        private int counter = 0;
        public IObjectPool<BFX_BloodSettings> BloodFXPool;

        private void Awake()
        {
            Instance = this;

            InitRiffleBulletPool();
            InitBulletDecalPool();
            InitBloodFXPools();
        }

        private void InitRiffleBulletPool()
        {
            RiffleBulletPool = new ObjectPool<RiffleBulletProjectile>(() =>
            {
                RiffleBulletProjectile projectile = Instantiate<RiffleBulletProjectile>(riffleBulletPrefab, Vector3.zero, Quaternion.identity);
                projectile.onHit += () => RiffleBulletPool.Release(projectile);
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
            BulletDecalPool = new ObjectPool<GameObject>(() =>
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
            BloodFXPool = new ObjectPool<BFX_BloodSettings>(() =>
            {
                if (counter >= bloodPrefabs.Length - 1)
                {
                    counter = 0;
                }
                
                BFX_BloodSettings bfxSettings = Instantiate<BFX_BloodSettings>(bloodPrefabs[counter], Vector3.zero, Quaternion.identity);
                bfxSettings.GroundHeight = 0.0f;
                bfxSettings.DecalRenderinMode = BFX_BloodSettings._DecalRenderinMode.AverageRayBetwenForwardAndFloor;

                counter++;

                return bfxSettings;
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
            false, 20, 50);
        }
    }
}

