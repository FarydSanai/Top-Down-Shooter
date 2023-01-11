using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TopDownShooter.Networking;

namespace TopDownShooter.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New PlayerInputReferences", menuName = "PhotonTest/PlayerInputReferences")]
    public class PlayerInputReferences : ScriptableObject
    {
        [Header("Input Action References")]
        [SerializeField] private InputActionReference playerMove;
        [SerializeField] private InputActionReference playerShoot;
        [SerializeField] private InputActionReference playerAim;

        public Vector2 CursorPos => playerAim.action.ReadValue<Vector2>();
        public Vector2 MoveDir => playerMove.action.ReadValue<Vector2>();

        public bool IsMove => (playerMove.action.ReadValue<Vector2>() != Vector2.zero);
        public bool IsShoot => playerShoot.action.IsPressed();

        private bool isEnabled;

        public void EnableActionReferences()
        {
            playerMove.action.Enable();
            playerShoot.action.Enable();
            playerAim.action.Enable();

            isEnabled = true;
        }

        public void DisableActionReferences()
        {
            playerMove.action.Disable();
            playerShoot.action.Disable();
            playerAim.action.Disable();

            isEnabled = false;
        }

        public NetworkInputData GetInputData()
        {
            if (isEnabled)
            {
                return new NetworkInputData(MoveDir, CursorPos, IsShoot);
            }
            return new NetworkInputData(Vector2.zero, Vector2.zero, false);
        }
    }
}