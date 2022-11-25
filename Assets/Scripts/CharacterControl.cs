using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterControllig
{
    public class CharacterControl : MonoBehaviour
    {
        [SerializeField] private InputActionReference playerMove;


        [Header("Movement options")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;

        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float RotationSmoothTime = 0.12f;

        private float aimRotationSmoothTime = 0.02f;


        private Vector2 PlayerMove => playerMove.action.ReadValue<Vector2>();


        [Header("Character components")]
        private Rigidbody rigidBody;
        private CharacterStateController characterStateController;

        private void Awake()
        {
            rigidBody = this.GetComponent<Rigidbody>();
            characterStateController = new CharacterStateController(this.GetComponent<Animator>());
        }

        private void OnEnable()
        {
            playerMove.action.Enable();
        }
        private void OnDisable()
        {
            playerMove.action.Disable();
        }

        private void Update()
        {
            //CharacterRotate();
        }

        private void FixedUpdate()
        {
            CharacterMove(); 
        }

        private void CharacterMove()
        {
            rigidBody.velocity = new Vector3(PlayerMove.x, 0, PlayerMove.y) * movementSpeed;

            characterStateController.UpdateState(CharacterState.Move, PlayerMove != Vector2.zero);
        }

        private void CharacterRotate()
        {
            if (PlayerMove != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(PlayerMove.x, PlayerMove.y) * Mathf.Rad2Deg;

                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                                                       _targetRotation,
                                                       ref _rotationVelocity,
                                                       RotationSmoothTime);

                // rotate to face input direction relative to camera position
                this.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
        }

        public void TestRotate(Vector3 targetRotation)
        {
            _targetRotation = Mathf.Atan2(targetRotation.x, targetRotation.z) * Mathf.Rad2Deg;

            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                                                   _targetRotation,
                                                   ref _rotationVelocity,
                                                   aimRotationSmoothTime);

            // rotate to face input direction relative to camera position
            this.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
    }
}
