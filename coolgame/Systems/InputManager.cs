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
    public static class InputManager
    {
        private static KeyboardState keyState;
        private static KeyboardState prevKeyState;
        private static MouseState mouseState;
        private static MouseState prevMouseState;

        public static void Update()
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
        }

        public static int MouseX { get { return mouseState.X; } }

        public static int MouseY { get { return mouseState.Y; } }

        public static int DeltaMouseX { get { return mouseState.X - prevMouseState.X; } }

        public static int DeltaMouseY { get { return mouseState.Y - prevMouseState.Y; } }

        public static ButtonState MouseLeft { get { return mouseState.LeftButton; } }

        public static ButtonState MouseRight { get { return mouseState.RightButton; } }

        public static ButtonState MouseMiddle { get { return mouseState.MiddleButton; } }

        public static int MouseScroll { get { return mouseState.ScrollWheelValue - prevMouseState.ScrollWheelValue; } }

        public static bool LeftClick
        {
            get
            {
                return mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released;
            }
        }

        public static bool RightClick
        {
            get
            {
                return mouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton == ButtonState.Released;
            }
        }

        public static bool MiddleClick
        {
            get
            {
                return mouseState.MiddleButton == ButtonState.Pressed && prevMouseState.MiddleButton == ButtonState.Released;
            }
        }

        public static bool KeyDown(Keys key)
        {
            return keyState.IsKeyDown(key);
        }

        public static bool KeyUp(Keys key)
        {
            return keyState.IsKeyUp(key);
        }

        public static bool KeyPress(Keys key)
        {
            return keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key);
        }

        public static bool KeyRelease(Keys key)
        {
            return keyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key);
        }
    }
}
