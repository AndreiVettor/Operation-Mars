using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class LaserBeam : Entity
    {
        public LaserBeam(ContentManager content) : base(content)
        {
            SetTexture("electrobeam");
        }
    }
}
