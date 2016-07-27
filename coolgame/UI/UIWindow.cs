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
    public class UIWindow : UIElement
    {
        protected List<Button> menuButtons;
        protected List<UIElement> menuItems;
        protected UIElement background;
        protected int spacing = 10;

        private bool hasBackground;
        public bool HasBackground
        {
            get { return hasBackground; }
            set { hasBackground = value; }
        }

        private bool buttonHeld;
        public bool ButtonHeld
        {
            get { return buttonHeld; }
        }

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
            ArrangeMenu();
        }

        public virtual void ArrangeMenu()
        {
            // calculate the space occupied by the buttons
            int totalHeight = spacing;
            int maxWidth = 0;
            foreach (Button b in menuButtons)
            {
                totalHeight += b.Height + spacing;
                if (b.Width > maxWidth)
                    maxWidth = b.Width;
            }

            foreach (Button b in menuButtons)
            {
                b.Width = maxWidth;
            }

            maxWidth += spacing * 2;

            // reposition the window in the center of the screen and resize it to fit all the elements
            position.X = Game.GAME_WIDTH / 2 - maxWidth / 2;
            position.Y = Game.GAME_HEIGHT / 2 - totalHeight / 2;
            Width = maxWidth;
            Height = totalHeight;
            background.Position = position;
            background.Width = Width;
            background.Height = Height;

            // place all the buttons in their own spot
            menuButtons[0].Position = new Vector2(position.X + spacing, position.Y + spacing);
            for (int i = 1; i < menuButtons.Count; ++i)
            {
                menuButtons[i].Position = new Vector2(position.X + spacing, menuButtons[i - 1].Position.Y + menuButtons[i - 1].Height + spacing);
            }
        }

        public UIWindow (ContentManager Content, Vector2 position, int width, int height) : base(Content, position, width, height)
        {
            menuButtons = new List<Button>();
            menuItems = new List<UIElement>();
            background = new UIElement(Content, position, width, height);
            //background.BackgroundColor = new Color(Color.SlateGray, 0.4f);
            buttonHeld = false;
            text = "";
            hasBackground = true;
        }

        public void Update()
        {
            buttonHeld = false;
            foreach (Button b in menuButtons)
            {
                b.Update();
                if(b.Held)
                {
                    buttonHeld = true;
                }
            }
        }

        new public void Draw(SpriteBatch spriteBatch)
        {
            if(hasBackground)
            {
                background.Draw(spriteBatch);
            }
            foreach(Button b in menuButtons)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
