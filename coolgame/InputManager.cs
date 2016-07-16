using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace coolgame
{
    public class InputManager
    {
        private KeyboardState keyState;
        private KeyboardState prevKeyState;
        private MouseState mouseState;
        private MouseState prevMouseState;

        public InputManager()
        {

        }

        public void Update()
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
        }

        public int MouseX { get { return mouseState.X; } }

        public int MouseY { get { return mouseState.Y; } }

        public int DeltaMouseX { get { return mouseState.X - prevMouseState.X; } }

        public int DeltaMouseY { get { return mouseState.Y - prevMouseState.Y; } }

        public ButtonState MouseLeft { get { return mouseState.LeftButton; } }

        public ButtonState MouseRight { get { return mouseState.RightButton; } }

        public ButtonState MouseMiddle { get { return mouseState.MiddleButton; } }

        public int MouseScroll { get { return mouseState.ScrollWheelValue - prevMouseState.ScrollWheelValue; } }

        public bool LeftClick
        {
            get
            {
                return mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released;
            }
        }

        public bool RightClick
        {
            get
            {
                return mouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton == ButtonState.Released;
            }
        }

        public bool MiddleClick
        {
            get
            {
                return mouseState.MiddleButton == ButtonState.Pressed && prevMouseState.MiddleButton == ButtonState.Released;
            }
        }

        public bool KeyDown(Keys key)
        {
            return keyState.IsKeyDown(key);
        }

        public bool KeyUp(Keys key)
        {
            return keyState.IsKeyUp(key);
        }

        public bool KeyPress(Keys key)
        {
            return keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key);
        }

        public bool KeyRelease(Keys key)
        {
            return keyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key);
        }
    }
}
