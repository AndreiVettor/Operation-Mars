using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame
{
    class UIElement
    {
        protected Vector2 position;
        public Vector2 Poisition
        {
            get { return position; }
            set {
                position = value;
                rectangle = new Rectangle((int)position.X, (int)position.Y, rectangle.Width, rectangle.Height);
            }
        }

        protected Vector2 textPosition;

        public int Width
        {
            get { return rectangle.Width; }
            set
            {
                rectangle = new Rectangle(rectangle.X, rectangle.Y, value, rectangle.Height);
            }
        }

        public int Height
        {
            get { return rectangle.Height; }
            set { rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height); }
        }

        protected Texture2D texture;
        protected Rectangle rectangle;

        protected Color backgroundColor;
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        protected Color foregroundColor;
        public Color ForegroundColor
        {
            get { return foregroundColor; }
            set { foregroundColor = value; }
        }

        protected string text;
        public string Text
        {
            get { return text; }
            set {
                text = value;
                textSize = font.MeasureString(text);
                textPosition = new Vector2(position.X + Width / 2 - textSize.X / 2, position.Y + Height / 2 - textSize.Y / 2);
            }
        }

        protected Vector2 textSize;

        protected SpriteFont font;

        public UIElement(Vector2 position, int width, int height)
        {
            this.position = position;
            Width = width;
            Height = height;
            this.text = "";
        }

        public UIElement(Vector2 position, int width, int height, string text)
        {
            this.position = position;
            Width = width;
            Height = height;
            this.text = text;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, backgroundColor);
            if(text != "")
            {
                spriteBatch.DrawString(font, text, position, foregroundColor);
            }
        }
    }
}
