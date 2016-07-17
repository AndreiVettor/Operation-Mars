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
    static class Debug
    {
        private static float frameCount = 0;
        private static float timer = 0;
        private static float fps = 0;

        static SpriteFont font;

        public static void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("debugFont");
        }

        public static void Update(float deltaTime)
        {
            timer += deltaTime;

            if (timer > 1000)
            {
                timer -= 1000;
                fps = frameCount;
                frameCount = 0;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            frameCount++;
            spriteBatch.DrawString(font, "FPS: " + fps.ToString(), new Vector2(10, 10), Color.White);
        }
    }
}
