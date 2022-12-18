using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooter
{
    public class RagdollController : MonoBehaviour
    {
        [SerializeField] private Animator characterAnimator;
        [SerializeField] private Transform weapon;

        private Rigidbody[] rigidBodies;

        private Collider[] colliders;

        private CharacterJoint[] joints;

        private void Awake()
        {
            InitRagdollParts();

            ToggleRagdoll(false);
        }

        private void InitRagdollParts()
        {
            rigidBodies = this.GetComponentsInChildren<Rigidbody>();
            colliders = this.GetComponentsInChildren<Collider>();
            joints = this.GetComponentsInChildren<CharacterJoint>();
        }


        //temp
        [ContextMenu("ToggleRagdoll")]
        public void ToggleRagdoll()
        {
            ToggleRagdoll(true);
        }

        public void ToggleRagdoll(bool active)
        {
            characterAnimator.enabled = !active;

            for (int i = 0; i < rigidBodies.Length; i++)
            {
                if (active)
                {
                    rigidBodies[i].velocity = Vector3.zero;
                }
                rigidBodies[i].detectCollisions = active;
                rigidBodies[i].useGravity = active;
            }

            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = active;
            }

            for (int i = 0; i < joints.Length; i++)
            {
                joints[i].enableCollision = active;
            }
        }

        public void AddRagdollForce(Vector3 direction, float force)
        {
            for (int i = 0; i < rigidBodies.Length; i++)
            {
                rigidBodies[i].AddForce(direction * force, ForceMode.Acceleration);
            }
        }
    }
}
