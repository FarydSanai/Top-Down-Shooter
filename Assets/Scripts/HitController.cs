using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace TopDownShooter
{
    public class HitController : MonoBehaviour
    {
        [SerializeField] private RagdollController ragdollController;
        [SerializeField] private LayerMask projectileLM;

        private int shotCount;

        private void OnCollisionEnter(Collision collision)
        {
            if (IsGetShot(collision))
            {
                Debug.Log("Get Shot");
                shotCount++;

                if (shotCount >= 3)
                {
                    ragdollController.ToggleRagdoll(true);
                }
            }
        }

        private bool IsGetShot(Collision collision)
        {
            return Utilities.CompareLayers(projectileLM.value, collision.gameObject.layer);
        }
    }
}
