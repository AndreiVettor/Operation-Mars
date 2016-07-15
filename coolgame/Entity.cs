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
    public class Entity
    {
        private Texture2D texture;
        private int width;
        private int height;
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
        }

        public int Height
        {
            get { return destinationRectangle.Height; }
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

        public Entity(Texture2D texture, int width, int height)
        {
            this.texture = texture;
            this.width = width;
            this.height = height;
            destinationRectangle = new Rectangle(0, 0, width, height);
            sourceRectangle = new Rectangle(0, 0, width, height);
            totalFrames = texture.Width / width;
        }

        public void Update(GameTime gameTime)
        {
            frameUpdateTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameUpdateTime >= 1 / animationSpeed)
            {
                frameUpdateTime = 0;

                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;

                sourceRectangle.X = currentFrame * width;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
