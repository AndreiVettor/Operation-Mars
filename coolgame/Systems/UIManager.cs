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
        private static List<UIElement> uiElements = new List<UIElement>();

        private static bool drawMenu = false;

        public static void ToggleMenu()
        {
            if(drawMenu)
            {
                drawMenu = false;
                GameManager.GamePaused = false;
            }
            else
            {
                drawMenu = true;
                GameManager.GamePaused = true;
            }
        }

        public static void AddElement(UIElement element)
        {
            uiElements.Add(element);
        }

        public static void Update(Game game)
        {
            foreach (Button b in uiElements)
            {
                b.Update();
                if (drawMenu && b.Pressed)
                {
                    switch (uiElements.IndexOf(b))
                    {
                        case 0:
                            {
                                ToggleMenu();
                                break;
                            }
                        case 1:
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
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < 2; i++)
            {
                if(drawMenu)
                {
                    uiElements[i].Draw(spriteBatch);
                }
            }
            for (int i = 2; i < uiElements.Count(); i++)
            {
                    uiElements[i].Draw(spriteBatch);
            }
        }
    }
}
