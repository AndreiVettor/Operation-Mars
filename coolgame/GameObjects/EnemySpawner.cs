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
        private float spawnTime;

        public EnemySpawner()
        {

        }

        public void Update(float totalGameTime, float deltaTime)
        {
            spawnTime += deltaTime;

            if (spawnTime >= 1000 / 6)
            {
                spawnTime = 0;

                //if (Roll(.01f))
                //{
                //    SpawnEnemy("crawler");
                //}
                //if (Roll(.01f))
                //{
                //    SpawnEnemy("steelroach");
                //}
                //if (Roll(.01f))
                //{
                //    SpawnEnemy("reptilian");
                //}
                //if (Roll(.01f))
                //{
                //    SpawnEnemy("reptiliansaucer");
                //}
                //if (Roll(.01f))
                //{
                //    SpawnEnemy("demolitionroverunit");
                //}
                //if (Roll(.05f))
                //{
                //    SpawnEnemy("mwat");
                //}
                //if (Roll(.00001f))
                //{
                //    SpawnEnemy("illuminati");
                //}
                if (Roll(.05f))
                {
                    SpawnEnemy("murderbot");
                }
            }
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

            int direction = GameManager.RNG.Next(2);

            if (direction == 0)
            {
                tempEnemy.X = Game.GAME_WIDTH;
                tempEnemy.Direction = Enemy.EnemyDirection.ToLeft;
            }
            else
            {
                tempEnemy.X = - tempEnemy.Width;
                tempEnemy.Direction = Enemy.EnemyDirection.ToRight;
            }

            if (enemyType != "reptiliansaucer")
                tempEnemy.Y = GameManager.Ground.Top - tempEnemy.Height;
        }       
    }
}
