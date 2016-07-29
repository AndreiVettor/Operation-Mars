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
        public Crawler(ContentManager Content) : base(Content)
        {
            SetTexture("crawler");
            Width = 64;
            Height = 64;
            EnableAnimation = true;
            healthBar.MaxHealth = 20;
            AnimationSpeed /= 3;
            movingSpeed = 5.3f;
            attackSpeed = 1f;
            attackSound = "crawlerhit";
            hitSound = "crawlerhit";
            attackPower = 10;
        }

        public override void InflictDamage(int hitpoints)
        {
            base.InflictDamage(hitpoints);
        }
    }
}
