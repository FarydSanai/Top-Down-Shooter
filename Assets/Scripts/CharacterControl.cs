using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterControllig
{
    public class CharacterControl : MonoBehaviour
    {
        [SerializeField] private PlayerInputReferences playerInput;

        [Header("Movement options")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;

        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float RotationSmoothTime = 0.12f;
        private float aimRotationSmoothTime = 0.02f;

        [Header("Character components")]
        private Rigidbody rigidBody;
        private CharacterStateController characterStateController;
        private CursorController cursorController;

        private void Awake()
        {
            rigidBody = this.GetComponent<Rigidbody>();
            characterStateController = new CharacterStateController(this.GetComponent<Animator>());
            cursorController = new CursorController(playerInput, Camera.main);
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
            if (playerInput.IsMove)
            {
                if (playerInput.IsShoot)
                {
                    Vector3 dir = cursorController.GetCursorPosition() - this.transform.position;
                    CharacterRotate(new Vector2(dir.x, dir.z), aimRotationSmoothTime);
                    return;
                }

                CharacterRotate(new Vector2(playerInput.MoveDir.x, playerInput.MoveDir.y), RotationSmoothTime);
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
    }
}
