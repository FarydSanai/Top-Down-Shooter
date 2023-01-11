using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter.CharacterControlling.Interfaces
{
    interface ICharacterStateController
    {
        void UpdateState(CharacterState characterState, bool active);
    }
}
