using System.Collections;
using System.Collections.Generic;
using Scripts.Game;
using SpawnSystem;
using UnityEngine;

namespace Scripts.Enemies
{
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

                        AsteroidBuckShot(childNumber, EnemiesType.Med);
                    }
                    break;
                case EnemiesType.Med:
                    var smallAstr = app.GameInitSettings.GetAsteroidSpawnChance(AsteroidsBuckshot.Med);
                    if (random < smallAstr.chance)
                    {
                        var childNumber = Random.Range(smallAstr.minAmount, smallAstr.maxAmount);
                        AsteroidBuckShot(childNumber, EnemiesType.Small);
                    }
                    break;
            }

            type = EnemiesType.Large;

            base.OnObjectDestroy();
        }

        private void AsteroidBuckShot(int childNumber, EnemiesType type)
        {
            for (int i = 0; i < childNumber; i++)
            {
                var asteroidMed = app.ObjectPooler.SpawnFromPool(PoolObjectsTag.Asteroid);
                var asteroid = asteroidMed.GetComponent<Asteroid>();
                asteroid.type = type;
                asteroidMed.transform.position = transform.position;

                asteroid.OnObjectSpawn();
            }

        }
    }
}