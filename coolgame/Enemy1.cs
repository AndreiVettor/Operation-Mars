using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame
{
    class Enemy1 : Enemy
    {
        public Enemy1(ContentManager Content) :base(Content) {
            SetTexture(Content, "enemy2");
            Width = 58;
            Height = 80;
            EnableAnimation = true;
            EnableHealthBar = true;
        }

        
    }
}
