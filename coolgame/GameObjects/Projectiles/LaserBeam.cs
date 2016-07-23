using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace coolgame
{
    public class LaserBeam : Entity
    {
        public LaserBeam(ContentManager content) : base(content)
        {
            SetTexture("electrobeam");
            color = Color.Blue;
        }
    }
}
