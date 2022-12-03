using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using CharacterControllig;

namespace TopDownShooter
{
    public class RiffleBulletProjectile : MonoBehaviour
    {
        public event Action onHit;

        [SerializeField] private float startSpeed = 50f;

        [SerializeField] private GameObject projectileDecal;

        [SerializeField] private LayerMask playerLM;
        [SerializeField] private  List<GameObject> bloodPrefabs;

        private Rigidbody rigidBody;

        private void Awake()
        {
            rigidBody = this.GetComponent<Rigidbody>();
        }

        private void OnDisable()
        {
            rigidBody.velocity = Vector3.zero;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer != this.gameObject.layer)
            {
                Vector3 hitPos = collision.contacts[0].point;

                if (IsShootEnemy(collision))
                {
                    Debug.Log("Shoot enemy");

                    float angle = Mathf.Atan2(hitPos.x, hitPos.z) * Mathf.Rad2Deg + 180f;

                    //var instance = Instantiate(bloodPrefabs[Random.Range(0, bloodPrefabs.Count - 1)], hitPos, Quaternion.Euler(0, angle + 90f, 0));

                    //BFX_BloodSettings bloodSettings = instance.GetComponent<BFX_BloodSettings>();

                    //bloodSettings.GroundHeight = 0f;
                    //bloodSettings.DecalRenderinMode = BFX_BloodSettings._DecalRenderinMode.AverageRayBetwenForwardAndFloor;

                    //Destroy(instance, 4f);

                    BFX_BloodSettings bloodFX = PoolSystem.Instance.bloodPool.Get();
                    bloodFX.transform.position = hitPos;
                    bloodFX.transform.rotation = Quaternion.Euler(0, angle + 90f, 0);

                }
                else
                {
                    GameObject bulletDecal = PoolSystem.Instance.bulletDecalPool.Get();
                    bulletDecal.transform.SetPositionAndRotation(hitPos, collision.transform.rotation);
                }

                onHit?.Invoke();
            }
        }

        private bool IsShootEnemy(Collision collision)
        {
            if ((playerLM.value & (1 << collision.gameObject.layer)) > 0)
            {
                return true;
            }
            return false;
        }

        public void Shoot()
        {
            rigidBody.velocity = this.transform.forward * startSpeed;
        }

        public void SetStartPosition(Vector3 position, Quaternion rotation)
        {
            this.transform.SetPositionAndRotation(position, rotation);
        }

        private void SetBloodFXTransform(Vector3 hitPosition)
        {
            float angle = Mathf.Atan2(hitPosition.x, hitPosition.z) * Mathf.Rad2Deg + 180f;
        }
    }
}