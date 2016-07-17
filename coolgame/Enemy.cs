using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame
{
    public abstract class Enemy : Entity
    {
        public Enemy (ContentManager Content) : base(Content)
        {
            
        }
    }
}
