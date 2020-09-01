using Scripts.UI;
using StateMachine.States;
using UnityEngine;

namespace StateMachine.Core
{
    public class StateMachine : MonoBehaviour
    {
        private BaseState currentState;

        [SerializeField] private UIRoot ui;
        public UIRoot UI => ui;

        public void ChangeState(BaseState newState)
        {
            if (currentState != null)
            {
                currentState.DestroyState();
            }

            // Swap reference
            currentState = newState;

            // If we passed reference to new state, we should assign owner of that state and initialize it!
            // If we decided to pass null as new state, nothing will happened.
            if (currentState != null)
            {
                currentState.Owner = this;
                currentState.PrepareState();
            }
        }
    }
}