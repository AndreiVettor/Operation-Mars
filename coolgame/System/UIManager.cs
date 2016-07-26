using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame.Systems
{
    public static class UIManager
    {
        private static List<Button> buttons = new List<Button>();
        private static List<UIWindow> windows = new List<UIWindow>();
        private static List<UIElement> elements = new List<UIElement>();

        private static Texture2D spaceCashTexture;
        private static Texture2D crosshair;
        private static bool displayCrosshair;

        private static int windowNumber = 0;
        private static bool pauseMenuOpen = false;
        private static bool upgradeMenuOpen = false;

        //message variables
        private static SpriteFont messageFont;
        private static bool showMessage = false;
        private static string messageText;
        private static float messageDuration;
        private static float messageTimer;
        private static float fadeDuration = 500;
        private static Color messageColor = Color.White;

        private static SpriteFont gameFont;


        public static void LoadContent(ContentManager Content)
        {
            crosshair = Content.Load<Texture2D>("crosshair");
            spaceCashTexture = Content.Load<Texture2D>("spaceCash");
            messageFont = Content.Load<SpriteFont>("messageFont");
            gameFont = Content.Load<SpriteFont>("gameFont");
        }

        public static void DisplayMessage(string text)
        {
            showMessage = true;
            messageText = text;
            messageTimer = 0;
            messageColor = new Color(messageColor, 0);

            messageDuration = 3000;
        }

        public static void DisplayMessage(string text, float duration)
        {
            showMessage = true;
            messageText = text;
            messageTimer = 0;
            messageColor = new Color(messageColor, 0);

            messageDuration = duration;
        }

        public static void TogglePauseMenu()
        {
            if(pauseMenuOpen)
            {
                pauseMenuOpen = false;
                GameManager.GamePaused = false;
            }
            else
            {
                pauseMenuOpen = true;
                GameManager.GamePaused = true;
            }
        }

        public static void ToggleUpgradeMenu()
        {
            if (upgradeMenuOpen)
            {
                upgradeMenuOpen = false;
                GameManager.GamePaused = false;
            }
            else
            {
                upgradeMenuOpen = true;
                GameManager.GamePaused = true;
            }
        }

        public static void AddElement(UIElement element)
        {
            elements.Add(element);
        }
        public static void AddElement(Button button)
        {
            buttons.Add(button);
        }
        public static void AddElement(UIWindow window)
        {
            windows.Add(window);
            windowNumber++;
        }

        public static void SetCrosshairDisplay(Game game, bool value)
        {
            displayCrosshair = value;
            game.IsMouseVisible = !value;
        }

        public static void Update(Game game, float deltaTime)
        {
            //Message display
            if (showMessage)
            {
                messageTimer += deltaTime;

                //Disable message
                if (messageTimer >= messageDuration)
                {
                    messageTimer = 0;
                    showMessage = false;
                }

                //Fade In
                if (messageTimer <= fadeDuration)
                {
                    messageColor = new Color(messageColor, messageColor.A + (int)(deltaTime / (fadeDuration / 255)));
                }

                //Fade Out
                if (messageTimer >= messageDuration - fadeDuration)
                {
                    messageColor = new Color(messageColor, messageColor.A - (int)(deltaTime / (fadeDuration / 255)));
                }
            }

            //Update menus
            for (int i = 0; i < windowNumber; i++)
            {
                windows[i].Update();
            }

            //Button events
            for (int i = 0; i < windowNumber; i++)
            {
                switch (i)
                {
                    //Pause Menu
                    case 0:
                        {
                            for (int j = 0; j < windows[i].GetButtons().Count; j++)
                            {
                                if (windows[i].GetButtons()[j].Pressed && pauseMenuOpen)
                                    switch (j)
                                    {
                                        //Resume button
                                        case 0:
                                            {
                                                TogglePauseMenu();
                                                break;
                                            }
                                        //Restart Button
                                        case 1:
                                            {
                                                GameManager.Restart();
                                                TogglePauseMenu();
                                                break;
                                            }
                                        //Exit Button
                                        case 2:
                                            {
                                                game.Exit();
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                            }
                            break;
                        }
                    //Upgrade Menu
                    case 1:
                        {
                            for (int j = 0; j < windows[i].GetButtons().Count; j++)
                            {
                                if (windows[i].GetButtons()[j].Pressed && upgradeMenuOpen)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            {
                                                GameManager.laser_damage++;
                                                break;
                                            }
                                        case 1:
                                            {
                                                GameManager.laser_speed++;
                                                break;
                                            }
                                        case 2:
                                            {
                                                GameManager.laser_spread++;
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spaceCashTexture, new Vector2(Game.GAME_WIDTH - 150, 33), Color.White);
            spriteBatch.DrawString(gameFont, "" + GameManager.SpaceCash, new Vector2(Game.GAME_WIDTH - 100, 40), Color.White);

            //Message Drawing
            if (showMessage)
            {
                spriteBatch.DrawString(
                    messageFont,
                    messageText, 
                    new Vector2(
                        Game.GAME_WIDTH/2 - messageFont.MeasureString(messageText).X/2, 
                        Game.GAME_HEIGHT/2 - messageFont.MeasureString(messageText).Y/2 - 150), 
                    messageColor);
            }


            //Menu Drawing
            for (int i = 0; i < windowNumber; i++)
            {
                switch (i)
                {
                    //Pause Menu
                    case 0:
                        {
                            if (pauseMenuOpen)
                            {
                                windows[i].Draw(spriteBatch);
                            }
                            break;
                        }
                    case 1:
                        {
                            if(upgradeMenuOpen)
                            {
                                windows[i].Draw(spriteBatch);
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

            }

            for (int i = 1; i < elements.Count(); i++)
            {
                    elements[i].Draw(spriteBatch);
            }

            if(displayCrosshair)
            {
                spriteBatch.Draw(
                    crosshair,
                    new Vector2(
                        InputManager.MouseX - crosshair.Width / 2,
                        InputManager.MouseY - crosshair.Height / 2),
                    null,
                    null,
                    null,
                    0f,
                    new Vector2(1, 1),
                    Color.White,
                    SpriteEffects.None,
                    0);
            }
        }
    }
}
