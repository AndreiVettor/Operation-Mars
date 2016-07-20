using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public abstract class MeleeEnemy : Enemy
    {
        public MeleeEnemy(ContentManager Content) : base(Content)
        {
            
        }

        public override void Update(float deltaTime, InputManager input)
        {
            base.Update(deltaTime, input);

            attackCooldown += deltaTime;
            target = CollisionManager.CollidesWithBuilding(this);
            
            if (target == null)
            {
                if (Direction == EnemyDirection.ToLeft)
                    X -= movingSpeed / 100 * deltaTime;
                else if (Direction == EnemyDirection.ToRight)
                    X += movingSpeed / 100 * deltaTime;
            }
            else
            {
                if (attackCooldown >= 1000f / attackSpeed)
                {
                    target.InflictDamage(attackPower);
                    attackCooldown = 0;
                    if (attackSound != null)
                        SoundManager.PlayClip(attackSound);
                }
            }
        }

        public override void InflictDamage(int hitpoints)
        {
            base.InflictDamage(hitpoints);
            if (hitSound != null)
                SoundManager.PlayClip(hitSound);
        }
    }
}
