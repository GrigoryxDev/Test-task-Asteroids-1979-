using System.Collections;
using System.Collections.Generic;
using Scripts.PlayerSystem;
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
        public GameUIUpdater GameUIUpdater { get; private set; }
        private Coroutine enemyCheck;
        private Player player;

        private void Start()
        {
            app = App.Instance;
            GameUIUpdater = GetComponent<GameUIUpdater>();
        }

        private void NewGame()
        {
            app.GameInitSettings.NewGameData();
            PlayerLives = app.GameInitSettings.LivesNumber;

            GameUIUpdater.UpdatePlayerLives();
            GameUIUpdater.UpdateUIText();

            SpawnPlayer();

            StartWaveCountdown();
        }

        private void StartWaveCountdown()
        {
            app.GetUI.GameView.Countdown.StartCountdown(app.GameInitSettings.TimeBetweenWaves);
        }

        private void EndGame()
        {
            app.GetUI.GameView.GameOver();
        }

        private void SpawnPlayer()
        {
            var playerGO = app.ObjectPooler.SpawnFromPool(PoolObjectsTag.Player);
            player = playerGO.GetComponent<Player>();
            player.HP = PlayerLives;
            player.transform.position = Vector3.zero;
        }

        private IEnumerator CheckEnemies()
        {
            yield return new WaitForFixedUpdate();
            if (EnemiesNumber <= 0)
            {
                app.GameInitSettings.GameData.WaveNumber++;

                StartWaveCountdown();
            }
        }

        public void OnDestroyPlayer()
        {
            PlayerLives--;
            GameUIUpdater.UpdatePlayerLives();

            if (PlayerLives <= 0)
            {
                PlayerLives = 0;

                EndGame();
            }
            else
            {
                SpawnPlayer();
            }
        }

        public void OnSpawnEnemy()
        {
            EnemiesNumber++;
        }

        public void OnKillEnemy(int score)
        {
            EnemiesNumber--;
            if (enemyCheck != null)
            {
                StopCoroutine(enemyCheck);
            }

            enemyCheck = StartCoroutine(CheckEnemies());

            GameUIUpdater.UpdateScore(score);
        }

        public void StartNewWave()
        {
            if (player != null)
            {
                player.AddInvinceble();
            }

            var gameSettings = app.GameInitSettings;
            var enemyNumber = gameSettings.GameData.WaveNumber + gameSettings.EnemyPlusPerWave;
            var asteroidsNumber = Random.Range(1, enemyNumber - 1);
            var ufoNumber = enemyNumber - asteroidsNumber;

            for (int i = 0; i < asteroidsNumber; i++)
            {
                OnSpawnEnemy();
                var asteroid = app.ObjectPooler.SpawnFromPool(PoolObjectsTag.Asteroid);

                asteroid.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-7, 7), 0);

            }
            for (int i = 0; i < ufoNumber; i++)
            {
                OnSpawnEnemy();
                var ufo = app.ObjectPooler.SpawnFromPool(PoolObjectsTag.UFO);

                ufo.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-7, 7), 0);
            }
        }

        public void ResetGame()
        {
            var Array = app.ObjectPooler.gameObject.GetComponentsInChildren<IEnemy>();

            foreach (var item in Array)
            {
                item.Enemy.GetComponent<IPooledObject>().OnReturnToPool();
            }
            EnemiesNumber = 0;
            NewGame();
        }
    }
}