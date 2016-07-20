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
    public abstract class LaserProjectile : Entity
    {
        protected float speed = 3;

        public LaserProjectile(ContentManager content, double x, double y, float direction) : base(content)
        {
            X = x - Width / 2;
            Y = y - Height / 2;
            Rotation = direction;
            layerDepth = LayerManager.GetLayerDepth(Layer.Projectiles);
            GameManager.AddEntity(this);
        }

        public override void Update(float deltaTime)
        {
            X += (float)(Math.Cos(Rotation) * speed * deltaTime);
            Y += (float)(Math.Sin(Rotation) * speed * deltaTime);

            if (X + Width < 0 || Y + Height < 0 || X > Game.GAME_WIDTH || Y > Game.GAME_HEIGHT)
                Alive = false;

            if (CollisionManager.CollidesWithGround(this))
                Alive = false;

            base.Update(deltaTime);
        }
    }
}
