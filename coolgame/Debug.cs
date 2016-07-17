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
        const string VERSION = "Alpha 0.1. Do not distribute.";

        private static float frameCount = 0;
        private static float timer = 0;
        private static float fps = 0;

        static SpriteFont font;
        private static List<string> messages = new List<string>();
        private static float messageLifespan = 1500;
        private static float messageTimer = 0;

        public static bool debugMessages = true;
        public static bool debugFPS = true;
        public static bool debugRectangles = false;

        private static Texture2D debugTexture;
        private static Rectangle debugRectangle;

        public static void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("SpriteFont");
            debugTexture = Content.Load<Texture2D>("tile");
        }

        public static void ToggleFPS()
        {
            debugFPS = !debugFPS;
        }

        public static void ToggleMessages()
        {
            debugMessages = !debugMessages;
        }

        public static void ToggleRectangles()
        {
            debugRectangles = !debugRectangles;
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
            if (debugFPS)
            {
                spriteBatch.DrawString(font, "FPS: " + fps.ToString(), new Vector2(10, 40), Color.Black);
                spriteBatch.DrawString(font, "FPS: " + fps.ToString(), new Vector2(9, 41), Color.White);
            }

            spriteBatch.DrawString(font, VERSION, new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(font, VERSION, new Vector2(9, 11), Color.White);

            if (debugMessages)
            {
                for (int i = 0; i < messages.Count; i++)
                {
                    spriteBatch.DrawString(font, messages[i], new Vector2(10, Game.GAME_HEIGHT - 30 - i * 30), Color.Black);
                    spriteBatch.DrawString(font, messages[i], new Vector2(9, Game.GAME_HEIGHT - 31 - i * 30), Color.White);
                }
            }

            if(debugRectangles)
            {
                List<Entity> entities = CollisionDetector.GetEntityList();
                foreach (Entity e in entities)
                {
                    debugRectangle = new Rectangle((int)e.X, (int)e.Y, e.Width, e.Height);
                    spriteBatch.Draw(debugTexture, debugRectangle, new Color(Color.Green, 0.5f));

                    debugRectangle = e.CollisionBox;
                    spriteBatch.Draw(debugTexture, debugRectangle, new Color(Color.Blue, 0.5f));
                }
            }
        }
    }
}
