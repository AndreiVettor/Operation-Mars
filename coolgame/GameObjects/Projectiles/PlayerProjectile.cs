using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class PlayerProjectile : LaserProjectile
    {
        private bool byTurret;

        public PlayerProjectile(ContentManager content, double x, double y, float direction, int attackPower, int powerLevel, bool byTurret) : base(content, x, y, direction, attackPower)
        {
            if (powerLevel > 4)
                powerLevel = 4;

            SetTexture("laser" + powerLevel.ToString());

            Width = texture.Width;
            Height = texture.Height;
            X = x - Width / 2;
            Y = y - Height / 2;
            speed = 3;

            if (!byTurret)
                GameManager.projectilesShot++;

            this.byTurret = byTurret;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            Enemy victim = CollisionManager.CollidesWithEnemy(this);
            if (victim != null)
            {
                if (!byTurret)
                    GameManager.landedHits++;

                victim.InflictDamage(attackPower);
                Alive = false;
            }

        }
    }
}
