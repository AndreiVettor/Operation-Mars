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

        public static bool CollidesWithGround(Entity e)
        {
            return GameManager.Ground.Collides(e);
        }

        public static Building CollidesWithBuilding(Entity e)
        {
            foreach (Building b in GameManager.Buildings)
                if (b.Alive && e.Collides(b))
                    return b;

            return null;
        }

        public static Building CollidesWithBuilding(Rectangle r)
        {
            foreach (Building b in GameManager.Buildings)
                if (b.Alive && b.Collides(r))
                    return b;

            return null;
        }

        public static Enemy CollidesWithEnemy(Entity e)
        {
            foreach (Enemy enemy in GameManager.Enemies)
                if (enemy.Alive && e.Collides(enemy))
                    return enemy;

            return null;
        }

        public static Enemy CollidesWithEnemy(Rectangle r)
        {
            foreach (Enemy enemy in GameManager.Enemies)
                if (enemy.Alive && enemy.Collides(r))
                    return enemy;

            return null;
        }
    }
}
