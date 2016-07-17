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
        private float speed = .5f;

        public LaserProjectile(ContentManager content, double x, double y, float direction) : base(content)
        {
            SetTexture(content, "redlaser");
            Width = texture.Width;
            Height = texture.Height;
            X = x - Width / 2;
            Y = y - Height / 2;
            Rotation = direction;
        }

        public override void Update(float deltaTime, InputManager input)
        {
            X += (float)(Math.Cos(Rotation) * speed * deltaTime);
            Y += (float)(Math.Sin(Rotation) * speed * deltaTime);

            if (X + Width < 0 || Y + Height < 0 || X > Game.GAME_WIDTH || Y > Game.GAME_HEIGHT)
                Alive = false;

            base.Update(deltaTime, input);
        }

        public void Hit(Entity target)
        {
            target.Damage(20);
            Alive = false;
        }
    }
}
