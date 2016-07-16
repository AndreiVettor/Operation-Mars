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
        private double x;
        private double y;
        protected Texture2D texture;
        private Rectangle sourceRectangle;
        private Vector2 drawPosition;
        private Vector2 origin;
        private int totalFrames;
        private int currentFrame;
        private float frameUpdateTime;
        private float rotation;
        private bool enableAnimation;
        private float animationSpeed = .025f;
        protected HealthBar healthBar;
        private bool enableHealthBar;
        private bool autoHideHealthBar = true;

        public double X
        {
            get { return x; }
            set
            {
                x = value;
                drawPosition.X = (float)value + origin.X;
                healthBar.X = (int)value + Width / 2;
            }
        }

        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                drawPosition.Y = (float)value + origin.Y;
                healthBar.Y = (int)value - 20;
            }
        }

        public int Width
        {
            get { return sourceRectangle.Width; }
            set
            {
                if (value > 0)
                {
                    sourceRectangle.Width = value;
                    totalFrames = texture.Width / value;
                    healthBar.X = (int)X + value / 2;
                    origin.X = value / 2;
                }
            }
        }

        public int Height
        {
            get { return sourceRectangle.Height; }
            set
            {
                sourceRectangle.Height = value;
                origin.Y = value / 2;
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

        public bool EnableHealthBar
        {
            get { return enableHealthBar; }
            set { enableHealthBar = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Entity(ContentManager content)
        {
            sourceRectangle = new Rectangle();
            totalFrames = 0;
            healthBar = new HealthBar(content);
            X = Y = 0;
        }

        public virtual void Update(GameTime gameTime, InputManager input)
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
            spriteBatch.Draw(texture, drawPosition, null, sourceRectangle, origin, rotation, Vector2.One, Color.White, SpriteEffects.None, 0);

            if (enableHealthBar)
                healthBar.Draw(spriteBatch);
        }
    }
}
