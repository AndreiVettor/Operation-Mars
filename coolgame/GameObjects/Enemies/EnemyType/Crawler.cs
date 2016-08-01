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
            Height = 39;
            EnableAnimation = true;

            AnimationSpeed /= 3;
            attackSound = "crawlerhit";
            hitSound = "crawlerhit";


            healthBar.MaxHealth = 180;
            movingSpeed = 6f;
            attackPower = 80;
            attackSpeed = 1f;

            spaceCash = 15;
        }

        public override void InflictDamage(int hitpoints)
        {
            base.InflictDamage(hitpoints);
        }
    }
}
