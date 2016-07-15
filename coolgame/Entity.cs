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
    public abstract class Entity
    {
        protected Texture2D texture;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private int totalFrames;
        private int currentFrame;
        private float frameUpdateTime;
        private bool enableAnimation;
        private float animationSpeed = .025f;

        public int X
        {
            get { return destinationRectangle.X; }
            set { destinationRectangle.X = value; }
        }

        public int Y
        {
            get { return destinationRectangle.Y; }
            set { destinationRectangle.Y = value; }
        }

        public int Width
        {
            get { return destinationRectangle.Width; }
            protected set
            {
                if (value > 0)
                {
                    destinationRectangle.Width = value;
                    sourceRectangle.Width = value;
                    totalFrames = texture.Width / value;
                }
            }
        }

        public int Height
        {
            get { return destinationRectangle.Height; }
            protected set
            {
                destinationRectangle.Height = value;
                sourceRectangle.Height = value;
            }
        }

        public bool EnableAnimation
        {
            get { return enableAnimation; }
            set { enableAnimation = value; }
        }

        public float AnimationSpeed
        {
            get { return animationSpeed; }
            set { animationSpeed = value; }
        }

        public Entity()
        {
            destinationRectangle = new Rectangle();
            sourceRectangle = new Rectangle();
            totalFrames = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (EnableAnimation && animationSpeed > 0)
            {
                frameUpdateTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (frameUpdateTime >= 1 / animationSpeed)
                {
                    frameUpdateTime = 0;

                    currentFrame++;
                    if (currentFrame == totalFrames)
                        currentFrame = 0;

                    sourceRectangle.X = currentFrame * Width;
                }
            }
        }

        protected void SetTexture(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
            if (Width > 0)
                totalFrames = texture.Width / Width;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
