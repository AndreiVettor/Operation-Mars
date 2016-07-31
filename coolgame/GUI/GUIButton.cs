using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace coolgame.GUI
{
    public class GUIButton : GUIElement
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

        public new Color BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                if(BackgroundType == BackType.Color)
                {
                    if (value.A != alpha)
                    {
                        Debug.Log("Change GUI back color alpha using the Alpha variable " + backgroundColor.A);
                    }
                    backgroundColor = new Color(value, alpha);
                    pressedColor = new Color(
                        backgroundColor.R + colorChange,
                        backgroundColor.G + colorChange,
                        backgroundColor.B + colorChange);
                    releasedColor = backgroundColor;
                }
            }
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
        
        protected bool hovered;
        public bool Hovered
        {
            get { return hovered; }
            set { hovered = value; }
        }

        protected string hoverInformation;
        public string HoverInformation
        {
            get { return hoverInformation; }
            set { hoverInformation = value; }
        }

        protected int hoverLines;
        public int HoverLines
        {
            get { return hoverLines; }
            set { hoverLines = value; }
        }


        private int colorChange = 30;

        public GUIButton(ContentManager Content, SpriteFont textFont, string text) : base(Content)
        {
            Initialize(textFont, text);
        }

        public GUIButton(ContentManager Content, SpriteFont textFont, string text, Vector2 position) : base(Content)
        {
            Position = position;
            Initialize(textFont, text);
        }

        public GUIButton(ContentManager Content, SpriteFont textFont, string text, Vector2 position, Color backgroundColor) : base(Content)
        {
            BackgroundColor = backgroundColor;
            Position = position;
            Initialize(textFont, text);
        }

        public GUIButton(ContentManager Content, SpriteFont textFont, string text, Vector2 position, Vector2 textPadding) : base(Content)
        {
            Position = position;
            this.textPadding = textPadding;
            Initialize(textFont, text);
        }

        public GUIButton(ContentManager Content, string textureName, Vector2 position) : base(Content, textureName)
        {
            Position = position;
            Initialize(null, "");
        }

        public GUIButton(ContentManager Content, string textureName, Vector2 position, string hoverInformation, int hoverLines) : base(Content, textureName)
        {
            Position = position;
            Initialize(null, "");
            this.hoverInformation = hoverInformation;
            this.hoverLines = hoverLines;
        }

        public void Initialize(SpriteFont textFont, string text)
        {
            hoverInformation = "";
            if(textFont != null)
            {
                font = textFont;
                SetText(text);
            }
            PressedColor = new Color(
                BackgroundColor.R - colorChange,
                BackgroundColor.G - colorChange,
                BackgroundColor.B - colorChange);
            ReleasedColor = BackgroundColor;
        }

        public void Update()
        {
            if (InputManager.MouseIntersects(rectangle))
            {
                hovered = true;
            }
            else
            {
                hovered = false;
            }

            //Make Pressed a trigger - Only stays true for an Update cycle
            Pressed = false;

            if (InputManager.MouseIntersects(Rectangle) && InputManager.LeftClick) {
                Pressed = true;
                SoundManager.PlayClip("button1");
            }

            //Determine if held
            if(!Pressed && InputManager.MouseLeft == ButtonState.Released)
            {
                Held = false;
            }

            //Change color on press
            if(held)
            {
                backgroundColor = PressedColor;
            }
            else
            {
                backgroundColor = ReleasedColor;
            }
        }
    }
}
