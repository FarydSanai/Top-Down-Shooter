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

        //pooling
        private IObjectPool<GameObject> projectilePool;

        public void Init(CursorController cursorController, Transform characterTransform)
        {
            this.cursorController = cursorController;
            this.characterTransform = characterTransform;
            InitProjectilePool();
        }

        private void InitProjectilePool()
        {
            projectilePool = new ObjectPool<GameObject>(() =>
            {
                GameObject obj = InstantiateProjectile(currentProjectilePrefab);
                obj.GetComponent<RiffleBulletProjectile>().onHit += () => projectilePool.Release(obj);
                return obj;
            },
            projectile =>
            {
                SetProjectileTransform(projectile);
                projectile.SetActive(true);
            },
            projectile =>
            {
                projectile.SetActive(false);
                projectile.transform.SetPositionAndRotation(Vector3.zero,
                                                            Quaternion.identity);

            },
            projectile =>
            {
                Destroy(projectile);
                Debug.Log("Destroy");
            },
            false, 10, 100);
        }

        public void Shoot()
        {
            if (Time.time >= fixedTimeMoment + shootDelay)
            {
                //InstantiateProjectile(currentProjectilePrefab);
                projectilePool.Get();
                //projectile.onHit += () => projectilePool.Release(projectile.gameObject);

                fixedTimeMoment = Time.time;
            }
        }

        private GameObject InstantiateProjectile(GameObject currentProjectile)
        {
            return Instantiate(currentProjectile,
                               projectileSpawnPoint.position,
                               Quaternion.LookRotation(characterTransform.forward +
                                                       new Vector3(Random.Range(-0.05f, 0.04f),
                                                       Random.Range(-0.05f, 0.04f), 0f),
                                                       Vector3.up));
        }

        private void SetProjectileTransform(GameObject projectile)
        {
            projectile.transform.SetPositionAndRotation(projectileSpawnPoint.position,
                                                        Quaternion.LookRotation(characterTransform.forward +
                                                                                new Vector3(Random.Range(-0.05f, 0.04f),
                                                                                Random.Range(-0.05f, 0.04f), 0f),
                                                                                Vector3.up));
            Debug.Log("Set");
        }
    }
}
