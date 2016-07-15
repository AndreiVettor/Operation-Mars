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
        private Texture2D texture;
        private Rectangle rectangle;
        private Color color;
        private int maxHealth;
        private int health;
        private int maxWidth;

        public int Width
        {
            get { return maxWidth; }
            set
            {
                maxWidth = value;
                UpdateAppearance();
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
                UpdateAppearance();
            }
        }

        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                UpdateAppearance();
            }
        }

        public int X
        {
            get { return rectangle.X; }
            set { rectangle.X = value; }
        }

        public int Y
        {
            get { return rectangle.Y; }
            set { rectangle.Y = value; }
        }

        public HealthBar(ContentManager content)
        {
            texture = content.Load<Texture2D>("hpbar");
            rectangle = new Rectangle();
            Width = 50;
            Height = 5;
            MaxHealth = Health = 100;
        }

        private void UpdateAppearance()
        {
            float value = (float)health / maxHealth;
            rectangle.Width = (int)(maxWidth * value);
            color = new Color(Math.Min(255, (int)(500 * (1 - value))), Math.Min(255, (int)(500 * value)), 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }
    }
}
