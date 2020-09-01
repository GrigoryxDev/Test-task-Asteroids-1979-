using Scripts.Game;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class GameView : BaseView
    {
        [SerializeField] private Text waveNumberText;
        [SerializeField] private Text scoreText;
        [SerializeField] private Text livesNumberText;
        [SerializeField] private NewWaveCounter countdown;

        public UnityAction OnGameOver;

        public Text WaveNumberText => waveNumberText;
        public Text ScoreText => scoreText;
        public Text LivesNumberText => livesNumberText;
        public NewWaveCounter Countdown => countdown;

        public void GameOver()
        {
            OnGameOver?.Invoke();
        }

    }
}