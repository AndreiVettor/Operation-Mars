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
    public class LaserGun : Entity
    {
        private float cooldownTime;
        private Random random;

        private Vector2 velocity;
        //private float acceleration;
        private int defaultX, defaultY;
        private int recoilOffset;
        private float recoilAcceleration;
        private float recoilRecovery;

        private int auxiliaryProjectiles;
        private float maxSpread;
        private float cooldown;

        public LaserGun(ContentManager content, int x, int y) : base(content)
        {
            SetTexture("laser");
            Width = texture.Width;
            Height = texture.Height;

            defaultX = x;
            defaultY = y;
            recoilOffset = 6;
            recoilAcceleration = -1;
            recoilRecovery = 0.3f;

            X = x;
            Y = y;
            this.content = content;
            layerDepth = LayerManager.GetLayerDepth(Layer.Buildings);
            random = new Random();
            cooldown = 200f;
        }

        public override void Update(float deltaTime)
        {
            if (InputManager.KeyPress(Keys.D1))
            {
                auxiliaryProjectiles++;
                maxSpread = (float)Math.PI / 40 * auxiliaryProjectiles;
            }
            if (InputManager.KeyDown(Keys.D2))
            {
                cooldown /= 1.05f;

                recoilRecovery += 0.3f;
                //recoilOffset -= 1;
                if (recoilOffset < 0) recoilOffset = 0;
            }

            //if (acceleration == 0)
                Rotation = (float)Math.Atan2(InputManager.MouseY - Y - Height / 2, InputManager.MouseX - X - Width / 2);

            cooldownTime += deltaTime;
            if (InputManager.MouseLeft == ButtonState.Pressed && cooldownTime > cooldown)
            {
                SoundManager.PlayClip("laser");
                cooldownTime = 0;
                double projectileX = X + Width / 2 + Math.Cos(Rotation) * (Width / 4);
                double projectileY = Y + Height / 2 + Math.Sin(Rotation) * (Width / 4);
                LaserProjectile p = new PlayerProjectile(content, projectileX, projectileY, Rotation);

                //Recoil
                X -= recoilOffset * Math.Cos(Rotation);
                Y -= recoilOffset * Math.Sin(Rotation);
                //acceleration = recoilAcceleration;
                velocity = new Vector2(recoilAcceleration * (float)Math.Cos(Rotation), recoilAcceleration * (float)Math.Sin(Rotation));

                for (int i = 0; i < auxiliaryProjectiles; ++i)
                {
                    p = new PlayerProjectile(content, projectileX, projectileY, Rotation + ((float)random.NextDouble() - .5f) * maxSpread);
                }
            }

            //X += acceleration * Math.Cos(Rotation);
            //Y += acceleration * Math.Sin(Rotation);

            //acceleration += recoilRecovery;

            X += velocity.X * deltaTime * 6 / 100;
            Y += velocity.Y * deltaTime * 6 / 100;

            float recoveryAngle = (float)Math.Atan2(Y - defaultY, X - defaultX);
            velocity -= new Vector2(recoilRecovery * (float)Math.Cos(recoveryAngle), recoilRecovery * (float)Math.Sin(recoveryAngle)) *
                deltaTime * 6 / 100;

            //If laser is close enough to default position lock into place
            if (Math.Abs(defaultX - X) < 3)
            {
                X = defaultX;
            }
            if (Math.Abs(defaultY - Y) < 3)
            {
                Y = defaultY;
            }
            if (Math.Abs(defaultX - X) < 3 && Math.Abs(defaultY - Y) < 3)
            {
                velocity = Vector2.Zero;
                //acceleration = 0;
            }

            //Clamp acceleration
            /*if (acceleration > -recoilAcceleration)
            {
                acceleration = -recoilAcceleration;
            }*/

            if (Math.Abs(defaultX - X) > 10)
            {
                if (defaultX - X > 0)
                {
                    X = defaultX - 10;
                }
                else {
                    X = defaultX + 10;
                }
            }
            if (Math.Abs(defaultY - Y) > 10)
            {
                if (defaultY - Y > 0)
                {
                    Y = defaultY - 10;
                }
                else
                {
                    Y = defaultY + 10;
                }
            }

            //origin = new Vector2((float)X - defaultX + Width / 2, (float)Y - defaultY + Height / 2);

            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
