using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace coolgame.GUI.Menus
{
    class MainMenu : GUIWindow
    {
        public MainMenu(ContentManager Content, GUIManager guiManager) : base(Content)
        {
            Width = 250;
            Height = 200;
            textPadding = new Vector2(30, 7);
            borderPadding = new Vector2(20, 20);
            X = 700;
            Y = 350;

            AddButton(new GUIButton(Content, guiManager.TextFont, "START GAME", new Vector2(0, 0), textPadding));
            AddButton(new GUIButton(Content, guiManager.TextFont, "ABOUT THE GAME", new Vector2(0, 60), textPadding));
            AddButton(new GUIButton(Content, guiManager.TextFont, "EXIT TO DESKTOP", new Vector2(0, 240), textPadding));

            TweakButtons(true, true, false, 20);

            Alpha = 0;
            BackgroundColor = CustomColor.DarkBlue;
            SecondaryColor = CustomColor.DarkBlue;
        }

        public override void Update(Game game, ContentManager Content, GUIManager guiManager, EnemySpawner spawner)
        {
            if(!guiManager.WindowOpen(new InformationWindow(Content, "about", guiManager)))
            {
                base.Update(game, Content, guiManager, spawner);
                if (ButtonPressed(0))
                {
                    Disabled = true;
                    GameManager.State = GameState.Game;
                }
                else if (ButtonPressed(1))
                {
                    guiManager.AddWindow(new InformationWindow(Content, "about", guiManager));
                }
                else if (ButtonPressed(2))
                {
                    game.Exit();
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
             base.Draw(spriteBatch);
        }
    }
}
