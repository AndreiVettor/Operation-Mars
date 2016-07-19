using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame
{
    class EnemySpawner
    {
        private Random random;
        private Enemy.EnemyDirection enemyDirection;
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private float spawnTime;

        public EnemySpawner(int seed, Vector2 position, Enemy.EnemyDirection enemyDirection)
        {
            this.position = position;
            this.enemyDirection = enemyDirection;
            random = new Random(seed);
        }

        public void Update(float totalGameTime, float deltaTime)
        {
            spawnTime += deltaTime;

            if (spawnTime >= 1000 / 6)
            {
                spawnTime = 0;

                if (Roll(GetSpawnRate(totalGameTime, Crawler.level, Crawler.spawnRateMultiplier)))
                {
                    SpawnEnemy("crawler");
                }
                if (Roll(GetSpawnRate(totalGameTime, Steelroach.level, Steelroach.spawnRateMultiplier)))
                {
                    SpawnEnemy("steelroach");
                }
            }
        }

        private float GetSpawnRate(float totalGameTime, int level, float multiplier)
        {
            return (float)(Math.Pow(Math.E, - Math.Pow(totalGameTime / 200000 - 1.25f - (level - 1) * .4f, 2))) * .1f * multiplier;
        }

        private bool Roll(float chance)
        {
            int pseudoRandomNumber = random.Next(100000);
            if (pseudoRandomNumber < chance * 100000)
                return true;
            return false;
        }

        public void SpawnEnemy(string enemyType)
        {
            Enemy tempEnemy = EnemyFactory.CreateEnemy(enemyType);
            tempEnemy.X = position.X;
            tempEnemy.Y = position.Y - tempEnemy.Height;
            tempEnemy.Direction = enemyDirection;
        }       
    }
}
