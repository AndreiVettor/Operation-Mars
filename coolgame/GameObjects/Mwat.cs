using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class Mwat : RangedEnemy
    {
        public Mwat(ContentManager Content) : base(Content)
        {
            SetTexture("enemy2");
            Width = 58;
            Height = 80;
            EnableAnimation = true;

            healthBar.MaxHealth = 50;
            movingSpeed = 9f;
            attackSpeed = 1f;
            attackPower = 30;
            precision = 10f;

            Range = GameManager.RNG.Next(100, 350);

            attackSound = "enemylaser";
        }
    }
}
