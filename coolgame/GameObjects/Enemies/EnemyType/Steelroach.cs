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
            hitSound = "steelroachhit";
            attackSound = "steelroachattack";


            healthBar.MaxHealth = 500;
            movingSpeed = 5f;
            attackSpeed = 1f;
            attackPower = 150;

            spaceCash = 3;
        }
    }
}
