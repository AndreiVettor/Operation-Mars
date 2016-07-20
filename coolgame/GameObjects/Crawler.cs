using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class Crawler : MeleeEnemy
    {
        public new static float spawnRateMultiplier = 1f;
        public new static int level = 1;

        public Crawler(ContentManager Content) : base(Content)
        {
            SetTexture(Content, "crawler");
            Width = 64;
            Height = 64;
            EnableAnimation = true;
            healthBar.MaxHealth = 20;
            AnimationSpeed /= 3;
            movingSpeed = 3.3f;
            attackSpeed = 1f;
            attackSound = "crawlerhit";
            hitSound = "crawlerhit";
            attackPower = 10;
        }

        protected override EnemyDirection SpriteDirection
        {
            get
            {
                return EnemyDirection.ToLeft;
            }
        }

        public override void InflictDamage(int hitpoints)
        {
            base.InflictDamage(hitpoints);
        }
    }
}
