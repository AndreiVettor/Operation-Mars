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
        private static List<Building> buildings = new List<Building>();
        private static List<LaserProjectile> projectiles = new List<LaserProjectile>();

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
            CollisionManager.AddEnemy(e);
        }

        public static void AddEntity(Building e)
        {
            buildings.Add(e);
            CollisionManager.AddBuilding(e);
        }

        public static void AddEntity(LaserProjectile e)
        {
            projectiles.Add(e);
            CollisionManager.AddProjectile(e);
        }

        public static void UpdateEntities(float deltaTime, InputManager input)
        {
            for(int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(deltaTime, input);
                if (!enemies[i].Alive)
                {
                    CollisionManager.RemoveEnemy(enemies[i]);
                    enemies.Remove(enemies[i]);
                }
            }

            for (int i = buildings.Count - 1; i >= 0; i--)
            {
                buildings[i].Update(deltaTime, input);
                if (!buildings[i].Alive)
                {
                    CollisionManager.RemoveBuilding(buildings[i]);
                    buildings.Remove(buildings[i]);
                }
            }

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update(deltaTime, input);
                if (!projectiles[i].Alive)
                {
                    CollisionManager.RemoveProjectile(projectiles[i]);
                    projectiles.Remove(projectiles[i]);
                }
            }
        }

        public static void DrawEntities(SpriteBatch spriteBatch)
        {
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
