using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverView : BaseView
{
    public UnityAction OnReplay;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;

    public Text ScoreText => scoreText;
    public Text BestScoreText => bestScoreText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Replay();
        }
    }

    public void Replay()
    {
        OnReplay?.Invoke();
    }

}
