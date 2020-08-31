using StateMachine.Core;
using UnityEngine;

namespace StateMachine.States
{
    public class GameOverState : BaseState
    {
        public override void PrepareState()
        {
            base.PrepareState();

            // Attach functions to view events
            Owner.UI.GameOverView.OnReplay += ReplayClicked;
            

            // Show summary view
            Owner.UI.GameOverView.ShowView();
        }

        public override void DestroyState()
        {
            // Hide summary view
            Owner.UI.GameOverView.HideView();

            // Detach functions from view events
            Owner.UI.GameOverView.OnReplay -= ReplayClicked;

            base.DestroyState();
        }

        
        private void ReplayClicked()
        {
            Owner.ChangeState(new GameState());
        }

        
    }
}