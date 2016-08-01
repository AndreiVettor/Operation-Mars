using coolgame.GUI;
using coolgame.GUI.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace coolgame
{
    public enum GameState
    {
        StartMenu,
        Game,
        Paused,
    }

    public static class GameManager
    {
        #region variables

        public static int accuracyBonus = 20;

        private static Random random = new Random();
        public static Random RNG
        {
            get { return random; }
        }

        private static List<Enemy> enemies = new List<Enemy>();
        public static List<Enemy> Enemies
        {
            get { return enemies; }
        }

        private static Dictionary<string, Building> buildings = new Dictionary<string, Building>();
        public static Dictionary<string, Building> Buildings
        {
            get { return buildings; }
        }

        private static List<LaserProjectile> projectiles = new List<LaserProjectile>();
        public static List<LaserProjectile> Projectiles
        {
            get { return projectiles; }
        }

        private static int spaceCash = 0;
        public static int SpaceCash
        {
            get { return spaceCash; }
            set { spaceCash = value; }
        }

        public static int projectilesShot;
        public static int landedHits;

        private static float resumeGameDelay;
        private static bool resumingGame = false;

        private static GameState state = GameState.Game;
        public static GameState State
        {
            get { return state; }
            set
            {
                switch (value)
                {
                    case GameState.Game:
                        {
                            resumeGameDelay = 0;
                            resumingGame = true;
                            break;
                        }
                    case GameState.Paused:
                        {
                            state = value;
                            resumingGame = false;
                            break;
                        }
                    case GameState.StartMenu:
                        {
                            SoundManager.PlayMenuMusic();
                            state = value;
                            resumingGame = false;
                            break;
                        }
                }
            }
        }

        private static bool gameOver;
        public static bool GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
        }

        private static Ground ground;
        public static Ground Ground
        {
            set { ground = value; }
            get { return ground; }
        }

        private static Texture2D background;
        public static Texture2D Background
        {
            get { return background; }
            set { background = value; }
        }

        private static Texture2D startBackground;
        public static Texture2D StartBackground
        {
            get { return startBackground; }
            set { startBackground = value; }
        }

        public static int godMode = 0;

        private static bool frameLimiting;
        public static bool FrameLimiting
        {
            get { return frameLimiting; }
        }
        private static bool vSync;

        #endregion

        #region methods

        #region upgrade_system

        public static int[,] upgradeCosts = new int[,] {
            { 90, 180, 540 }, //laser power
            { 80, 160, 480 }, //laser speed
            { 90, 180, 540 }, //laser spread
            { 80, 180, 400 }, //forcefield regen
            { 70, 140, 350 }, //forcefield health
            { 70, 140, 350 }, //base health
            { 40, 100, 200 }, //turret health
          };

        public static int[] buildCosts = new int[] {
            130, //forcefield
            65, //turret
        };

        public static int lTurretRepairCost
        {
            get { return (int)(buildings["leftturret"].Damage * 50); }
        }

        public static int rTurretRepairCost
        {
            get { return (int)(buildings["rightturret"].Damage * 50); }
        }

        public static int baseRepairCost
        {
            get { return (int)(buildings["base"].Damage * 40); }
        }

        public static int GetUpgradeCost(int id)
        {
            switch(id)
            {
                case 0:
                    {
                        if (((Base)buildings["base"]).Gun.AttackPowerLevel < 4)
                            return upgradeCosts[0, ((Base)buildings["base"]).Gun.AttackPowerLevel - 1];
                        break;
                    }
                case 1:
                    {
                        if (((Base)buildings["base"]).Gun.SpeedLevel < 4)
                            return upgradeCosts[1, ((Base)buildings["base"]).Gun.SpeedLevel - 1];
                        break;
                    }
                case 2:
                    {
                        if (((Base)buildings["base"]).Gun.SpreadLevel < 4)
                            return upgradeCosts[2, ((Base)buildings["base"]).Gun.SpreadLevel - 1];
                        break;
                    }
                case 3:
                    {
                        if (((Forcefield)buildings["forcefield"]).RechargeLevel < 4)
                        {
                            if (((Forcefield)buildings["forcefield"]).Activated)
                                return upgradeCosts[3, ((Forcefield)buildings["forcefield"]).RechargeLevel - 1];
                            else
                                return int.MaxValue - 1;
                        }
                        break;
                    }
                case 4:
                    {
                        if (((Forcefield)buildings["forcefield"]).StrengthLevel < 4)
                        {
                            if (((Forcefield)buildings["forcefield"]).Activated)
                                return upgradeCosts[3, ((Forcefield)buildings["forcefield"]).StrengthLevel - 1];
                            else
                                return int.MaxValue - 1;
                        }
                        break;
                    }
                case 5:
                    {
                        if (((Base)buildings["base"]).HealthLevel < 4)
                            return upgradeCosts[5, ((Base)buildings["base"]).HealthLevel - 1];
                        break;
                    }
                case 6:
                    {
                        if (((Turret)buildings["leftturret"]).HealthLevel < 4)
                        {
                            if (buildings["leftturret"].Alive)
                                return upgradeCosts[6, ((Turret)buildings["leftturret"]).HealthLevel - 1];
                            else
                                return int.MaxValue - 1;
                        }
                        break;
                    }
                case 7:
                    {
                        if (((Turret)buildings["rightturret"]).HealthLevel < 4)
                        {
                            if (buildings["rightturret"].Alive)
                                return upgradeCosts[6, ((Turret)buildings["rightturret"]).HealthLevel - 1];
                            else
                                return int.MaxValue - 1;
                        }
                        break;
                    }
                case 8:
                    {
                        if (!((Forcefield)buildings["forcefield"]).Activated)
                            return buildCosts[0];
                        break;
                    }
                case 9:
                    {
                        if (!((Turret)buildings["leftturret"]).Alive)
                            return buildCosts[1];
                        break;
                    }
                case 10:
                    {
                        if (!((Turret)buildings["rightturret"]).Alive)
                            return buildCosts[1];
                        break;
                    }
                case 11:
                    {
                        return baseRepairCost;
                    }
                case 12:
                    {
                        if (buildings["leftturret"].Alive)
                            return lTurretRepairCost;
                        break;
                    }
                case 13:
                    {
                        if (buildings["rightturret"].Alive)
                            return rTurretRepairCost;
                        break;
                    }
                default:
                    return 0;
            }

            return int.MaxValue;
        }

        public static void ApplyUpgrade(int id)
        {
            int cost;

            if (godMode > 0)
                cost = 0;
            else
                cost = GetUpgradeCost(id);

            if (cost > spaceCash)
                return;

            spaceCash -= cost;

            switch (id)
            {
                case 0:
                    {
                        UpgradeLaserPower();
                        UpgradeTurretPower();
                        break;
                    }
                case 1:
                    {
                        UpgradeLaserSpeed();
                        UpgradeTurretSpeed();
                        break;
                    }
                case 2:
                    {
                        UpgradeLaserSpread();
                        UpgradeTurretSpread();
                        break;
                    }
                case 3:
                    {
                        UpgradeForcefieldRecharge();
                        break;
                    }
                case 4:
                    {
                        UpgradeForcefieldStrength();
                        break;
                    }
                case 5:
                    {
                        UpgradeBaseHealth();
                        break;
                    }
                case 6:
                    {
                        UpgradeTurretHealth(true);
                        break;
                    }
                case 7:
                    {
                        UpgradeTurretHealth(false);
                        break;
                    }
                case 8:
                    {
                        ActivateForcefield();
                        break;
                    }
                case 9:
                    {
                        ActivateTurret(true);
                        break;
                    }
                case 10:
                    {
                        ActivateTurret(false);
                        break;
                    }
                case 11:
                    {
                        RepairBuilding("base");
                        break;
                    }
                case 12:
                    {
                        RepairBuilding("leftturret");
                        break;
                    }
                case 13:
                    {
                        RepairBuilding("rightturret");
                        break;
                    }
                default:
                    {
                        Debug.Log("Invalid upgrade ID: " + id.ToString());
                        break;
                    }
            }
        }

        public static void ActivateForcefield()
        {
            if (buildings.ContainsKey("forcefield") && !buildings["forcefield"].Alive)
            {
                ((Forcefield)buildings["forcefield"]).Alive = true;
                ((Forcefield)buildings["forcefield"]).Activated = true;
            } 
        }

        public static void ActivateTurret(bool left)
        {
            if (left)
            {
                if (buildings.ContainsKey("leftturret") && !buildings["leftturret"].Alive)
                    ((Turret)buildings["leftturret"]).Alive = true;
            }
            else
            {
                if (buildings.ContainsKey("rightturret") && !buildings["rightturret"].Alive)
                    ((Turret)buildings["rightturret"]).Alive = true;
            }
        }

        public static void UpgradeLaserPower()
        {
            if (buildings.ContainsKey("base"))
            {
                ((Base)buildings["base"]).Gun.AttackPowerLevel++;
            }
        }

        public static void UpgradeLaserSpeed()
        {
            if (buildings.ContainsKey("base"))
                ((Base)buildings["base"]).Gun.SpeedLevel++;
        }

        public static void UpgradeLaserSpread()
        {
            if (buildings.ContainsKey("base"))
                ((Base)buildings["base"]).Gun.SpreadLevel++;
        }

        public static void UpgradeTurretPower()
        {
            if (buildings.ContainsKey("leftturret"))
                ((Turret)buildings["leftturret"]).Gun.AttackPowerLevel++;
            if (buildings.ContainsKey("rightturret"))
                ((Turret)buildings["rightturret"]).Gun.AttackPowerLevel++;
        }

        public static void UpgradeTurretSpeed()
        {
            if (buildings.ContainsKey("leftturret"))
                ((Turret)buildings["leftturret"]).Gun.SpeedLevel++;
            if (buildings.ContainsKey("rightturret"))
                ((Turret)buildings["rightturret"]).Gun.SpeedLevel++;
        }

        public static void UpgradeTurretSpread()
        {
            if (buildings.ContainsKey("leftturret"))
                ((Turret)buildings["leftturret"]).Gun.SpreadLevel++;
            if (buildings.ContainsKey("rightturret"))
                ((Turret)buildings["rightturret"]).Gun.SpreadLevel++;
        }

        public static void UpgradeTurretHealth(bool left)
        {
            if (left)
            {
                if (buildings.ContainsKey("leftturret"))
                    ((Turret)buildings["leftturret"]).HealthLevel++;
            }
            else
            {
                if (buildings.ContainsKey("rightturret"))
                    ((Turret)buildings["rightturret"]).HealthLevel++;
            }
        }

        public static void UpgradeForcefieldRecharge()
        {
            if (buildings.ContainsKey("forcefield"))
                ((Forcefield)buildings["forcefield"]).RechargeLevel++;
        }

        public static void UpgradeForcefieldStrength()
        {
            if (buildings.ContainsKey("forcefield"))
                ((Forcefield)buildings["forcefield"]).StrengthLevel++;
        }

        public static void UpgradeBaseHealth()
        {
            if (buildings.ContainsKey("base"))
                ((Base)buildings["base"]).HealthLevel++;
        }

        public static void RepairBuilding(string tag)
        {
            buildings[tag].Reset();
        }

        #endregion

        public static void SetFrameLimiting(Game game, bool value)
        {
            frameLimiting = value;
            game.IsFixedTimeStep = value;
        }
        public static void SetVSync(GraphicsDeviceManager graphics, bool value)
        {
            vSync = value;
            graphics.SynchronizeWithVerticalRetrace = value;
        }

        public static void AddEntity(Enemy e)
        {
            enemies.Add(e);
        }
        public static void AddEntity(Base e)
        {
            if (buildings.ContainsKey("base"))
                buildings.Remove("base");

            buildings.Add("base", e);
        }
        public static void AddEntity(Forcefield e)
        {
            if (buildings.ContainsKey("forcefield"))
                buildings.Remove("forcefield");

            buildings.Add("forcefield", e);
        }
        public static void AddEntity(Turret e, bool left)
        {
            if (left)
            {
                if (buildings.ContainsKey("leftturret"))
                    buildings.Remove("leftturret");
                buildings.Add("leftturret", e);
            }
            else
            {
                if (buildings.ContainsKey("rightturret"))
                    buildings.Remove("rightturret");
                buildings.Add("rightturret", e);
            }
        }
        public static void AddEntity(LaserProjectile e)
        {
            projectiles.Add(e);
        }

        public static List<Entity> GetEntityList()
        {
            List<Entity> temp = new List<Entity>();
            temp.AddRange(enemies);
            temp.AddRange(buildings.Values);
            temp.AddRange(projectiles);
            return temp;
        }

        private static void ClearEntities()
        {
            enemies.Clear();
            projectiles.Clear();
        }

        private static void ResetBuildings(ContentManager Content)
        {
            buildings.Clear();
            AddEntity(new Base(Content, Ground.Top));
            AddEntity(new Turret(Content, Ground.Top, Enemy.EnemyDirection.ToLeft), true);
            AddEntity(new Turret(Content, Ground.Top, Enemy.EnemyDirection.ToRight), false);
            AddEntity(new Forcefield(Content, Ground.Top));
        }

        public static void Restart(ContentManager Content, GUIManager guiManager, EnemySpawner spawner)
        {
            ClearEntities();
            ResetBuildings(Content);
            spaceCash = 0;
            guiManager.Restart();
            spawner.SetWave(1, guiManager);
            SoundManager.PlayMusic();
        }

        public static void Update(float deltaTime, ContentManager Content, GUIManager guiManager, EnemySpawner enemySpawner)
        {
            if (resumingGame)
            {
                resumeGameDelay += deltaTime;
                if (resumeGameDelay >= 100)
                {
                    resumeGameDelay = 0;
                    state = GameState.Game;
                }
            }

            if(state == GameState.Game)
            {
                UpdateEntities(deltaTime);
            }

            if (GameOver)
            {
                if (!guiManager.WindowOpen(typeof(GameOverWindow)))
                {
                    guiManager.AddWindow(new GameOverWindow(Content, guiManager, enemySpawner));
                    guiManager.DisplayMessage("GAME OVER!", 0);
                }
            }
        }

        public static void UpdateEntities(float deltaTime)
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].LayerDepth = LayerManager.GetLayerDepth(Layer.Enemies) + i * .00001f;
                if (enemies[i].Alive)
                {
                    enemies[i].Update(deltaTime);
                }
                else
                {
                    enemies.Remove(enemies[i]);
                }
            }

            foreach (KeyValuePair<string, Building> b in buildings)
            {
                if (b.Value.Alive)
                {
                    b.Value.Update(deltaTime);
                }
            }

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                if (projectiles[i].Alive)
                {
                    projectiles[i].Update(deltaTime);
                }
                else
                {
                    projectiles.Remove(projectiles[i]);
                }
            }
        }

        public static void DrawEntities(SpriteBatch spriteBatch)
        {
            if (state == GameState.StartMenu)
            {
                spriteBatch.Draw(startBackground, Vector2.Zero, Color.White);
            }
            else
            {
                spriteBatch.Draw(background,
                    new Rectangle(0, 0, Game.GAME_WIDTH, Game.GAME_HEIGHT),
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    LayerManager.GetLayerDepth(Layer.Background));
                ground.Draw(spriteBatch);

                foreach (Enemy e in enemies)
                {
                    e.Draw(spriteBatch);
                }
                foreach (KeyValuePair<string, Building> b in buildings)
                {
                    if (b.Value.Alive)
                    {
                        b.Value.Draw(spriteBatch);
                    }
                }
                foreach (LaserProjectile e in projectiles)
                {
                    e.Draw(spriteBatch);
                }
            }
        }

        #endregion
    }
}
