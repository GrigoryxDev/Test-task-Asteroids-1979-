using StateMachine.Core;
using UnityEngine;

namespace StateMachine.States
{
    public class MenuState : BaseState
    {
        public override void PrepareState()
        {
            base.PrepareState();

            Owner.UI.MenuView.OnStart += StartGame;

            Owner.UI.MenuView.ShowView();

        }

        public override void DestroyState()
        {
            Owner.UI.MenuView.HideView();

            Owner.UI.MenuView.OnStart -= StartGame;

            base.DestroyState();
        }


        private void StartGame()
        {
            Owner.ChangeState(new GameState());
        }

    }
}