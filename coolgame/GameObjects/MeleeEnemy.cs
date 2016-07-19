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
        private Building target;
        private float attackCooldown;
        protected float movingSpeed;
        protected float attackSpeed;
        protected int attackPower;
        protected string attackSound;

        public MeleeEnemy(ContentManager Content) : base(Content)
        {
            movingSpeed = 1f;
            attackSpeed = 1f;
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
                    SoundManager.PlayClip(attackSound);
                    attackCooldown = 0;
                }
            }
        }
    }
}
