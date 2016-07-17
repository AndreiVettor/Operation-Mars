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


        public EnemySpawner(Vector2 position, float spawnRate)
        {
            this.position = position;
            this.spawnRate = spawnRate;
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
            GameManager.AddEntity(tempEnemy);
            CollisionDetector.AddEnemy(tempEnemy);
            Debug.Log("Steve spawned");
        }       
    }
}
