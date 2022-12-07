using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CharacterControlling.Interfaces;

namespace CharacterControlling
{
    public class CharacterControl : MonoBehaviour
    {
        private const float rotationSmoothTime = 0.12f;
        private const float shootRotationSmoothTime = 0.02f;
        private const float rotationApprox = 3.0f;

        [SerializeField] private PlayerInputReferences playerInput;

        [Header("Movement options")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;

        [Header("Rotation options")]
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;

        [Header("Character components")]
        private Rigidbody rigidBody;
        private ICharacterStateController characterStateController;
        private ICursorController cursorController;
        private IShootController shootController;

        private void Awake()
        {
            rigidBody = this.GetComponent<Rigidbody>();
            characterStateController = new CharacterStateController(this.GetComponent<Animator>());
            cursorController = new CursorController(playerInput, Camera.main);
            shootController = this.GetComponentInChildren<ShootController>();
            shootController.Init(this.transform);
        }

        private void OnEnable()
        {
            playerInput.EnableActionReferences();
        }
        private void OnDisable()
        {
            playerInput.DisableActionReferences();
        }

        private void Update()
        {
            CharacterRotate();
        }

        private void FixedUpdate()
        {
            CharacterMove(); 
        }

        private void CharacterMove()
        {
            rigidBody.velocity = new Vector3(playerInput.MoveDir.x, 0, playerInput.MoveDir.y)
                                        * movementSpeed;

            characterStateController.UpdateState(CharacterState.Move, playerInput.IsMove);
        }

        private void CharacterRotate()
        {
            if ((playerInput.IsMove && playerInput.IsShoot) || playerInput.IsShoot)
            {
                Vector3 dir = cursorController.GetCursorPosition() - this.transform.position;
                CharacterRotate(new Vector2(dir.x, dir.z), shootRotationSmoothTime);

                if (RotationComplete())
                {
                    shootController.Shoot();
                }
                return;
            }
            if (playerInput.IsMove)
            {
                CharacterRotate(new Vector2(playerInput.MoveDir.x, playerInput.MoveDir.y), rotationSmoothTime);
            }

        }

        private void CharacterRotate(Vector2 direction, float rotationSmoothTime)
        {
            _targetRotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            float rotation = Mathf.SmoothDampAngle(this.transform.eulerAngles.y,
                                                   _targetRotation,
                                                   ref _rotationVelocity,
                                                   rotationSmoothTime);

            // rotate to face input direction relative to camera position
            this.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        //private float GetAngleZX(Vector3 first, Vector3 second)
        //{
        //    return Mathf.Atan2(second.x - first.x, second.z - first.z) * Mathf.Rad2Deg;
        //}

        private bool RotationComplete()
        {
            return Mathf.Abs(this.transform.eulerAngles.y - SetAngleTo360(_targetRotation)) < rotationApprox;
        }

        private float SetAngleTo360(float angle)
        {
            return angle < 0 ? angle + 360 : angle;
        }
    }
}
