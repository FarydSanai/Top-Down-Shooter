using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace TopDownShooter.Networking
{
    public struct NetworkInputData : INetworkInput
    {
        public Vector2 movementInput;
        public Vector2 rotationInput;
        public NetworkBool isShooting;

        public NetworkInputData(Vector2 movementInput, Vector2 rotationInput, bool isShooting)
        {
            this.movementInput = movementInput;
            this.rotationInput = rotationInput;
            this.isShooting = isShooting;
        }
    }
}
