using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class Steelroach : MeleeEnemy
    {
        public Steelroach(ContentManager Content) : base(Content)
        {
            SetTexture("steelroach2");
            Width = 98;
            Height = 53;
            EnableAnimation = true;

            healthBar.MaxHealth = 40;
            movingSpeed = 6f;
            attackSpeed = 1f;
            attackPower = 15;

            hitSound = "steelroachhit";
            attackSound = "steelroachattack";
        }
    }
}
