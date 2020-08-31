using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameView : BaseView
{
    [SerializeField] private Text waveNumberText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesNumberText;

    public UnityAction OnGameOver;

    public Text WaveNumberText => waveNumberText;
    public Text ScoreText => scoreText;
    public Text LivesNumberText => livesNumberText;

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

}
