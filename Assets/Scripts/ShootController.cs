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
        [SerializeField] private GameObject currentProjectilePrefab;
        [SerializeField] private Transform projectileSpawnPoint;

        private CursorController cursorController;
        private Transform characterTransform;

        private float shootDelay = 0.12f;
        private float fixedTimeMoment;

        public void Init(CursorController cursorController, Transform characterTransform)
        {
            this.cursorController = cursorController;
            this.characterTransform = characterTransform;
        }

        public void Shoot()
        {
            if (Time.time >= fixedTimeMoment + shootDelay)
            {
                RiffleBulletProjectile projectile = PoolSystem.Instance.riffleBulletPool.Get();
                projectile.onSpawn += () => SetProjectileTransform(projectile.gameObject);

                fixedTimeMoment = Time.time;
            }
        }

        private void SetProjectileTransform(GameObject projectile)
        {
            float offsetX = Random.Range(-0.05f, 0.04f);
            float offsetY = Random.Range(-0.05f, 0.04f);

            projectile.transform.SetPositionAndRotation(projectileSpawnPoint.position,
                                                        Quaternion.LookRotation(characterTransform.forward +
                                                                                new Vector3(offsetX, offsetY),
                                                                                Vector3.up));
            Debug.Log("Set");
        }
    }
}
