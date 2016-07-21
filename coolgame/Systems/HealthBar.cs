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
    public class HealthBar
    {
        public enum HealthBarColoring { Normal, Forcefield };

        private Texture2D texture;
        private Rectangle rectangle;
        private Color color;
        private int maxHealth;
        private int health;
        private int maxWidth;
        private int centerX;
        private bool autoHide = true;
        private float autoHideTime;
        private bool visible;
        private float layerDepth = LayerManager.GetLayerDepth(Layer.Healthbar);
        private HealthBarColoring colorScheme;

        public int Width
        {
            get { return maxWidth; }
            set
            {
                maxWidth = value;
                UpdateAppearance();
                UpdatePosition();
            }
        }

        public int Height
        {
            get { return rectangle.Height; }
            set { rectangle.Height = value; }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
            set
            {
                maxHealth = value;
                health = value;
                UpdateAppearance();
                UpdatePosition();
            }
        }

        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                UpdateAppearance();
                UpdatePosition();
                visible = true;
                autoHideTime = 0;
            }
        }

        public int X
        {
            get { return centerX; }
            set
            {
                centerX = value;
                UpdatePosition();
            }
        }

        public int Y
        {
            get { return rectangle.Y; }
            set { rectangle.Y = value; }
        }

        public bool AutoHide
        {
            get { return autoHide; }
            set { autoHide = value; }
        }

        public HealthBarColoring ColorScheme
        {
            get { return colorScheme; }
            set
            {
                colorScheme = value;
                UpdateAppearance();
            }
        }

        public HealthBar(ContentManager content)
        {
            texture = content.Load<Texture2D>("hpbar");
            rectangle = new Rectangle();
            Width = 50;
            Height = 5;
            MaxHealth = 100;
            visible = false;
            colorScheme = HealthBarColoring.Normal;
        }

        public void Update(float deltaTime)
        {
            if (autoHide && visible)
            {
                autoHideTime += deltaTime;
                if (autoHideTime >= 1000f)
                {
                    autoHideTime = 0;
                    visible = false;
                }
            }
        }

        private void UpdateAppearance()
        {
            float value = (float)health / maxHealth;
            rectangle.Width = (int)(maxWidth * value);

            if (colorScheme == HealthBarColoring.Normal)
                color = new Color(Math.Min(255, (int)(500 * (1 - value))), Math.Min(255, (int)(500 * value)), 0);
            else if (colorScheme == HealthBarColoring.Forcefield)
                color = new Color((int)(255 * (1 - value)), Math.Max(135, (int)(255 * (1 - value))), 255);
        }

        private void UpdatePosition()
        {
            rectangle.X = centerX - rectangle.Width / 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!autoHide || (autoHide && visible))
                spriteBatch.Draw(texture, rectangle, null, color, 0, Vector2.Zero, SpriteEffects.None, layerDepth);
        }
    }
}
