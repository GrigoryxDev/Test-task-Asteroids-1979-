using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameInitSettings", menuName = "ScriptableObjects/GameInitSettings", order = 0)]
public class GameInitSettings : ScriptableObject
{
    [SerializeField, Header("Player's lives count")] private int livesNumber;
    [SerializeField, Header("First wave number")] private int wave = 1;
    [SerializeField, Header("Increase enemies per wave")] private int enemyPlusPerWave = 1;
    [SerializeField, Space(5)] private Enemies[] enemies;
    [SerializeField, Space(5)] private AsteroidSpawnChance[] asteroidSpawnChances;
    private Dictionary<EnemiesType, Enemies> enemyDict;
    public GameData GameData { get; private set; }
    public int LivesNumber => livesNumber;
    public int Wave => wave;
    public int EnemyPlusPerWave => enemyPlusPerWave;

    public void NewGameData()
    {
        GameData = new GameData(Wave, 0);
        enemyDict = new Dictionary<EnemiesType, Enemies>();

        foreach (var item in enemies)
        {
            if (!enemyDict.ContainsKey(item.type))
            {
                enemyDict.Add(item.type, item);
            }

        }
    }

    public void UpdateScore(int wave, int score)
    {
        GameData.WaveNumber = wave;
        GameData.Score += score;
    }

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