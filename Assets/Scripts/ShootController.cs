using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using TopDownShooter;
using Random = UnityEngine.Random;
using CharacterControlling.Interfaces;

namespace CharacterControlling
{
    public class ShootController : MonoBehaviour, IShootController
    {
        private const float shootDelay = 0.12f;
        private float lastFixedTime;

        [SerializeField] private GameObject currentProjectilePrefab;
        [SerializeField] private Transform projectileSpawnPoint;

        private Transform characterTransform;
        private HitController hitController;

        public void Init(Transform characterTransform)
        {
            this.characterTransform = characterTransform;
        }

        public void Shoot()
        {
            if (Time.time >= lastFixedTime + shootDelay)
            {
                RiffleBulletProjectile projectile = PoolSystem.Instance.RiffleBulletPool.Get();
                //Debug.Log(projectile.transform.position + " | " + projectileSpawnPoint.position);

                projectile.Shoot(projectileSpawnPoint.position, SetProjectileRotation());

                lastFixedTime = Time.time;               
            }
        }

        private Quaternion SetProjectileRotation()
        {
            float offsetX = Random.Range(-0.05f, 0.04f);
            float offsetY = Random.Range(-0.05f, 0.04f);
            return Quaternion.LookRotation(characterTransform.forward + new Vector3(offsetX, offsetY), Vector3.up);
        }
    }
}
