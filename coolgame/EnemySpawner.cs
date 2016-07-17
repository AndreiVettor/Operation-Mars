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

        public EnemySpawner(Vector2 position)
        {
            this.position = position;
        }

        public void SpawnEnemy(string enemyType)
        {
            Enemy tempEnemy = EnemyFactory.CreateEnemy(enemyType);
            tempEnemy.X = position.X;
            tempEnemy.Y = position.Y;
            GameManager.AddEntity(tempEnemy);
            CollisionDetector.AddEnemy(tempEnemy);
        }       
    }
}
