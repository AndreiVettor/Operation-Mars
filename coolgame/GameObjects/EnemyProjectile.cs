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
        private int attackPower;

        public EnemyProjectile(ContentManager content, double x, double y, float direction, int attackPower) : base(content, x, y, direction)
        {
            SetTexture("testlaser");
            Width = texture.Width;
            Height = texture.Height;
            this.attackPower = attackPower;
            speed = 1f;
            X = x - Width / 2;
            Y = y - Height / 2;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            Building target = CollisionManager.CollidesWithBuilding(this);
            if (target != null)
            {
                target.InflictDamage(attackPower);
                Alive = false;
            }
        }
    }
}
