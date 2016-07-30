using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace coolgame.GUI
{
    public abstract class GUIElement
    {
        public enum BackType
        {
            Color,
            Texture
        }

        protected Rectangle rectangle;
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
                if (textCentered)
                {
                    textPosition = new Vector2(Position.X + Width / 2 - font.MeasureString(text).X / 2, Position.Y + textPadding.Y);
                }
                else
                {
                    textPosition = value + new Vector2(textPadding.X, textPadding.Y);
                }
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
                return alpha;
            }
            set
            {
                alpha = value;
                textAlpha = value;
                backgroundAlpha = value;
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

        protected Vector2 textPadding;
        public Vector2 TextPadding
        {
            get { return textPadding; }
            set { textPadding = value; }
        }

        protected bool textCentered;
        public bool TextCentered
        {
            get { return textCentered; }
            set { textCentered = value; }
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

        public virtual void SetText(string text)
        {
            this.text = text;
            if (Width == 0 && Height == 0)
            {
                Width = (int)font.MeasureString(text).X;
                Height = (int)font.MeasureString(text).Y;
            }
            Width += (int)textPadding.X * 2;
            Height += (int)textPadding.Y * 2;

            if(textCentered)
            {
                textPosition = new Vector2(Position.X + Width/2 - font.MeasureString(text).X/2, Position.Y + textPadding.Y);
            }
            else
            {
                textPosition = new Vector2(Position.X + textPadding.X, Position.Y + textPadding.Y);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(BackgroundTexture != null)
            {
                spriteBatch.Draw(BackgroundTexture, rectangle, Color.FromNonPremultiplied(backgroundColor.R, backgroundColor.G, backgroundColor.B, (int)backgroundAlpha));
            }
            if(Text != "")
            {
                spriteBatch.DrawString(font, Text, textPosition, Color.FromNonPremultiplied(textColor.R, textColor.G, textColor.B, (int)TextAlpha));
            }
        }
    }
}
