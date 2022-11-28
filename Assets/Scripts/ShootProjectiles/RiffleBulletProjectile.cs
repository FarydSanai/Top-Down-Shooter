using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class RiffleBulletProjectile : MonoBehaviour
    {
        [SerializeField] private float startSpeed = 100f;
        [SerializeField] private GameObject projectileDecal;
        //[SerializeField] private LayerMask layerMask;

        private Rigidbody rigidBody;

        private void Awake()
        {
            rigidBody = this.GetComponent<Rigidbody>();
        }

        private void Start()
        {
            rigidBody.velocity = this.transform.forward * startSpeed;
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.layer != this.gameObject.layer)
        //    {

        //    }
        //}

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer != this.gameObject.layer)
            {
                //Debug.Log("Destroy");

                Vector3 hitPos = collision.contacts[0].point;

                Instantiate(projectileDecal, hitPos, this.transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }
}