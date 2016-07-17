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
    public class CollisionDetector
    {
        private List<Entity> enemies;
        private List<Entity> buildings;
        private List<LaserProjectile> projectiles;
        private Ground ground;

        public CollisionDetector(Ground ground)
        {
            enemies = new List<Entity>();
            buildings = new List<Entity>();
            projectiles = new List<LaserProjectile>();
            this.ground = ground;
        }

        public void AddEnemy(Entity e)
        {
            enemies.Add(e);
        }

        public void RemoveEnemy(Entity e)
        {
            enemies.Remove(e);
        }

        public void AddBuilding(Entity e)
        {
            buildings.Add(e);
        }

        public void RemoveBuilding(Entity e)
        {
            buildings.Remove(e);
        }

        public void AddProjectile(LaserProjectile e)
        {
            projectiles.Add(e);
        }

        public void RemoveProjectile(LaserProjectile e)
        {
            projectiles.Remove(e);
        }

        public void Update()
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
                        projectiles[p].Hit(enemies[e]);
                }
            }

            for (int p = projectiles.Count - 1; p >= 0; --p)
            {
                if (!projectiles[p].Alive)
                {
                    projectiles.RemoveAt(p);
                    continue;
                }
                if (ground.Collides(projectiles[p]))
                    projectiles[p].Alive = false;
            }
        }
    }
}
