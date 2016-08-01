using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class Reptilian : MeleeEnemy
    {
        public Reptilian(ContentManager Content) : base(Content)
        {
            SetTexture("reptilian");
            Width = 58;
            Height = 80;
            EnableAnimation = true;

            AnimationSpeed *= (15f / 9);
            attackSound = "steelroachattack";
            hitSound = "steelroachattack";

            healthBar.MaxHealth = 35;
            movingSpeed = 15f;
            attackSpeed = 2f;
            attackPower = 10;

            spaceCash = 2;
        }
    }
}
