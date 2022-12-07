using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using CharacterControlling;

namespace TopDownShooter
{
    public class RiffleBulletProjectile : MonoBehaviour
    {
        public event Action onHit;

        [SerializeField] private float startSpeed = 50f;
        [SerializeField] private LayerMask playerLM;
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

                    BFX_BloodSettings bloodFX = PoolSystem.Instance.BloodFXPool.Get();
                    SetBloodFXTransform(bloodFX.transform, hitPos);
                }
                else
                {
                    GameObject bulletDecal = PoolSystem.Instance.BulletDecalPool.Get();
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

        private void SetBloodFXTransform(Transform bloogFX, Vector3 hitPosition)
        {
            float angle = Mathf.Atan2(hitPosition.x, hitPosition.z) * Mathf.Rad2Deg + 180f;

            bloogFX.SetPositionAndRotation(hitPosition, Quaternion.Euler(0, angle + 90f, 0));
        }
    }
}