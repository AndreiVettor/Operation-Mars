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
        public Crawler(ContentManager Content) : base(Content)
        {
            SetTexture(Content, "crawler");
            Width = 64;
            Height = 64;
            EnableAnimation = true;
            healthBar.MaxHealth = 20;
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
            if (Direction == EnemyDirection.ToLeft)
                X -= 0.1f * deltaTime;
            else if (Direction == EnemyDirection.ToRight)
                X += 0.1f * deltaTime;
        }
    }
}
