using System.Collections;
using System.Collections.Generic;
using Scripts.Core;
using UnityEngine;

public class GameUIUpdater : MonoBehaviour
{
    private App app;
    private void Start()
    {
        app = App.Instance;
    }

    public void UpdateScore(int score)
    {
        app.GameInitSettings.GameData.Score += score;

        UpdateUIText();
    }

    public void UpdatePlayerLives()
    {
        var livesText = app.GetUI.GameView.LivesNumberText;
        livesText.text = $"x{app.GameManager.PlayerLives}";
    }

    public void UpdateUIText()
    {
        var scoreText = app.GetUI.GameView.ScoreText;
        var waveText = app.GetUI.GameView.WaveNumberText;
        var gameData = app.GameInitSettings.GameData;

        scoreText.text = gameData.Score.ToString();
        waveText.text = gameData.WaveNumber.ToString();
    }
}
