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

        private float spawnRate = 0.01f;
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

        public void Update(float deltaTime)
        {
            if(Roll(spawnRate))
            {
                SpawnEnemy("Steve");
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
            tempEnemy.Y = position.Y;
            tempEnemy.Direction = enemyDirection;
            GameManager.AddEntity(tempEnemy);
            CollisionManager.AddEnemy(tempEnemy);
        }       
    }
}
