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
            borderPadding = 20;
            Center();
            AddButton(new GUIButton(Content, textFont, "RESUME",        new Vector2(0, 0)));
            AddButton(new GUIButton(Content, textFont, "MUTE SOUND",    new Vector2(0, 60)));
            AddButton(new GUIButton(Content, textFont, "RESTART GAME",  new Vector2(0, 120)));
            AddButton(new GUIButton(Content, textFont, "BACK TO START", new Vector2(0, 180)));
            AddButton(new GUIButton(Content, textFont, "EXIT GAME",     new Vector2(0, 240)));
            NormalizeButtonLength(true, true, 10);
            BackgroundColor = Color.CornflowerBlue;
            SecondaryColor = Color.DarkSlateBlue;
        }

        public override void Update(Game game, ContentManager Content, GUIManager guiManager, EnemySpawner spawner)
        {
            base.Update(game, Content, guiManager, spawner);

            if (ButtonPressed(0))
            {
                GameManager.State = GameState.Game;
                Disabled = true;
            }
            else if (ButtonPressed(1))
            {
                SoundManager.ToggleMute();
            }
            else if (ButtonPressed(2))
            {
                GameManager.Restart(Content, guiManager, spawner);
                GameManager.State = GameState.Game;
                Disabled = true;
            }
            else if (ButtonPressed(3))
            {
                GameManager.Restart(Content, guiManager, spawner);
                GameManager.State = GameState.StartMenu;
                guiManager.AddWindow(new MainMenu(Content, guiManager.TextFont));
                Disabled = true;
            }
            else if (ButtonPressed(4))
            {
                game.Exit();
            }
        }
    }
}
