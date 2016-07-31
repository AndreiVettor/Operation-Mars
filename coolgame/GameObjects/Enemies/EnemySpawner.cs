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
        private const float WAVE_DELAY = 2000f;

        private float waveLength;

        //needs tweaking
        private float[,] spawnTable = new float[,]
        {
            { .075f, .1f,   .1f,   .1f,     0,     0,   0,    0,     0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, .2f, 0 }, //crawler
            {     0,   0, .010f, .025f, .085f, .085f, .1f,    0,     0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, .2f, 0 },  //steelroach
            {     0,   0,     0,     0,     0,  .07f, .1f, .13f,     0, .125f, .125f, .125f, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, .175f, .2f, 0 }, //reptilian
            {     0,   0,     0,     0,     0,     0,   0,    0, .075f, .075f, 0, .075f, .1f, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, .15f, .175f, .2f, 0 }, //mwat
            {     0,   0,     0,     0,     0,     0,   0,    0,     0, 0, .035f, .04f, 0, .05f, .07f, .1f, .1f, 0, .1f, .11f, .12f, 0, 0, 0, .13f, .14f, .145f, .15f, .155f, 0 }, //murderbot
            {     0,   0,     0,     0,     0,     0,   0,    0,     0, 0, 0, 0, .065f, .075f, .085f, 0, .1f, 0, 0, 0, .115f, .14f, 0, 0, 0, .145f, .15f, .16f, .175f, 0}, //dru
            {     0,   0,     0,     0,     0,     0,   0,    0,     0, 0, 0, 0, 0, 0, 0, 0, 0, .1f, .1f, .115f, .115f, .13f, .13f, .135f, .135f, .14f, .145f, .15f, .16f, 0 }, //saucer
            {     0,   0,     0,     0,     0,     0,   0,    0,     0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, .025f, .035f, .045f, .055f, .065f, .075f, .085f, .1f } //saucer2
        };

        private float spawnTime;
        private float waveTime;
        private float waveDelayTime, waveDelayTimeFin;
        private int wave;
        private bool waveFinished;

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
                waveTime = 0;
                if (GameManager.GameOver == false)
                {
                    guiManager.DisplayMessage("DAY " + Wave.ToString());
                }

                float gameProgress = (wave - 1) / 30f;

                waveLength = 25000 * (1 - gameProgress) + 45000 * gameProgress;
            }
            waveFinished = false;
        }

        public void Update(float totalGameTime, float deltaTime, GUIManager guiManager, ContentManager content)
        {
            spawnTime += deltaTime;
            waveTime += deltaTime;

            if (waveTime >= waveLength)
            {
                waveFinished = true;
            }

            if (!waveFinished)
            {
                waveDelayTime += deltaTime;

                if (waveDelayTime >= WAVE_DELAY && spawnTime >= SPAWN_CYCLE)
                {
                    spawnTime = 0;

                    if (Roll(spawnTable[0, wave - 1] / 6))
                    {
                        SpawnEnemy("crawler");
                    }
                    if (Roll(spawnTable[1, wave - 1] / 6))
                    {
                        SpawnEnemy("steelroach");
                    }
                    if (Roll(spawnTable[2, wave - 1] / 6))
                    {
                        SpawnEnemy("reptilian");
                    }
                    if (Roll(spawnTable[6, wave - 1] / 6))
                    {
                        SpawnEnemy("reptiliansaucer");
                    }
                    if (Roll(spawnTable[5, wave - 1] / 6))
                    {
                        SpawnEnemy("demolitionroverunit");
                    }
                    if (Roll(spawnTable[3, wave - 1] / 6))
                    {
                        SpawnEnemy("mwat");
                    }
                    if (Roll(spawnTable[4, wave - 1] / 6))
                    {
                        SpawnEnemy("murderbot");
                    }
                    if (Roll(spawnTable[7, wave - 1] / 6))
                    {
                        SpawnEnemy("tarantularsaucer");
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
