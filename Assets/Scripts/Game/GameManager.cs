using System.Collections;
using System.Collections.Generic;
using Scripts.Core;
using SpawnSystem;
using UnityEngine;

namespace Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        private App app;
        public int PlayerLives { get; private set; }
        public int EnemiesNumber { get; private set; }

        private void Start()
        {
            app = App.Instance;
        }

        public void NewGame()
        {
            app.GameInitSettings.NewGameData();
            PlayerLives = app.GameInitSettings.LivesNumber;


            StartNewWave();
            SpawnPlayer();
        }

        public void UpdateScore(int score)
        {
            app.GameInitSettings.UpdateScore(app.GameInitSettings.Wave, score);

            UpdateUIText();
        }

        private void SpawnPlayer()
        {
            var player = app.ObjectPooler.SpawnFromPool(PoolObjectsTag.Player);
            player.GetComponent<Player>().HP = PlayerLives;
            player.transform.position = Vector3.zero;
        }

        public void DestroyPlayer()
        {
            PlayerLives--;
            UpdatePlayerLives();

            if (PlayerLives <= 0)
            {
                PlayerLives = 0;

                EndGame();
            }
            else
            {

            }
        }

        public void UpdatePlayerLives()
        {
            var livesText = app.GetUI.GameView.LivesNumberText;
            livesText.text = $"x{PlayerLives}";
        }

        private void StartNewWave()
        {
            var gameSettings = app.GameInitSettings;
            var enemyNumber = gameSettings.GameData.WaveNumber + gameSettings.EnemyPlusPerWave;
            var asteroidsNumber = Random.Range(1, enemyNumber - 1);
            var ufoNumber = enemyNumber - asteroidsNumber;

            EnemiesNumber = asteroidsNumber + ufoNumber;

            for (int i = 0; i < asteroidsNumber; i++)
            {
                var asteroid = app.ObjectPooler.SpawnFromPool(PoolObjectsTag.Asteroid);

                asteroid.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-7, 7), 0);

            }
            for (int i = 0; i < ufoNumber; i++)
            {
                var ufo = app.ObjectPooler.SpawnFromPool(PoolObjectsTag.UFO);

                ufo.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-7, 7), 0);
            }

            UpdateUIText();
        }

        private void UpdateUIText()
        {
            var scoreText = app.GetUI.GameView.ScoreText;
            var waveText = app.GetUI.GameView.WaveNumberText;
            var gameData = app.GameInitSettings.GameData;

            scoreText.text = gameData.Score.ToString();
            waveText.text = gameData.WaveNumber.ToString();
        }

        private void EndGame()
        {

            app.GetUI.GameView.GameOver();
        }
    }
}