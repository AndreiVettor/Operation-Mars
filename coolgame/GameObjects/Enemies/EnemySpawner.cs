using coolgame.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        private const float WAVE_DELAY = 0;

        private int[,] spawnTable = new int[,]
        {
            {20,22,24,16,12,  8, 6, 4, 2, 2,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0}, //Crawler
            { 0, 2, 4, 6, 8, 10,12,14,16,18, 16,14,12,12,12,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0}, //SteelRoach
            { 0, 0, 0, 0, 0,  4,10,14,18,20, 18,20,18,18,16, 16,14,12,10, 8,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0}, //Reptilian
            { 0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  4,10,14,18,22, 22,24,26,28,30, 16,13,11,10, 6,  6, 7, 8, 9,10}, //MWAT
            { 0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  2, 4, 6, 8,10, 20,22,24,26,30, 10,10,10,10,10}, //Murderbot
            { 0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  2, 4, 6,12,16, 16,18,20,22,24}, //DRU
            { 0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  1, 5,11,15,17, 21,23,27,30,25, 17,11, 3, 0, 0}, //Saucer
            { 0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  1, 7,25,30,35}, //BlackSaucer
            { 0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  0, 0, 0, 0, 0,  0, 0, 0, 0, 1}  //Mother ship
        };

        private float[] spawnChances = new float[9];
        private int[] spawned = new int[9];

        private float[] totalSpawnChance = new float[]
        { 0.98f, 0.89f, 0.99f, 0.95f, 1.15f,
            1.60f, 1.70f, 1.80f, 1.90f, 1.95f,
            1.55f, 1.80f, 1.80f, 1.90f, 1.92f,
            1.65f, 1.85f, 1.80f, 1.80f, 1.90f,
            1.60f, 1.80f, 1.80f, 1.90f, 1.95f,
            1.60f, 1.80f, 1.80f, 1.90f, 1.95f};

        private float spawnTime;
        private float waveDelayTime, waveDelayTimeFin;
        private int wave;
        private bool waveFinished;

        private int enemiesSpawned;
        private int enemiesToSpawn;

        private float timeSinceLastSpawn;

        public EnemySpawner(GUIManager guiManager)
        {
            SetWave(1, guiManager);
        }

        public int Wave
        {
            get { return wave; }
        }

        public void SetWave(int waveNumber, GUIManager guiManager)
        {
            if (waveNumber >= 1 && waveNumber <= 30)
            {
                wave = waveNumber;
                if (GameManager.GameOver == false)
                {
                    guiManager.DisplayMessage("DAY " + Wave.ToString());
                }

                float gameProgress = (wave - 1) / 30f;

                waveFinished = false;

                int totalEnemies = 0;

                for (int i = 0; i < spawnChances.Length; ++i)
                    totalEnemies += spawnTable[i, waveNumber - 1];

                for (int i = 0; i < spawnChances.Length; ++i)
                    spawnChances[i] = (float)spawnTable[i, waveNumber - 1] / totalEnemies;

                enemiesToSpawn = totalEnemies;
                enemiesSpawned = 0;

                for (int i = 0; i < spawned.Length; ++i)
                    spawned[i] = 0;
            }
        }

        public void Update(float totalGameTime, float deltaTime, GUIManager guiManager, ContentManager content)
        {
            spawnTime += deltaTime;

            if (enemiesSpawned == enemiesToSpawn)
            {
                waveFinished = true;
            }

            if (!waveFinished)
            {
                waveDelayTime += deltaTime;

                if (waveDelayTime >= WAVE_DELAY)
                    timeSinceLastSpawn += deltaTime;

                if (waveDelayTime >= WAVE_DELAY && spawnTime >= SPAWN_CYCLE)
                {
                    spawnTime = 0;
                    bool spawnedNow = false;
                    bool forceSpawn = false;
                    bool triedToSpawn = true;

                    if (timeSinceLastSpawn / 1000 > 1 / totalSpawnChance[wave - 1] * 2f)
                        forceSpawn = true;

                    if (timeSinceLastSpawn / 1000 < 1 / totalSpawnChance[wave - 1] * 0.5f)
                        return;

                    while (forceSpawn || triedToSpawn)
                    {
                        triedToSpawn = false;

                        if (Roll(spawnChances[0] * totalSpawnChance[wave - 1] / 6) && spawned[0] < spawnTable[0, wave - 1])
                        {
                            SpawnEnemy("crawler");
                            spawned[0]++;
                            spawnedNow = true;
                        }
                        else if (Roll(spawnChances[1] * totalSpawnChance[wave - 1] / 6) && spawned[1] < spawnTable[1, wave - 1])
                        {
                            SpawnEnemy("steelroach");
                            spawned[1]++;
                            spawnedNow = true;
                        }
                        else if (Roll(spawnChances[2] * totalSpawnChance[wave - 1] / 6) && spawned[2] < spawnTable[2, wave - 1])
                        {
                            SpawnEnemy("reptilian");
                            spawned[2]++;
                            spawnedNow = true;
                        }
                        else if (Roll(spawnChances[6] * totalSpawnChance[wave - 1] / 6) && spawned[6] < spawnTable[6, wave - 1])
                        {
                            SpawnEnemy("reptiliansaucer");
                            spawned[6]++;
                            spawnedNow = true;
                        }
                        else if (Roll(spawnChances[5] * totalSpawnChance[wave - 1] / 6) && spawned[5] < spawnTable[5, wave - 1])
                        {
                            SpawnEnemy("demolitionroverunit");
                            spawned[5]++;
                            spawnedNow = true;
                        }
                        else if (Roll(spawnChances[3] * totalSpawnChance[wave - 1] / 6) && spawned[3] < spawnTable[3, wave - 1])
                        {
                            SpawnEnemy("mwat");
                            spawned[3]++;
                            spawnedNow = true;
                        }
                        else if (Roll(spawnChances[4] * totalSpawnChance[wave - 1] / 6) && spawned[4] < spawnTable[4, wave - 1])
                        {
                            SpawnEnemy("murderbot");
                            spawned[4]++;
                            spawnedNow = true;
                        }
                        else if (Roll(spawnChances[7] * totalSpawnChance[wave - 1] / 6) && spawned[7] < spawnTable[7, wave - 1])
                        {
                            SpawnEnemy("tarantularsaucer");
                            spawned[7]++;
                            spawnedNow = true;
                        }

                        if (spawnedNow)
                        {
                            //Debug.Log("Time since last spawn: " + (timeSinceLastSpawn / 1000).ToString());
                            enemiesSpawned++;
                            timeSinceLastSpawn = 0;
                            forceSpawn = false;
                        }
                    }
                }
            }
            else if (GameManager.Enemies.Count == 0)
            {
                waveDelayTime = 0;
                waveDelayTimeFin += deltaTime;
                if (waveDelayTimeFin >= WAVE_DELAY)
                {
                    waveDelayTimeFin = 0;
                    guiManager.AddWindow(new GUI.Menus.UpgradeMenu(content, guiManager, this));
                    GameManager.State = GameState.Paused;
                    SetWave(wave + 1, guiManager);
                    GameManager.projectilesShot = 0;
                    GameManager.landedHits = 0;
                    if (((Forcefield)GameManager.Buildings["forcefield"]).Activated)
                        GameManager.Buildings["forcefield"].Alive = true;
                }
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

            if (enemyType != "reptiliansaucer" && enemyType != "emag" && enemyType != "tarantularsaucer")
                tempEnemy.Y = GameManager.Ground.Top - tempEnemy.Height;
        }       
    }
}
