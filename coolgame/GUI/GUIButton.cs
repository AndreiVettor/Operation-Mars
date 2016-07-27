using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace coolgame.UI
{
    class GUIButton :GUIElement
    {
        private Color pressedColor;
        public Color PressedColor
        {
            get { return pressedColor; }
            set { pressedColor = value; }
        }

        private Color releasedColor;
        public Color ReleasedColor
        {
            get { return releasedColor; }
            set { releasedColor = value; }
        }

        private bool pressed;
        public bool Pressed
        {
            get { return pressed; }
            set
            {
                pressed = value;
                if(value)
                {
                    held = true;
                }
            }
        }

        private bool held;
        public bool Held
        {
            get { return held; }
            set { held = value; }
        }

        private int colorChange = 30;

        public GUIButton(ContentManager Content, SpriteFont textFont, string text) : base(Content)
        {
            Initialize(textFont, text);
        }

        public GUIButton(ContentManager Content, SpriteFont textFont, string text, Vector2 position, Color backgroundColor) : base(Content)
        {
            BackgroundColor = backgroundColor;
            Position = position;
            Initialize(textFont, text);
        }

        public void Initialize(SpriteFont textFont, string text)
        {
            PressedColor = new Color(
                BackgroundColor.R + colorChange,
                BackgroundColor.G + colorChange,
                BackgroundColor.B + colorChange);
            ReleasedColor = BackgroundColor;
            TextPadding = 15;
            SetText(textFont, text);
        }

        public void Update()
        {
            //Make Pressed a trigger - Only stays true for an Update cycle
            Pressed = false;

            if (InputManager.MouseIntersects(Rectangle) && InputManager.LeftClick) {
                Pressed = true;
            }

            //Determine if held
            if(!Pressed && InputManager.MouseLeft == ButtonState.Released)
            {
                Held = false;
            }

            //Change color on press
            if(held)
            {
                BackgroundColor = PressedColor;
            }
            else
            {
                BackgroundColor = ReleasedColor;
            }
        }
    }
}
