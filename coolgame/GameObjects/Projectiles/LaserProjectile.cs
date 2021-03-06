﻿using System;
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
        protected int attackPower;

        public LaserProjectile(ContentManager content, double x, double y, float direction, int attackPower) : base(content)
        {
            //X = x;
            //Y = y;
            Rotation = direction;
            layerDepth = LayerManager.GetLayerDepth(Layer.Projectiles);
            GameManager.AddEntity(this);
            this.attackPower = attackPower;
        }

        public override int Width
        {
            get
            {
                return base.Width;
            }

            set
            {
                base.Width = value;
                //X -= value / 2;
            }
        }

        public override int Height
        {
            get
            {
                return base.Height;
            }

            set
            {
                base.Height = value;
                //Y -= value / 2;
            }
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
