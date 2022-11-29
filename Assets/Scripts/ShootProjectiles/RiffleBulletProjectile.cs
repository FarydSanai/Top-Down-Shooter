using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class RiffleBulletProjectile : MonoBehaviour
    {
        [SerializeField] private float startSpeed = 100f;

        [SerializeField] private GameObject projectileDecal;

        [SerializeField] private LayerMask playerLM;
        [SerializeField] private  List<GameObject> bloodPrefabs;


        private Rigidbody rigidBody;

        private void Awake()
        {
            rigidBody = this.GetComponent<Rigidbody>();
        }

        private void Start()
        {
            rigidBody.velocity = this.transform.forward * startSpeed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer != this.gameObject.layer)
            {
                //Debug.Log("Destroy");

                Vector3 hitPos = collision.contacts[0].point;

                if (IsShootEnemy(collision))
                {
                    Debug.Log("Shoot enemy");

                    float angle = Mathf.Atan2(hitPos.x, hitPos.z) * Mathf.Rad2Deg + 180;

                    var instance = Instantiate(bloodPrefabs[Random.Range(0, 14)], hitPos, Quaternion.Euler(0, angle + 90, 0));

                    BFX_BloodSettings bloodSettings = instance.GetComponent<BFX_BloodSettings>();

                    bloodSettings.GroundHeight = 0f;
                    bloodSettings.DecalRenderinMode = BFX_BloodSettings._DecalRenderinMode.AverageRayBetwenForwardAndFloor;

                    Destroy(instance, 4f);
                }
                else
                {
                    Instantiate(projectileDecal, hitPos, this.transform.rotation);
                }

                Destroy(this.gameObject);
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
    }
}