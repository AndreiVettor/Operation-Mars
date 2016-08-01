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

            healthBar.MaxHealth = 35;
            movingSpeed = 15f;
            attackSpeed = 1f;
            attackPower = 30;
            AnimationSpeed *= (15f / 9);
            spaceCash = 2;

            attackSound = "steelroachattack";
            hitSound = "steelroachattack";
        }
    }
}
