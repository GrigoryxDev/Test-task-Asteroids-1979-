using System.Collections;
using System.Collections.Generic;
using SpawnSystem;
using UnityEngine;

public class Asteroid : BaseEnemy
{
    public override void OnObjectDestroy()
    {
        var random = Random.Range(0.0f, 1.0f);
        switch (type)
        {
            case EnemiesType.Large:
                var medAstr = app.GameInitSettings.GetAsteroidSpawnChance(AsteroidsBuckshot.Med);
                if (random < medAstr.chance)
                {
                    var childNumber = Random.Range(medAstr.minAmount, medAstr.maxAmount);
                    for (int i = 0; i < childNumber; i++)
                    {
                        var asteroidMed = app.ObjectPooler.SpawnFromPool(PoolObjectsTag.Asteroid);
                        asteroidMed.GetComponent<Asteroid>().type = EnemiesType.Med;
                    }
                }
                break;
            case EnemiesType.Med:
                var smallAstr = app.GameInitSettings.GetAsteroidSpawnChance(AsteroidsBuckshot.Med);
                if (random < smallAstr.chance)
                {
                    var childNumber = Random.Range(smallAstr.minAmount, smallAstr.maxAmount);
                    for (int i = 0; i < childNumber; i++)
                    {
                        var asteroidSmall = app.ObjectPooler.SpawnFromPool(PoolObjectsTag.Asteroid);
                        asteroidSmall.GetComponent<Asteroid>().type = EnemiesType.Small;
                    }
                }
                break;
        }

        type = EnemiesType.Large;

        base.OnObjectDestroy();
    }
}
