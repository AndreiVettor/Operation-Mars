using coolgame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame.GUI.Menus
{
    class GameMenu : GUIWindow
    {
        public GameMenu (ContentManager Content, SpriteFont textFont) : base(Content)
        {
            Width = 250;
            Height = 350;
            borderPadding = 30;
            Center();
            AddButton(new GUIButton(Content, textFont, "RESUME",        new Vector2(0, 0), Color.DarkSlateBlue));
            AddButton(new GUIButton(Content, textFont, "MUTE SOUND",    new Vector2(0, 60), Color.DarkSlateBlue));
            AddButton(new GUIButton(Content, textFont, "RESTART GAME",  new Vector2(0, 120), Color.DarkSlateBlue));
            AddButton(new GUIButton(Content, textFont, "BACK TO START", new Vector2(0, 180), Color.DarkSlateBlue));
            AddButton(new GUIButton(Content, textFont, "EXIT GAME",     new Vector2(0, 240), Color.DarkSlateBlue));
            NormalizeButtonLength(true, true, 10);
            AddLabel(new GUILabel(Content, textFont, "I'm a Label",     new Vector2(150, 0)));
            BackgroundColor = Color.CornflowerBlue;
        }

        private void NormalizeButtonLength(bool centerButtons, bool resizeMenu, int spacing)
        {
            int maxWidth = 0;

            //Find Max Width
            foreach(GUIButton button in buttons)
            {
                maxWidth = Math.Max(maxWidth, button.Width);
            }

            //Resize Menu
            if(resizeMenu)
            {
                Width = maxWidth + borderPadding * 2;
                Height = (buttons[0].Height + spacing) * buttons.Count + borderPadding * 2;
            }

            //Apply Max Width to All Buttons
            foreach (GUIButton button in buttons)
            {
                button.Width = maxWidth;
            }

            if(centerButtons)
            {
                for(int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].Y = Y + borderPadding + i * (spacing + buttons[i].Height);
                    buttons[i].X = X + Width / 2 - buttons[i].Width / 2;
                }
            }
        }

        public override void Update(Game game, ContentManager Content, GUIManager guiManager)
        {
            base.Update(game, Content, guiManager);

            if (ButtonPressed(0))
            {
                Closing = true;
                GameManager.State = GameState.Game;
            }
            else if (ButtonPressed(1))
            {
                SoundManager.ToggleMute();
            }
            else if (ButtonPressed(2))
            {
                GameManager.Restart(Content);
                GameManager.State = GameState.Game;
                Closing = true;
            }
            else if (ButtonPressed(3))
            {
                Closing = true;
                GameManager.Restart(Content);
                GameManager.State = GameState.StartMenu;
            }
            else if (ButtonPressed(4))
            {
                game.Exit();
            }
        }
    }
}
