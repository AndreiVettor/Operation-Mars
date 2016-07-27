using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace coolgame
{
    public class Mwat : RangedEnemy
    {
        public Mwat(ContentManager Content) : base(Content)
        {
            SetTexture("mwat_shooting");
            Width = 46;
            Height = 75;
            EnableAnimation = true;
            AnimationSpeed = 100;

            healthBar.MaxHealth = 80;
            movingSpeed = 9f;
            attackSpeed = 1.5f;
            attackPower = 10;
            burstFire = true;
            burstFireAmmount = 3;
            burstFireSpeed = 10f;
            precision = ((float)GameManager.RNG.NextDouble() * 10f + 10f);

            Range = GameManager.RNG.Next(100, 250);

            attackSound = "enemylaser";
        }
    }
}
