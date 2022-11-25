using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterControllig
{
    public class CursorControl : MonoBehaviour
    {
        [SerializeField] private CharacterControl player;
        [SerializeField] private InputActionReference cursor;
        [SerializeField] private InputActionReference shoot;


        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            cursor.action.Enable();
            shoot.action.Enable();
        }

        private void OnDisable()
        {
            cursor.action.Disable();
            shoot.action.Disable();
        }

        private void Update()
        {
            if (shoot.action.IsPressed())
            {
                RaycastHit hit;

                Ray ray = mainCamera.ScreenPointToRay((Vector3)(cursor.action.ReadValue<Vector2>()));

                if (Physics.Raycast(ray, out hit))
                {
                    player.TestRotate(hit.point - player.transform.position);
                }

                //Debug.DrawLine(player.transform.position + new Vector3(0f, 1.5f, 0f),
                //               player.transform.forward * 100f,
                //               Color.red,
                //               100f);
            }
        }
    }
}
