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
        private static List<Enemy> enemies = new List<Enemy>();
        private static List<Building> buildings = new List<Building>();
        private static List<LaserProjectile> projectiles = new List<LaserProjectile>();
        private static Ground ground;

        public static List<Entity> GetEntityList()
        {
            List<Entity> newList = new List<Entity>();
            newList.AddRange(enemies);
            newList.AddRange(buildings);
            newList.AddRange(projectiles);
            return newList;
        }

        public static void SetGround(Ground g)
        {
            ground = g;
        }

        public static void AddEnemy(Enemy e)
        {
            enemies.Add(e);
        }

        public static void RemoveEnemy(Enemy e)
        {
            enemies.Remove(e);
        }

        public static void AddBuilding(Building e)
        {
            buildings.Add(e);
        }

        public static void RemoveBuilding(Building e)
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

        public static bool CollidesWithGround(Entity e)
        {
            return ground.Collides(e);
        }

        public static Building CollidesWithBuilding(Entity e)
        {
            foreach (Building b in buildings)
                if (b.Alive && e.Collides(b))
                    return b;

            return null;
        }

        public static Enemy CollidesWithEnemy(Entity e)
        {
            foreach (Enemy enemy in enemies)
                if (enemy.Alive && e.Collides(enemy))
                    return enemy;

            return null;
        }
    }
}
