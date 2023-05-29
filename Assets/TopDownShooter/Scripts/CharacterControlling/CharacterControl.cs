using UnityEngine;
using TopDownShooter.ScriptableObjects;
using TopDownShooter.CharacterControlling.Interfaces;
using Fusion;
using TopDownShooter.Networking;

namespace TopDownShooter.CharacterControlling
{
    public class CharacterControl : NetworkBehaviour
    {
        private const float rotationSmoothTime = 0.12f;
        private const float shootRotationSmoothTime = 0.02f;
        private const float rotationApprox = 3.0f;

        [SerializeField] private PlayerInputReferences playerInput;
        public PlayerInputReferences PlayerInput => playerInput;

        [Header("Movement options")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;

        [Header("Rotation options")]
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;

        [Header("Character components")]
        private BoxCollider boxCollider;
        private Rigidbody rigidBody;
        private HitController characterHitController;
        private ICharacterStateController characterStateController;
        private ICursorController cursorController;
        private IShootController shootController;

        private void Awake()
        {
            boxCollider = this.GetComponent<BoxCollider>();
            rigidBody = this.GetComponent<Rigidbody>();
            characterHitController = this.gameObject.GetComponent<HitController>();
            characterStateController = new CharacterStateController(this.GetComponent<Animator>());
            cursorController = new CursorController(playerInput, Camera.main);
            shootController = this.GetComponentInChildren<ShootController>();
            shootController.Init(this.transform);
        }

        private void OnEnable()
        {
            playerInput.EnableActionReferences();
            characterHitController.onCharacterDeath += OnDeathProc;
        }
        private void OnDisable()
        {
            playerInput.DisableActionReferences();
            characterHitController.onCharacterDeath -= OnDeathProc;
        }

        private void Update()
        {
            //Temp
            if (NetworkingPlayer.Local == this.GetComponent<NetworkingPlayer>())
            {
                CharacterRotate();
            }
        }

        private void FixedUpdate()
        {
            //Temp
            if (NetworkingPlayer.Local == this.GetComponent<NetworkingPlayer>())
            {
                CharacterMove();
            }
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData networkInputData))
            {
                rigidBody.velocity = new Vector3(networkInputData.movementInput.x,
                                                 0f,
                                                 networkInputData.movementInput.y) * movementSpeed;

                characterStateController.UpdateState(CharacterState.Move, playerInput.IsMove);

                if (playerInput.IsMove)
                {
                    CharacterRotate(new Vector2(networkInputData.movementInput.x, networkInputData.movementInput.y), rotationSmoothTime);
                }
            }
        }

        private void CharacterMove()
        {
            if (playerInput.IsMove)
            {
                rigidBody.velocity = new Vector3(playerInput.MoveDir.x, 0f, playerInput.MoveDir.y)
                                        * movementSpeed;

                characterStateController.UpdateState(CharacterState.Move, playerInput.IsMove);
            }
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

            this.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        private bool RotationComplete()
        {
            return Mathf.Abs(this.transform.eulerAngles.y - SetAngleTo360(_targetRotation)) < rotationApprox;
        }

        private float SetAngleTo360(float angle)
        {
            return angle < 0 ? angle + 360 : angle;
        }

        private void OnDeathProc()
        {
            boxCollider.enabled = false;
            Debug.Log("Invoke??");
        }
    }
}
