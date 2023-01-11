using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TopDownShooter.CharacterControlling;

namespace TopDownShooter.Networking
{
    public class NetworkingPlayer : NetworkBehaviour, IPlayerLeft
    {
        public static NetworkingPlayer Local { get; private set; }
        public CharacterControl characterControl;

        private void Start()
        {
            
        }

        public override void Spawned()
        {
            if (Object.HasInputAuthority)
            {
                Local = this;
                characterControl = this.GetComponent<CharacterControl>();
                Debug.Log("Spawned Local player");
            }
            else
            {
                Debug.Log("Spawned Remote player");
            }
        }

        public void PlayerLeft(PlayerRef player)
        {
            if (player == Object.InputAuthority)
            {
                Runner.Despawn(Object);
            }
        }
    }
}