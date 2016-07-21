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

        private float acceleration;
        private int defaultX;
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
                X -= recoilOffset;
                acceleration = recoilAcceleration;

                for (int i = 0; i < auxiliaryProjectiles; ++i)
                {
                    p = new PlayerProjectile(content, projectileX, projectileY, Rotation + ((float)random.NextDouble() - .5f) * maxSpread);
                }
            }

            X += acceleration;

            //If laser is close enough to default position lock into place
            if (Math.Abs(defaultX - X) < 2)
            {
                X = defaultX;
                acceleration = 0;
            }

            acceleration += recoilRecovery;

            //Clamp acceleration
            if (acceleration > -recoilAcceleration)
            {
                acceleration = -recoilAcceleration;
            }

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

            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
