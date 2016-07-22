using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class DemolitionRoverUnit : MeleeEnemy
    {
        public DemolitionRoverUnit(ContentManager Content) : base(Content)
        {
            SetTexture("tank");
            Width = 132;
            Height = 57;
            EnableAnimation = true;
            AnimationSpeed /= 3;
            healthBar.MaxHealth = 50;
            movingSpeed = 9f;
            attackSpeed = 50f;
            attackPower = 1;
        }
    }
}
