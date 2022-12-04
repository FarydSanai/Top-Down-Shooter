using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using TopDownShooter;
using Random = UnityEngine.Random;

namespace CharacterControllig
{
    public class ShootController : MonoBehaviour
    {
        private const float shootDelay = 0.12f;

        [SerializeField] private GameObject currentProjectilePrefab;
        [SerializeField] private Transform projectileSpawnPoint;

        private CursorController cursorController;
        private Transform characterTransform;

        private float lastFixedTime;

        public void Init(CursorController cursorController, Transform characterTransform)
        {
            this.cursorController = cursorController;
            this.characterTransform = characterTransform;
        }

        public void Shoot()
        {
            if (Time.time >= lastFixedTime + shootDelay)
            {
                RiffleBulletProjectile projectile = PoolSystem.Instance.RiffleBulletPool.Get();
                
                projectile.SetStartPosition(projectileSpawnPoint.position, SetProjectileRotation());
                projectile.Shoot();

                lastFixedTime = Time.time;
            }
        }

        private Quaternion SetProjectileRotation()
        {
            float offsetX = Random.Range(-0.05f, 0.04f);
            float offsetY = Random.Range(-0.05f, 0.04f);
            return Quaternion.LookRotation(characterTransform.forward + new Vector3(offsetX, offsetY), Vector3.up);
        }

        //private void SetProjectileTransform(GameObject projectile)
        //{
        //    float offsetX = Random.Range(-0.05f, 0.04f);
        //    float offsetY = Random.Range(-0.05f, 0.04f);

        //    projectile.transform.SetPositionAndRotation(projectileSpawnPoint.position,
        //                                                Quaternion.LookRotation(characterTransform.forward +
        //                                                                        new Vector3(offsetX, offsetY),                                                                    Vector3.up));
        //}
    }
}
