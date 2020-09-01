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
        public GameUIUpdater GameUIUpdater { get; private set; }
        private Player player;
        private Coroutine coroutine;
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

        private void SpawnPlayer()
        {
            var playerGO = app.ObjectPooler.SpawnFromPool(PoolObjectsTag.Player);
            player = playerGO.GetComponent<Player>();
            player.HP = PlayerLives;
            player.transform.position = Vector3.zero;
        }

        private void ResetAllEnemies()
        {
            var Array = app.ObjectPooler.gameObject.GetComponentsInChildren<IEnemy>();

            foreach (var item in Array)
            {
                item.Enemy.GetComponent<IPooledObject>().OnReturnToPool();
            }
        }

        private int GetEnemyCount => app.ObjectPooler.gameObject.GetComponentsInChildren<IEnemy>().Length;
        private IEnumerator GetEnemyArray()
        {
            yield return new WaitForFixedUpdate();
            if (GetEnemyCount < 1)
            {
                app.GameInitSettings.GameData.WaveNumber++;
                StartWaveCountdown();
            }
            coroutine = null;
        }

        public void OnDestroyPlayer()
        {
            PlayerLives--;
            GameUIUpdater.UpdatePlayerLives();

            if (PlayerLives <= 0)
            {
                PlayerLives = 0;

                app.GetUI.GameView.GameOver();
            }
            else
            {
                SpawnPlayer();
            }
        }


        public void OnKillEnemy(int score)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            coroutine = StartCoroutine(GetEnemyArray());
            GameUIUpdater.UpdateScore(score);
        }

        public void StartNewWave()
        {
            ResetAllEnemies();
            GameUIUpdater.UpdateScore(0);
            
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
                var asteroid = app.ObjectPooler.SpawnFromPool(PoolObjectsTag.Asteroid);

                asteroid.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-7, 7), 0);

            }
            for (int i = 0; i < ufoNumber; i++)
            {
                var ufo = app.ObjectPooler.SpawnFromPool(PoolObjectsTag.UFO);

                ufo.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-7, 7), 0);
            }
        }

        public void ResetGame()
        {
            NewGame();
        }
    }
}