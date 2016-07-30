using coolgame.GUI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame
{
    public class EnemySpawner
    {
        private const float SPAWN_CYCLE = 1000f / 6;

        //needs tweaking
        private float[,] spawnTable = new float[,]
        {
            { .1f, .1f, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //crawler
            { 0, .04f, .095f, 0, 0, .12f, 0, 0, 0, 0, 0 },  //steelroach
            { 0, 0, 0, .2f, .15f, .1f, 0, 0, 0, 0, 0 }, //reptilian
            { 0, 0, 0, 0, .05f, .075f, .05f, .055f, 0, .06f, 0 }, //mwat
            { 0, 0, 0, 0, 0, 0, .01f, .015f, .025f, .025f, 0 }, //murderbot
            { 0, 0, 0, 0, 0, 0, 0, .02f, 0, .03f, 0}, //dru
            { 0, 0, 0, 0, 0, 0, 0, 0, .03f, 0, .15f }, //saucer
        };

        private float spawnTime;
        private float waveTime;
        private int wave;
        private bool waveFinished;

        public EnemySpawner(GUIManager guiManager)
        {
            SetWave(7, guiManager);
        }

        public int Wave
        {
            get { return wave; }
        }

        public void SetWave(int waveNumber, GUIManager guiManager)
        {
            if (waveNumber >= 1 && waveNumber <= 11)
            {
                wave = waveNumber;
                waveTime = 0;
                if (GameManager.GameOver == false)
                {
                    guiManager.DisplayMessage("WAVE " + Wave.ToString());
                }
            }
            waveFinished = false;
        }

        public void Update(float totalGameTime, float deltaTime, GUIManager guiManager)
        {
            spawnTime += deltaTime;
            waveTime += deltaTime;

            if (waveTime >= 40000f)
            {
                waveFinished = true;
            }

            if (!waveFinished)
            {
                if (spawnTime >= SPAWN_CYCLE)
                {
                    spawnTime = 0;

                    if (Roll(spawnTable[0, wave - 1]))
                    {
                        SpawnEnemy("crawler");
                    }
                    if (Roll(spawnTable[1, wave - 1]))
                    {
                        SpawnEnemy("steelroach");
                    }
                    if (Roll(spawnTable[2, wave - 1]))
                    {
                        SpawnEnemy("reptilian");
                    }
                    if (Roll(spawnTable[6, wave - 1]))
                    {
                        SpawnEnemy("reptiliansaucer");
                    }
                    if (Roll(spawnTable[5, wave - 1]))
                    {
                        SpawnEnemy("demolitionroverunit");
                    }
                    if (Roll(spawnTable[3, wave - 1]))
                    {
                        SpawnEnemy("mwat");
                    }
                    if (Roll(spawnTable[4, wave - 1]))
                    {
                        SpawnEnemy("murderbot");
                    }
                }
            }
            else if (GameManager.Enemies.Count == 0)
            {
                SetWave(wave + 1, guiManager);
            }
        }

        private bool Roll(float chance)
        {
            if (chance > 0)
            {
                int pseudoRandomNumber = GameManager.RNG.Next(100000);
                if (pseudoRandomNumber < chance * 100000)
                    return true;
            }
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

            if (enemyType != "reptiliansaucer" && enemyType != "emag")
                tempEnemy.Y = GameManager.Ground.Top - tempEnemy.Height;
        }       
    }
}
