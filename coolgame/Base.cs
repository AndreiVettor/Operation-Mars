using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class Base : Building
    {
        public Base(ContentManager content, int groundLevel) : base(content)
        {
            SetTexture(content, "base");
            Width = texture.Width;
            Height = texture.Height;
            Y = groundLevel - Height;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            health -= 
        }
    }
}
