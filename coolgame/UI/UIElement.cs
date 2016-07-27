using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame
{
    public class UIElement
    {
        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                rectangle.Location = new Point((int)value.X, (int)value.Y);
                Text = text;
            }
        }

        protected Vector2 textPosition;

        public int Width
        {
            get { return rectangle.Width; }
            set { rectangle.Width = value; }
        }
        public int Height
        {
            get { return rectangle.Height; }
            set { rectangle.Height = value; }
        }

        protected Texture2D texture;
        protected Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
        }

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
                textSize = font.MeasureString(value);
                textPosition = new Vector2(position.X + Width / 2 - textSize.X / 2, position.Y + Height / 2 - textSize.Y / 2);
            }
        }

        protected Vector2 textSize;

        protected SpriteFont font;

        public UIElement(ContentManager Content, string textureName,  Vector2 position)
        {
            texture = Content.Load<Texture2D>(textureName);
            Width = texture.Width;
            Height = texture.Height;
            font = Content.Load<SpriteFont>("UIFont");
            BackgroundColor = new Color(Color.DarkSlateBlue, 0.5f);
            ForegroundColor = Color.White;
            this.text = "";
            Position = position;
        }

        public UIElement(ContentManager Content, Vector2 position, string text)
        {
            texture = Content.Load<Texture2D>("tile");
            font = Content.Load<SpriteFont>("UIFont");
            BackgroundColor = new Color(Color.DarkSlateBlue, 0.5f);
            ForegroundColor = Color.White;
            this.text = text;
            Position = position;
        }

        public UIElement(ContentManager Content, Vector2 position, int width, int height)
        {
            texture = Content.Load<Texture2D>("tile");
            font = Content.Load<SpriteFont>("UIFont");
            BackgroundColor = new Color(Color.DarkSlateBlue, 0.5f);
            ForegroundColor = Color.White;
            this.text = "";
            Position = position;
            Width = width;
            Height = height;
        }

        public UIElement(ContentManager Content, Vector2 position, int width, int height, string text)
        {
            texture = Content.Load<Texture2D>("tile");
            font = Content.Load<SpriteFont>("UIFont");
            BackgroundColor = new Color(Color.DarkSlateBlue, 0.5f);
            ForegroundColor = Color.White;
            Text = text;
            Position = position;
            Width = width;
            Height = height;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, backgroundColor);
            if(text != "")
            {
                spriteBatch.DrawString(font, text, textPosition, foregroundColor);
            }
        }
    }
}
