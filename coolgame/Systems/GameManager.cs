using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame
{
    public static class GameManager
    {
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

        private static bool gamePaused = false;
        public static bool GamePaused
        {
            set { gamePaused = value; }
            get { return gamePaused; }
        }

        private static Ground ground;
        public static Ground Ground
        {
            set { ground = value; }
            get { return ground; }
        }

        private static bool frameLimiting;
        public static bool FrameLimiting
        {
            get { return frameLimiting; }
            set { frameLimiting = value; }
        }
        
        public static void ToggleFrameLimiting(Game game)
        {
            frameLimiting = !frameLimiting;
            game.IsFixedTimeStep = frameLimiting;
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

        public static void UpdateEntities(float deltaTime, InputManager input)
        {
            if (!gamePaused)
            {
                for (int i = enemies.Count - 1; i >= 0; i--)
                {
                    if (enemies[i].Alive)
                    {
                        enemies[i].Update(deltaTime, input);
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
                        buildings[i].Update(deltaTime, input);
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
                        projectiles[i].Update(deltaTime, input);
                    }
                    else
                    {
                        projectiles.Remove(projectiles[i]);
                    }
                }
            }
        }

        public static void DrawEntities(SpriteBatch spriteBatch)
        {
            ground.Draw(spriteBatch);

            foreach(Enemy e in enemies)
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
