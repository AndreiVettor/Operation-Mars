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
        const string VERSION = "Operation Mars Alpha 1.1. Do not distribute.";

        private static float frameCount = 0;
        private static float timer = 0;
        private static float fps = 0;

        static SpriteFont font;
        private static List<string> messages = new List<string>();
        private static float messageLifespan = 2500;
        private static float messageTimer = 0;
        private static Vector2 messageBoxPos = new Vector2(15, Game.GAME_HEIGHT - 15);
        private static int messagePadding = 10;
        private static int messageMaxLength = 0;
        private static int textShadowSize = 1;
        private static int lineHeight = 20;

        private static float layerDepth = LayerManager.GetLayerDepth(Layer.Debugging);

        public static bool debugMessages = true;
        public static bool debugFPS = true;
        public static bool debugRectangles = false;

        private static Texture2D debugTexture;
        private static Rectangle debugRectangle;
        private static float debugOpacity = 0.2f;

        public static void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("SpriteFont");
            debugTexture = Content.Load<Texture2D>("tile");
        }

        public static void ToggleFPS()
        {
            debugFPS = !debugFPS;
        }

        public static void ToggleDebugLog()
        {
            debugMessages = !debugMessages;
        }

        public static void ToggleRectangles()
        {
            debugRectangles = !debugRectangles;
        }

        public static void Log(string message)
        {
            if (debugMessages)
            {
                messages.Insert(0, message);
                if (messages.Count > 15)
                {
                    messages.RemoveRange(14, messages.Count - 15);
                }
                messageMaxLength = Math.Max(messageMaxLength, (int)font.MeasureString(message).X + 10 + messagePadding);
            }
        }

        public static void Log(string message, bool variable)
        {
            if (debugMessages)
            {
                message = message + " " + (variable ? "Enabled" : "Disabled");

                messages.Insert(0, message);
                if (messages.Count > 15)
                {
                    messages.RemoveRange(14, messages.Count - 15);
                }
                messageMaxLength = Math.Max(messageMaxLength, (int)font.MeasureString(message).X + 10 + messagePadding);
            }
        }

        public static void Log(int message)
        {
            Log(message.ToString());
        }

        public static void Log(float message)
        {
            Log(message.ToString());
        }

        public static void Log(bool message)
        {
            Log(message.ToString());
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
                    if (messages.Count == 0)
                    {
                        messageTimer = 0;
                        messageMaxLength = 0;
                    }
                }
            }
        }

        private static void DrawText(SpriteBatch spriteBatch, string text, Vector2 position)
        {
            spriteBatch.DrawString(font, text, position, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, layerDepth + 0.01f);
            spriteBatch.DrawString(font, text, new Vector2(position.X - textShadowSize, position.Y + textShadowSize), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            frameCount++;
            if (debugFPS)
            {
                string fpsText;
                if(GameManager.FrameLimiting == true)
                {
                    fpsText = "FPS: " + fps.ToString() + " (capped)";
                }
                else
                {
                    fpsText = "FPS: " + fps.ToString();
                }
                DrawText(spriteBatch, fpsText, new Vector2(10, 30));
            }

            DrawText(spriteBatch, VERSION, new Vector2(10, 10));

            if (debugMessages)
            {
                debugRectangle = new Rectangle((int)messageBoxPos.X, (int)messageBoxPos.Y - lineHeight * messages.Count, messageMaxLength, messages.Count * lineHeight);
                spriteBatch.Draw(debugTexture, debugRectangle, null, new Color(Color.Black, 0.05f), 0, Vector2.Zero, SpriteEffects.None, layerDepth + 0.02f);

                for (int i = 0; i < messages.Count; i++)
                {
                    DrawText(spriteBatch, messages[i], new Vector2(messageBoxPos.X + messagePadding, Game.GAME_HEIGHT - messagePadding - (i + 1) * lineHeight));
                }
            }

            if(debugRectangles)
            {
                foreach (Entity e in GameManager.GetEntityList())
                {
                    debugRectangle = e.CollisionBox;
                    spriteBatch.Draw(debugTexture, debugRectangle, null, new Color(Color.Blue, debugOpacity), 0, Vector2.Zero, SpriteEffects.None, layerDepth + 0.03f);
                }
            }
        }
    }
}
