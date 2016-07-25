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

        private static Texture2D crosshair;

        private static int windowNumber = 0;
        private static bool pauseMenuOpen = false;
        private static bool upgradeMenuOpen = false;

        private static bool clickedUI = false;
        public static bool ClickedUI
        {
            get { return clickedUI; }
        }

        //message variables
        private static SpriteFont messageFont;
        private static bool showMessage = false;
        private static string messageText;
        private static float messageDuration;
        private static float messageTimer;
        private static float fadeDuration = 500;
        private static Color messageColor = Color.White;

        public static void LoadContent(ContentManager Content)
        {
            crosshair = Content.Load<Texture2D>("crosshair");
            messageFont = Content.Load<SpriteFont>("messageFont");
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
            }
            else
            {
                upgradeMenuOpen = true;
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

        public static void Update(Game game, float deltaTime)
        {
            //Message display
            if(showMessage)
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
                    messageColor = new Color(messageColor, messageColor.A + deltaTime / (fadeDuration / 255));
                }
   
                //Fade Out
                if (messageTimer > fadeDuration && messageTimer <= messageDuration - fadeDuration)
                {
                    messageColor = new Color(messageColor, messageColor.A - deltaTime / (fadeDuration / 255));
                }

            }

            //Update menus
            clickedUI = false;
            for (int i = 0; i < windowNumber; i++)
            {
                windows[i].Update();
                if(windows[i].ButtonHeld)
                {
                    clickedUI = true;
                }
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
                                switch(j)
                                {
                                    //Resume button
                                    case 0:
                                        {
                                            if(windows[i].GetButtons()[j].Pressed && pauseMenuOpen)
                                            {
                                                TogglePauseMenu();
                                            }
                                            break;
                                        }
                                    //Exit button
                                    case 1:
                                        {
                                            if (windows[i].GetButtons()[j].Pressed && pauseMenuOpen)
                                            {
                                                game.Exit();
                                            }
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
                                                //ToggleUpgradeMenu();
                                                break;
                                            }
                                        case 1:
                                            {
                                                
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

            if(crosshair != null)
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
