using Scripts.Core;
using StateMachine.Core;
using UnityEngine;

namespace StateMachine.States
{
    public class GameOverState : BaseState
    {
        private const string hideScorePPkey = "hideScorePPkey";
        public override void PrepareState()
        {
            base.PrepareState();

            Owner.UI.GameOverView.OnReplay += ReplayClicked;
            var gameOverView = Owner.UI.GameOverView;

            gameOverView.ShowView();

            var score = App.Instance.GameInitSettings.GameData.Score;
            var bestScore = PlayerPrefs.GetInt(hideScorePPkey, 0);

            if (score > bestScore)
            {
                bestScore = score;
                PlayerPrefs.SetInt(hideScorePPkey, bestScore);
                gameOverView.BestScoreText.gameObject.SetActive(false);
            }
            else
            {
                gameOverView.BestScoreText.gameObject.SetActive(true);
                gameOverView.BestScoreText.text = $"Best score: {bestScore}";
            }

            Owner.UI.GameOverView.ScoreText.text = $"Current score: {score}";
        }

        public override void DestroyState()
        {

            Owner.UI.GameOverView.HideView();

            Owner.UI.GameOverView.OnReplay -= ReplayClicked;

            base.DestroyState();
        }


        private void ReplayClicked()
        {
            Owner.ChangeState(new GameState());
        }


    }
}