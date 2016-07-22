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
        private Enemy.EnemyDirection enemyDirection;
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private float spawnTime;

        public EnemySpawner(Vector2 position, Enemy.EnemyDirection enemyDirection)
        {
            this.position = position;
            this.enemyDirection = enemyDirection;
        }

        public void Update(float totalGameTime, float deltaTime)
        {
            spawnTime += deltaTime;

            if (spawnTime >= 1000 / 6)
            {
                spawnTime = 0;

                if (Roll(GetSpawnRate(totalGameTime, 1, 1.5f)))
                {
                    SpawnEnemy("crawler");
                }
                if (Roll(GetSpawnRate(totalGameTime, 1, .25f)))
                {
                    SpawnEnemy("steelroach");
                }
                if (Roll(GetSpawnRate(totalGameTime, 1, .5f)))
                {
                    SpawnEnemy("reptilian");
                }
                if (Roll(GetSpawnRate(totalGameTime, 1, .5f)))
                {
                    SpawnEnemy("reptiliansaucer");
                }
                if (Roll(GetSpawnRate(totalGameTime, 1, .35f)))
                {
                    SpawnEnemy("demolitionroverunit");
                }
                if (Roll(GetSpawnRate(totalGameTime, 1, .5f)))
                {
                    SpawnEnemy("mwat");
                }
            }
        }

        private float GetSpawnRate(float totalGameTime, int level, float multiplier)
        {
            return (float)(Math.Pow(Math.E, - Math.Pow(totalGameTime / 200000 - 1.2f - (level - 1) * .4f, 2))) * .05f * multiplier;
        }

        private bool Roll(float chance)
        {
            int pseudoRandomNumber = GameManager.RNG.Next(100000);
            if (pseudoRandomNumber < chance * 100000)
                return true;
            return false;
        }

        public void SpawnEnemy(string enemyType)
        {
            Enemy tempEnemy = EnemyFactory.CreateEnemy(enemyType);

            if (enemyDirection == Enemy.EnemyDirection.ToLeft)
                tempEnemy.X = position.X;
            else
                tempEnemy.X = position.X - tempEnemy.Width;

            if (enemyType != "reptiliansaucer")
                tempEnemy.Y = position.Y - tempEnemy.Height;

            tempEnemy.Direction = enemyDirection;
        }       
    }
}
