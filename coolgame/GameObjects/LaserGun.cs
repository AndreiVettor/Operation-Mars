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
        private ContentManager content;
        private float cooldownTime;
        private Random random;

        public LaserGun(ContentManager content, int x, int y) : base(content)
        {
            SetTexture("laser");
            Width = texture.Width;
            Height = texture.Height;
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
                LaserProjectile p = new LaserProjectile(content, projectileX, projectileY, Rotation);

                /*for (int i = 0; i < 1000; ++i)
                {
                    p = new LaserProjectile(content, projectileX, projectileY, Rotation + ((float)random.NextDouble() - .5f) * (float)Math.PI * 2);
                }*/
            }

            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
