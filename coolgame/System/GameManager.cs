using coolgame.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static int spaceCash = 100;
        public static int SpaceCash
        {
            get { return spaceCash; }
            set { spaceCash = value; }
        }

        private static GameState state = GameState.Game;
        public static GameState State
        {
            get { return state; }
            set { state = value; }
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

        private static bool godMode;
        public static bool GodMode
        {
            get { return godMode; }
            set { godMode = value; }
        }

        private static bool frameLimiting;
        public static bool FrameLimiting
        {
            get { return frameLimiting; }
        }
        private static bool vSync;

        #endregion

        #region methods

        #region upgrade_system

        public static void UpgradeLaserPower()
        {
            if (buildings.ContainsKey("base"))
                ((Base)buildings["base"]).Gun.AttackPowerLevel++;
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

        public static void UpgradeTurretPower(bool left)
        {
            if (left)
            {
                if (buildings.ContainsKey("leftturret"))
                    ((Turret)buildings["leftturret"]).Gun.AttackPowerLevel++;
            }
            else
            {
                if (buildings.ContainsKey("rightturret"))
                    ((Turret)buildings["rightturret"]).Gun.AttackPowerLevel++;
            }
        }

        public static void UpgradeTurretSpeed(bool left)
        {
            if (left)
            {
                if (buildings.ContainsKey("leftturret"))
                    ((Turret)buildings["leftturret"]).Gun.SpeedLevel++;
            }
            else
            {
                if (buildings.ContainsKey("rightturret"))
                    ((Turret)buildings["rightturret"]).Gun.SpeedLevel++;
            }
        }

        public static void UpgradeTurretSpread(bool left)
        {
            if (left)
            {
                if (buildings.ContainsKey("leftturret"))
                    ((Turret)buildings["leftturret"]).Gun.SpreadLevel++;
            }
            else
            {
                if (buildings.ContainsKey("rightturret"))
                    ((Turret)buildings["rightturret"]).Gun.SpreadLevel++;
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

        #endregion

        public static void TogglePause()
        {
            if (state == GameState.Paused)
            {
                state = GameState.Game;
            }
            else if (state == GameState.Game)
            {
                state = GameState.Paused;
            }
            Debug.Log("Toggled Pause");
        }

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

        public static void Restart(ContentManager Content)
        {
            ClearEntities();
            ClearUpgrades();
            ResetBuildings(Content);
            UIManager.Reset();
            spaceCash = 100;
        }

        public static void Update(float deltaTime)
        {
            if(state == GameState.Game)
            {
                UpdateEntities(deltaTime);
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
