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
        private float speed = .3f;

        public LaserProjectile(ContentManager content, double x, double y, float direction) : base(content)
        {
            SetTexture(content, "testlaser");
            Width = texture.Width;
            Height = texture.Height;
            X = x - Width / 2;
            Y = y - Height / 2;
            Rotation = direction;
        }

        public override void Update(GameTime gameTime, InputManager input, CollisionDetector collisionDetector)
        {
            X += (float)(Math.Cos(Rotation) * speed * gameTime.ElapsedGameTime.TotalMilliseconds);
            Y += (float)(Math.Sin(Rotation) * speed * gameTime.ElapsedGameTime.TotalMilliseconds);

            if (X + Width < 0 || Y + Height < 0 || X > Game.GAME_WIDTH || Y > Game.GAME_HEIGHT)
                Alive = false;

            base.Update(gameTime, input, collisionDetector);
        }

        public void Hit(Entity target)
        {
            target.Damage(20);
            Alive = false;
        }
    }
}
