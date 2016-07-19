using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public static class CollisionManager
    {
        private static List<Entity> enemies = new List<Entity>();
        private static List<Entity> buildings = new List<Entity>();
        private static List<LaserProjectile> projectiles = new List<LaserProjectile>();

        public static List<Entity> GetEntityList()
        {
            List<Entity> newList = new List<Entity>();
            newList.AddRange(enemies);
            newList.AddRange(buildings);
            newList.AddRange(projectiles);
            return newList;
        }

        public static void AddEnemy(Entity e)
        {
            enemies.Add(e);
        }

        public static void RemoveEnemy(Entity e)
        {
            enemies.Remove(e);
        }

        public static void AddBuilding(Entity e)
        {
            buildings.Add(e);
        }

        public static void RemoveBuilding(Entity e)
        {
            buildings.Remove(e);
        }

        public static void AddProjectile(LaserProjectile e)
        {
            projectiles.Add(e);
        }

        public static void RemoveProjectile(LaserProjectile e)
        {
            projectiles.Remove(e);
        }

        public static Entity CollidesWithBuilding(Entity e)
        {
            foreach (Entity b in buildings)
                if (b.Alive && e.Collides(b))
                    return b;

            return null;
        }

        public static void Update()
        {
            for (int e = enemies.Count - 1; e >= 0; --e)
            {
                if (!enemies[e].Alive)
                {
                    enemies.RemoveAt(e);
                    continue;
                }
                for (int p = projectiles.Count - 1; p >= 0; --p)
                {
                    if (!projectiles[p].Alive)
                    {
                        projectiles.RemoveAt(p);
                        continue;
                    }
                    if (enemies[e].Collides(projectiles[p]))
                    {
                        projectiles[p].Hit(enemies[e]);
                    }

                }
            }

            for (int p = projectiles.Count - 1; p >= 0; --p)
            {
                if (!projectiles[p].Alive)
                {
                    projectiles.RemoveAt(p);
                    continue;
                }
            }
        }
    }
}
