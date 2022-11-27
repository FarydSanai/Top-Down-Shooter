using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TopDownShooter;

namespace CharacterControllig
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private GameObject currentProjectilePrefab;
        [SerializeField] private Transform projectileSpawnPoint;

        private CursorController cursorController;
        private Transform characterTransform;

        private float shootDelay = 0.1f;
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
                //Vector3 shootDir = (cursorController.GetCursorPosition() - projectileSpawnPoint.position).normalized;
                Instantiate(currentProjectilePrefab, projectileSpawnPoint.position, Quaternion.LookRotation(characterTransform.forward, Vector3.up));

                fixedTimeMoment = Time.time;
            }
        }
    }
}
