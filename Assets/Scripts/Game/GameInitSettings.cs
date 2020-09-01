using System.Collections.Generic;
using Scripts.Enemies;
using UnityEngine;

namespace Scripts.Game
{

    /// <summary>
    /// ScriptableObject contains game settings and initialise it at the start.
    /// </summary>
    [CreateAssetMenu(fileName = "GameInitSettings", menuName = "ScriptableObjects/GameInitSettings", order = 0)]
    public class GameInitSettings : ScriptableObject
    {
        [SerializeField, Header("Time between waves")] private float timeBetweenWaves = 3;
        [SerializeField, Header("First wave number")] private int wave = 1;
        [SerializeField, Header("Increase enemies per wave")] private int enemyPlusPerWave = 1;

        [SerializeField, Space(5)] private Enemies[] enemies;
        [SerializeField, Space(5)] private AsteroidSpawnChance[] asteroidSpawnChances;

        [SerializeField, Header("Player settings")] private float rotateSpeed;
        [SerializeField] private int livesNumber;
        [SerializeField] private float speed;
        [SerializeField] private float invincebleTime;

        private Dictionary<EnemiesType, Enemies> enemyDict;


        public GameData GameData { get; private set; }
        public int LivesNumber => livesNumber;
        public int EnemyPlusPerWave => enemyPlusPerWave;
        public float RotateSpeed => rotateSpeed;
        public float Speed => speed;
        public float InvincebleTime => invincebleTime;
        public float TimeBetweenWaves => timeBetweenWaves;

        public void NewGameData()
        {
            GameData = new GameData(wave, 0);
            enemyDict = new Dictionary<EnemiesType, Enemies>();

            foreach (var item in enemies)
            {
                if (!enemyDict.ContainsKey(item.type))
                {
                    enemyDict.Add(item.type, item);
                }
            }
        }

        /// <summary>
        /// Quick access to any enemy data
        /// </summary>
        /// <param name="type">Enemy type</param>
        /// <returns></returns>
        public Enemies GetEnemyData(EnemiesType type)
        {
            return enemyDict[type];
        }

        public AsteroidSpawnChance GetAsteroidSpawnChance(AsteroidsBuckshot type)
        {
            return asteroidSpawnChances[(int)type];
        }
    }

    [System.Serializable]
    public class Enemies
    {
        public EnemiesType type;
        public float scale;
        public float speed;
        public int hp;
        public int score;
    }

    [System.Serializable]
    public class AsteroidSpawnChance
    {
        public float chance;
        public int minAmount;
        public int maxAmount;
    }

    public enum AsteroidsBuckshot
    {
        Med,
        Small
    }
}