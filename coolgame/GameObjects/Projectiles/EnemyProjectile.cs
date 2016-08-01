using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class EnemyProjectile : LaserProjectile
    {   
        public EnemyProjectile(ContentManager content, double x, double y, float direction, int attackPower, string assetName) : base(content, x, y, direction, attackPower)
        {
            SetTexture(assetName);
            Width = texture.Width;
            Height = texture.Height;
            speed = 2f;
            X = x - Width / 2;
            Y = y - Height / 2;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            Building target = CollisionManager.CollidesWithForcefield(this);

            if (target == null)
                target = CollisionManager.CollidesWithBuilding(this);

            if (target != null)
            {
                target.InflictDamage(attackPower);
                Alive = false;
            }
        }
    }
}
