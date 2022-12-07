using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterControlling.Interfaces;


namespace CharacterControlling
{
    public enum CharacterState
    {
        Move,
        Shoot,

        COUNT,
    }

    public class CharacterStateController : ICharacterStateController
    {
        public Dictionary<CharacterState, bool> CurrentStates { get; private set; } = new();
        public int[] ArrTransitionParams = new int[(int)CharacterState.COUNT];

        private Animator characterAnimator;

        public CharacterStateController(Animator characterAnimator)
        {
            this.characterAnimator = characterAnimator;

            HashParameters();
        }

        private void HashParameters()
        {
            for (int i = 0; i < (int)CharacterState.COUNT; i++)
            {
                ArrTransitionParams[i] = Animator.StringToHash(((CharacterState)i).ToString());
            }
        }

        public void UpdateState(CharacterState state, bool active)
        {
            if (CurrentStates.ContainsKey(state))
            {
                CurrentStates[state] = active;           
            }
            else
            {
                CurrentStates.Add(state, active);
            }

            characterAnimator.SetBool(ArrTransitionParams[(int)state], active);
        }
    }
}