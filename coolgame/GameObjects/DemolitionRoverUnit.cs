﻿using System;
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
            Width = 133;
            Height = 57;
            EnableAnimation = true;
            AnimationSpeed /= 1;
            healthBar.MaxHealth = 550;
            movingSpeed = 9f;
            attackSpeed = 50f;
            attackPower = 1;
        }
    }
}
