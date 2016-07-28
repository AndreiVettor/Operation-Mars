using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame.UI
{
    public abstract class GUIElement
    {
        public enum BackType
        {
            Color,
            Texture
        }

        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
        }
        public Vector2 Position
        {
            get { return new Vector2(rectangle.X, rectangle.Y); }
            set
            {
                rectangle.Location = new Point((int)value.X, (int)value.Y);
                textPosition = value + new Vector2(textPadding, textPadding);
            }
        }
        public int X
        {
            get { return (int)Position.X; }
            set { Position = new Vector2(value, Position.Y); }

        }
        public int Y
        {
            get { return (int)Position.Y; }
            set { Position = new Vector2(Position.X, value); }

        }
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

        protected Color backgroundColor;
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                if(value.A != alpha)
                {
                    Debug.Log("Change GUI back color alpha using the Alpha variable " + backgroundColor.A);
                }
                backgroundColor = new Color(value, alpha);
            }
        }

        protected BackType backgroundType;
        public BackType BackgroundType
        {
            get { return backgroundType; }
            set { backgroundType = value; }
        }

        protected Color textColor;
        public Color TextColor
        {
            get { return textColor; }
            set
            {
                if (value.A != alpha)
                {
                    Debug.Log("Change GUI text color alpha using the Alpha variable " + textColor.A);
                }
                textColor = new Color(value, alpha);
            }
        }

        protected float alpha; //full opacity
        public float Alpha
        {
            get
            {
                if(TextAlpha == BackgroundAlpha)
                {
                    return alpha;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                alpha = value;
                TextAlpha = value;
                BackgroundAlpha = value;
            }
        }

        private float textAlpha;
        public float TextAlpha
        {
            get { return textAlpha; }
            set { textAlpha = value; }
        }

        private float backgroundAlpha;
        public float BackgroundAlpha
        {
            get { return backgroundAlpha; }
            set { backgroundAlpha = value; }
        }

        protected Texture2D backgroundTexture;
        public Texture2D BackgroundTexture
        {
            get { return backgroundTexture; }
            set
            {
                backgroundTexture = value;
            }
        }

        protected SpriteFont font;
        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        protected string text;
        public string Text
        {
            get { return text; }
        }

        protected Vector2 textPosition;
        public Vector2 TextPosition
        {
            get { return textPosition; }
            set { textPosition = value; }
        }

        protected int textPadding;
        public int TextPadding
        {
            get { return textPadding; }
            set { textPadding = value; }
        }

        public bool Disabled;

        public GUIElement()
        {
            Initialize();
        }

        public GUIElement (ContentManager Content)
        {
            Initialize(Content);
        }

        public GUIElement(ContentManager Content, string textureName)
        {
            BackgroundTexture = Content.Load<Texture2D>(textureName);
            BackgroundType = BackType.Texture;
            Width = backgroundTexture.Width;
            Height = backgroundTexture.Height;
            backgroundColor = Color.White;
            Initialize(Content);
        }

        public virtual void Initialize()
        {
            Alpha = 255; //Full opacity
            if (textColor == new Color())
            {
                TextColor = Color.White;
            }
            if (text == null)
            {
                text = "";
            }
        }

        public virtual void Initialize(ContentManager Content)
        {
            Disabled = false;
            Alpha = 255; //Full opacity
            if (backgroundColor == new Color() && backgroundTexture == null)
            {
                BackgroundColor = Color.Black;
            }
            if (textColor == new Color())
            {
                TextColor = Color.White;
            }
            if(backgroundTexture == null)
            {
                BackgroundTexture = Content.Load<Texture2D>("tile");
            }
            if(text == null)
            {
                text = "";
            }
        }

        public void SetText(string text)
        {
            this.text = text;
            if (Width == 0 && Height == 0)
            {
                Width = (int)font.MeasureString(text).X;
                Height = (int)font.MeasureString(text).Y;
            }
            Width += textPadding * 2;
            Height += textPadding * 2;

            textPosition = new Vector2(Position.X + textPadding, Position.Y + textPadding);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(BackgroundTexture != null)
            {
                spriteBatch.Draw(BackgroundTexture, rectangle, new Color(BackgroundColor, (int)BackgroundAlpha));
            }
            if(Text != "")
            {
                spriteBatch.DrawString(font, Text, textPosition, Color.FromNonPremultiplied(textColor.R, textColor.G, textColor.B, (int)TextAlpha));
            }
        }
    }
}
