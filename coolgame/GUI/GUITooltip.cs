using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame.GUI
{
    public class GUITooltip : GUIElement
    {
        private int borderSize;
        public int BorderSize
        {
            get { return borderSize; }
            set { borderSize = value; }
        }

        private bool visible;
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        private Color borderColor;
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        private List<string> textLines;

        private int lineHeight;

        private int lineSpacing;
        public int LineSpacing
        {
            get { return lineSpacing; }
            set { lineSpacing = value; }
        }

        private int currentIndex;
        private int lastIndex;
        private string nextString;

        public GUITooltip (ContentManager Content, SpriteFont textFont) : base(Content)
        {
            currentIndex = 0;
            nextString = "";
            textLines = new List<string>();
            lineSpacing = 5;

            font = textFont;
            TextCentered = true;
            borderSize = 2;
            textPadding = new Vector2(8 + borderSize, 3 + borderSize);
            SetText("You forgot to add text");
            BorderColor = CustomColor.LightBlue;
            BackgroundColor = new Color(
                BackgroundColor.R - 40,
                BackgroundColor.G - 40,
                BackgroundColor.B - 40);

            BackgroundAlpha = 200;
        }

        public void SetText(string text, int lineNumber)
        {
            textLines.Clear();
            currentIndex = 0;
            nextString = text.Substring(currentIndex, text.Length);
            for ( int i = 0; i < lineNumber; i++)
            {
                lastIndex = currentIndex;
                currentIndex = nextString.IndexOf("*");
                if(currentIndex != text.Length - 1)
                {
                    nextString = text.Substring(currentIndex, text.Length - 1 - currentIndex);
                }
                textLines.Add(text.Substring(lastIndex, currentIndex));
            }
            this.text = text;
                
            Width = (int)font.MeasureString(textLines[0]).X + (int)textPadding.X * 2 + borderSize * 2;
            Height = (int)font.MeasureString(text).Y * lineNumber + (int)textPadding.Y * 2 + borderSize * 2;
            lineHeight = (int)font.MeasureString(text).Y;

            if (textCentered)
            {
                textPosition = new Vector2(Position.X + Width / 2 - font.MeasureString(textLines[0]).X / 2, Position.Y + textPadding.Y + borderSize);
            }
            else
            {
                textPosition = new Vector2(Position.X + textPadding.X + borderSize, Position.Y + textPadding.Y + borderSize);
            }
        }

        public void Update()
        {
            X = InputManager.MouseX;
            Y = InputManager.MouseY;
            visible = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(visible && text != "")
            {
                if (BackgroundTexture != null)
                {
                    spriteBatch.Draw(
                        BackgroundTexture, 
                        rectangle,
                        Color.FromNonPremultiplied(borderColor.R, borderColor.G, borderColor.B, (int)BackgroundAlpha));
                    spriteBatch.Draw(
                        BackgroundTexture, 
                        new Rectangle(
                            rectangle.X + borderSize, 
                            rectangle.Y + BorderSize,
                            rectangle.Width - borderSize*2, 
                            rectangle.Height - borderSize*2),
                        Color.FromNonPremultiplied(backgroundColor.R, backgroundColor.G, backgroundColor.B, (int)BackgroundAlpha));
                }
                for(int i = 0; i < textLines.Count; i++) 
                {
                    spriteBatch.DrawString(font, textLines[i], textPosition + new Vector2(0, i*(lineHeight + lineSpacing)), Color.FromNonPremultiplied(textColor.R, textColor.G, textColor.B, (int)TextAlpha));
                }
            }
        }
    }
}
