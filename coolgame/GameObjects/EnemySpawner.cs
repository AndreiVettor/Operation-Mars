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

        float spawnTimer = 0;

        private float spawnRate;
        public float SpawnRate
        {
            get { return spawnRate; }
            set { spawnRate = value; }
        }


        public EnemySpawner(Vector2 position, float spawnRate, Enemy.EnemyDirection enemyDirection)
        {
            this.position = position;
            this.spawnRate = spawnRate;
            this.enemyDirection = enemyDirection;
        }

        public void Update(float deltaTime)
        {
            spawnTimer += deltaTime;

            if(spawnTimer >= SpawnRate)
            {
                spawnTimer -= SpawnRate;
                SpawnEnemy("Steve");
            }
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
