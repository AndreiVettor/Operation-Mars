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
    class MainMenu : GUIWindow
    {
        public MainMenu(ContentManager Content, SpriteFont textFont) : base(Content)
        {
            Width = 250;
            Height = 350;
            borderPadding = 30;
            Center();
            AddButton(new GUIButton(Content, textFont, "START GAME", new Vector2(0, 0)));
            AddButton(new GUIButton(Content, textFont, "ABOUT THE GAME", new Vector2(0, 60)));
            AddButton(new GUIButton(Content, textFont, "EXIT TO DESKTOP", new Vector2(0, 240)));
            NormalizeButtonLength(false, false, 0);
            BackgroundColor = Color.CornflowerBlue;
            SecondaryColor = Color.DarkSlateBlue;
        }

        public override void Update(Game game, ContentManager Content, GUIManager guiManager, EnemySpawner spawner)
        {
            base.Update(game, Content, guiManager, spawner);

            if (ButtonPressed(0))
            {
                Disabled = true;
                GameManager.State = GameState.Game;
            }
            else if (ButtonPressed(1))
            {
                guiManager.AddWindow(new InformationWindow(Content, "about", guiManager.TextFont));
            }
            else if (ButtonPressed(2))
            {
                game.Exit();
            }
        }
    }
}
