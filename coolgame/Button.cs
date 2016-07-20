using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame
{
    class Button : UIElement
    {
        private Color pressedColor;
        public Color PressedColor
        {
            get { return pressedColor; }
            set { pressedColor = value; }
        }

        private new Color backgroundColor;
        public new Color BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
                pressedColor = new Color(backgroundColor.R - 20, backgroundColor.G - 20, backgroundColor.B - 20);
            }
        }


        private bool pressed;
        public bool Pressed
        {
            get { return pressed; }
            set { pressed = value; }
        }

        private bool hovered;
        public bool Hovered
        {
            get { return hovered; }
            set { hovered = value; }
        }

        private bool held;
        public bool Held
        {
            get { return held; }
            set { held = value; }
        }

        public Button (ContentManager Content, Vector2 position, int width, int height) : base (Content, position,  width,  height) {
            this.text = "BUTTON";
            textSize = font.MeasureString(text);
            textPosition = new Vector2(position.X + Width / 2 - textSize.X / 2, position.Y + Height / 2 - textSize.Y / 2);
        }

        public Button(ContentManager Content, Vector2 position, int width, int height, string text) : base(Content, position, width, height, text)
        {

        }

        public void Update()
        {
            pressed = false;
            if (InputManager.HoversUIElement(this) && InputManager.MouseLeft == ButtonState.Pressed)
            {
                if(!held)
                {
                    pressed = true;
                }
                held = true;
            }
            if (held && InputManager.MouseLeft == ButtonState.Released) 
            {
                pressed = false;
                held = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (pressed)
            {
                spriteBatch.Draw(texture, rectangle, PressedColor);
            }
            else
            {
                spriteBatch.Draw(texture, rectangle, backgroundColor);
            }

            if (text != "")
            {
                spriteBatch.DrawString(font, text, textPosition, foregroundColor);
            }
        }
    }
}
