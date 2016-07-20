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
        public PlayerProjectile(ContentManager content, double x, double y, float direction) : base(content, x, y, direction)
        {
            SetTexture("redlaser");
            Width = texture.Width;
            Height = texture.Height;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            Enemy victim = CollisionManager.CollidesWithEnemy(this);
            if (victim != null)
            {
                victim.InflictDamage(5);
                Alive = false;
            }

        }
    }
}
