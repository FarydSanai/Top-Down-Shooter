using System.Collections;
using System.Collections.Generic;
using CharacterControlling;
using UnityEngine;

namespace CharacterControlling.Interfaces
{
    interface ICharacterStateController
    {
        void UpdateState(CharacterState characterState, bool active);
    }
}
