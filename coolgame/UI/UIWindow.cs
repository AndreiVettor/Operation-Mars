using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame.UI
{
    public class UIWindow : UIElement
    {
        private List<Button> menuButtons;
        private List<UIElement> menuItems;
        private UIElement background;

        public bool ButtonPressed;

        public List<Button> GetButtons()
        {
            return menuButtons;
        }

        public void AddItem(UIElement item)
        {
            menuItems.Add(item);
        }

        public void AddItem(Button button)
        {
            menuButtons.Add(button);
        }

        public UIWindow (ContentManager Content, Vector2 position, int width, int height) : base(Content, position, width, height)
        {
            menuButtons = new List<Button>();
            menuItems = new List<UIElement>();
            background = new UIElement(Content, position, width, height);
            background.BackgroundColor = new Color(Color.SlateGray, 0.2f);
            ButtonPressed = false;
        }

        public void Update()
        {
            foreach(Button b in menuItems)
            {
                ButtonPressed = false;
                b.Update();
                if(b.Pressed)
                {
                    ButtonPressed = true;
                }
            }
        }

        new public void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            foreach(Button b  in menuButtons)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
