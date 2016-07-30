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
            foreach (KeyValuePair<string, Building> b in GameManager.Buildings)
                if (b.Value.Alive && e.Collides(b.Value))
                    return b.Value;

            return null;
        }

        public static Building CollidesWithBuilding(Rectangle r)
        {
            foreach (KeyValuePair<string, Building> b in GameManager.Buildings)
                if (b.Value.Alive && b.Value.Collides(r))
                    return b.Value;

            return null;
        }

        public static Forcefield CollidesWithForcefield(Rectangle r)
        {
            if (GameManager.Buildings.ContainsKey("forcefield"))
            {
                Forcefield f = (Forcefield)GameManager.Buildings["forcefield"];
                if (f.Alive && f.Collides(r))
                    return f;
            }

            return null;
        }

        public static Forcefield CollidesWithForcefield(Entity e)
        {
            if (GameManager.Buildings.ContainsKey("forcefield"))
            {
                Forcefield f = (Forcefield)GameManager.Buildings["forcefield"];
                if (f.Alive && e.Collides(f))
                    return f;
            }

            return null;
        }

        public static Turret CollidesWithTurret(Rectangle r)
        {
            if (GameManager.Buildings.ContainsKey("leftturret"))
            {
                Turret t = (Turret)GameManager.Buildings["leftturret"];
                if (t.Alive && t.Collides(r))
                    return t;
            }

            if (GameManager.Buildings.ContainsKey("rightturret"))
            {
                Turret t = (Turret)GameManager.Buildings["rightturret"];
                if (t.Alive && t.Collides(r))
                    return t;
            }

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
