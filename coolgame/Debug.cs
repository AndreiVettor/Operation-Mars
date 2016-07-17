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
        private static List<string> messages = new List<string>();
        private static float messageLifespan = 1500;
        private static float messageTimer = 0;

        public static void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("SpriteFont");
        }

        public static void Log(string message)
        {
            messages.Insert(0, message);
            if(messages.Count > 15)
            {
                messages.RemoveRange(14, messages.Count - 15);
            }
        }

        public static void Update(float deltaTime)
        {
            timer += deltaTime;


            if (timer >= 1000)
            {
                timer -= 1000;
                fps = frameCount;
                frameCount = 0;
            }

            if (messages.Count != 0)
            {
                messageTimer += deltaTime;
                if (messageTimer >= messageLifespan)
                {
                    messageTimer -= messageLifespan;
                    messages.RemoveAt(messages.Count - 1);
                    if (messages.Count == 0) messageTimer = 0;
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            frameCount++;
            spriteBatch.DrawString(font, "FPS: " + fps.ToString(), new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(font, "FPS: " + fps.ToString(), new Vector2(9, 11), Color.White);

            for (int i = 0; i < messages.Count; i++) 
            {
                spriteBatch.DrawString(font, messages[i], new Vector2(10, Game.GAME_HEIGHT - 30 - i * 30), Color.Black);
                spriteBatch.DrawString(font, messages[i], new Vector2(9, Game.GAME_HEIGHT - 31 - i * 30), Color.White);
            }
        }
    }
}
