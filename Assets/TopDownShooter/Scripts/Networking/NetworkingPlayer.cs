using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace TopDownShooter.Networking
{
    public class NetworkingPlayer : NetworkBehaviour, IPlayerLeft
    {
        public static NetworkingPlayer Local { get; private set; }


        private void Start()
        {
            
        }

        public override void Spawned()
        {
            if (Object.HasInputAuthority)
            {
                Local = this;
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