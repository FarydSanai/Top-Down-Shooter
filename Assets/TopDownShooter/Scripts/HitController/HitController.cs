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
        public event Action onCharacterDeath;

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
                    Vector3 forceDir = ((this.transform.forward * -1) + this.transform.up);
                    ragdollController.AddRagdollForce(forceDir, 100f);

                    onCharacterDeath?.Invoke();
                }
            }
        }

        private bool IsGetShot(Collision collision)
        {
            return Utilities.CompareLayers(projectileLM.value, collision.gameObject.layer);
        }
    }
}
