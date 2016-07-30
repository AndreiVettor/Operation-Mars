using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class Tower : Entity
    {
        private LaserGun laserGun;

        public LaserGun Gun
        {
            get { return laserGun; }
        }

        public Tower(ContentManager content, int groundLevel, int basePosition) : base(content)
        {
            SetTexture("tower1");
            Width = texture.Width;
            Height = texture.Height;
            X = basePosition + 200;
            Y = groundLevel - Height;
            laserGun = new LaserGun(content, (int)X + 15, (int)Y + 15, 3);
            layerDepth = LayerManager.GetLayerDepth(Layer.Buildings);

            layerDepth += .01f;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            laserGun.PointAt(InputManager.MouseX, InputManager.MouseY);

            if (InputManager.MouseLeft == ButtonState.Pressed)
            {
                laserGun.Shoot();
            }

            laserGun.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            laserGun.Draw(spriteBatch);
        }
    }
}
