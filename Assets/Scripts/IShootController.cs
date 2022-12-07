using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControlling.Interfaces
{
    interface IShootController
    {
        void Init(Transform characterTransform);
        void Shoot();
    }
}

