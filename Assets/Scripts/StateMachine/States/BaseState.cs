using UnityEngine;

namespace StateMachine.States
{
    public abstract class BaseState
    {
        [SerializeField] private Core.StateMachine owner;

        public Core.StateMachine Owner { get => owner; set => owner = value; }

        public virtual void PrepareState()
        {

        }


        public virtual void UpdateState()
        {

        }

        public virtual void DestroyState()
        {

        }
    }
}