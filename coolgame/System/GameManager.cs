using Microsoft.Xna.Framework;
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
        // VARIABLES
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

        private static List<Building> buildings = new List<Building>();
        public static List<Building> Buildings
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

        public static int laser_speed = 1;
        public static int laser_damage = 1;
        public static int laser_spread = 1;


        //METHODS
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
        public static void AddEntity(Building e)
        {
            buildings.Add(e);
        }
        public static void AddEntity(LaserProjectile e)
        {
            projectiles.Add(e);
        }

        public static List<Entity> GetEntityList()
        {
            List<Entity> temp = new List<Entity>();
            temp.AddRange(enemies);
            temp.AddRange(buildings);
            temp.AddRange(projectiles);
            return temp;
        }

        private static void ClearEntities()
        {
            enemies.Clear();
            projectiles.Clear();
        }

        private static void ClearUpgrades()
        {
            laser_speed = 1;
            laser_damage = 1;
            laser_spread = 1;
        }

        public static void Restart()
        {
            ClearEntities();
            ClearUpgrades();
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

            for (int i = buildings.Count - 1; i >= 0; i--)
            {
                if (buildings[i].Alive)
                {
                    buildings[i].Update(deltaTime);
                }
                else
                {
                    buildings.Remove(buildings[i]);
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
                foreach (Building e in buildings)
                {
                    e.Draw(spriteBatch);
                }
                foreach (LaserProjectile e in projectiles)
                {
                    e.Draw(spriteBatch);
                }
            }
        }
    }
}
