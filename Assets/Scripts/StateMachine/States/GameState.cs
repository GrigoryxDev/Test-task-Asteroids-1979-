using Scripts.Core;
using StateMachine.Core;
using UnityEngine;

namespace StateMachine.States
{
    public class GameState : BaseState
    {
        public override void PrepareState()
        {
            base.PrepareState();

            Owner.UI.GameView.OnGameOver += GameOver;

            Owner.UI.GameView.ShowView();
            App.Instance.GameManager.ResetGame();
        }

        public override void DestroyState()
        {
            Owner.UI.GameView.HideView();

            Owner.UI.GameView.OnGameOver -= GameOver;

            base.DestroyState();
        }

        private void GameOver()
        {
            Owner.ChangeState(new GameOverState());
        }

    }
}