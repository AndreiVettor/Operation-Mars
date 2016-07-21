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

            healthBar.MaxHealth = 100;
            movingSpeed = 9f;
            attackSpeed = 50f;
            attackPower = 1;
        }
    }
}
