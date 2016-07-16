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
    public class LaserProjectile : Entity
    {
        private float speed = 50f;

        public LaserProjectile(ContentManager content, float x, float y, float direction) : base(content)
        {
            SetTexture(content, "redlaser");
            Width = texture.Width;
            Height = texture.Height;
            X = x - Width / 2;
            Y = y - Height / 2;
            Rotation = direction;
        }

        public override void Update(GameTime gameTime, InputManager input)
        {
            X += (float)(Math.Cos(Rotation) * speed);
            Y += (float)(Math.Sin(Rotation) * speed);

            base.Update(gameTime, input);
        }
    }
}
