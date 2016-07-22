using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class Illuminati : RangedEnemy
    {
        public Illuminati(ContentManager Content) : base(Content)
        {
            SetTexture("illuminati");

            healthBar.MaxHealth = int.MaxValue;
            movingSpeed = 9f;
            attackSpeed = 1f;
            attackPower = int.MaxValue;
            precision = float.MaxValue;

            Range = 100;

            attackSound = "enemylaser";
        }
    }
}
