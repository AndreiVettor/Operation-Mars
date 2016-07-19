using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class Crawler : Enemy
    {
        private Entity target;
        private float attackCooldown;

        public Crawler(ContentManager Content) : base(Content)
        {
            SetTexture(Content, "crawler");
            Width = 64;
            Height = 64;
            EnableAnimation = true;
            healthBar.MaxHealth = 20;
            AnimationSpeed /= 3;
        }

        protected override EnemyDirection SpriteDirection
        {
            get
            {
                return EnemyDirection.ToLeft;
            }
        }

        public override void Update(float deltaTime, InputManager input)
        {
            base.Update(deltaTime, input);

            attackCooldown += deltaTime;

            target = CollisionManager.CollidesWithBuilding(this);

            if (target == null)
            {
                if (Direction == EnemyDirection.ToLeft)
                    X -= 0.033f * deltaTime;
                else if (Direction == EnemyDirection.ToRight)
                    X += 0.033f * deltaTime;
            }
            else
            {
                if (attackCooldown >= 1000)
                {
                    target.InflictDamage(10);
                    attackCooldown = 0;
                }
            }
            
        }
    }
}
