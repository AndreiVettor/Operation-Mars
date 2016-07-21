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

        public LaserGun(ContentManager content, int x, int y) : base(content)
        {
            SetTexture("laser");
            Width = texture.Width;
            Height = texture.Height;
            defaultX = x;
            X = x;
            Y = y;
            this.content = content;
            layerDepth = LayerManager.GetLayerDepth(Layer.Buildings);
            random = new Random();
        }

        public override void Update(float deltaTime)
        {
            Rotation = (float)Math.Atan2(InputManager.MouseY - Y - Height / 2, InputManager.MouseX - X - Width / 2);

            cooldownTime += deltaTime;
            if (InputManager.MouseLeft == ButtonState.Pressed && cooldownTime > 200.0f)
            {
                SoundManager.PlayClip("laser");
                cooldownTime = 0;
                double projectileX = X + Width / 2 +  Math.Cos(Rotation) * (Width / 4);
                double projectileY = Y + Height / 2 + Math.Sin(Rotation) * (Width / 4);
                LaserProjectile p = new PlayerProjectile(content, projectileX, projectileY, Rotation);

                //Recoil
                X -= 5;
                acceleration = -1;


                /*for (int i = 0; i < 3; ++i)
                {
                    p = new PlayerProjectile(content, projectileX, projectileY, Rotation + ((float)random.NextDouble() - .5f) * (float)Math.PI / 20);
                }*/
            }
            X += acceleration;
            if (Math.Abs(defaultX - X) < 5)
            {
                X = defaultX;
                acceleration = 0;
            }

            acceleration += 0.2f;
            if(acceleration > 1)
            {
                acceleration = 1;
            }


            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
