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
        public static float spawnRate;

        private Random random;
        private Enemy.EnemyDirection enemyDirection;
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        //private float spawnRate = 0.01f;
        private float spawnTime;

        public float SpawnRate
        {
            get { return spawnRate; }
            set { spawnRate = value; }
        }


        public EnemySpawner(int seed, Vector2 position, Enemy.EnemyDirection enemyDirection)
        {
            this.position = position;
            this.enemyDirection = enemyDirection;
            random = new Random(seed);
        }

        public void Update(float totalGameTime, float deltaTime)
        {
            spawnRate = (float)(Math.Pow(Math.E, - Math.Pow(totalGameTime / 200000 - 1.3f, 2))) * .02f;

            spawnTime += deltaTime;

            if (spawnTime >= 1000 / 60)
            {
                spawnTime = 0;

                if (Roll(spawnRate))
                {
                    SpawnEnemy("crawler");
                }
            }
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
            GameManager.AddEntity(tempEnemy);
        }       
    }
}
