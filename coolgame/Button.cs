using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        public Button (ContentManager Content, Vector2 position, int width, int height) : base (Content, position,  width,  height) {
            this.text = "BUTTON";
            textSize = font.MeasureString(text);
            textPosition = new Vector2(position.X + Width / 2 - textSize.X / 2, position.Y + Height / 2 - textSize.Y / 2);
        }

        public void Update()
        {
            if (InputManager.HoversUIElement(this) && InputManager.MouseLeft == ButtonState.Pressed)
            {
                pressed = true;
                BackgroundColor = new Color(backgroundColor, 0.1f);
                Debug.Log("Pressed");
            }
            if (pressed && InputManager.MouseLeft == ButtonState.Released) 
            {
                pressed = false;
                BackgroundColor = new Color(backgroundColor, 0.3f);
                Debug.Log("Released");
            }
        }
    }
}
