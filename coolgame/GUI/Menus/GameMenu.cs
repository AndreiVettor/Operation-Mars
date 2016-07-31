using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace coolgame.GUI.Menus
{
    class GameMenu : GUIWindow
    {
        public GameMenu (ContentManager Content, GUIManager guiManager) : base(Content)
        {
            Width = 250;
            Height = 350;
            borderPadding = new Vector2(20, 20);
            textPadding = new Vector2(30, 7);
            Center();

            AddButton(new GUIButton(Content, guiManager.MediumFont, "RESUME",        new Vector2(0,   0), textPadding));
            AddButton(new GUIButton(Content, guiManager.MediumFont, "MUTE SOUND",    new Vector2(0,  60), textPadding));
            AddButton(new GUIButton(Content, guiManager.MediumFont, "MUTE MUSIC",    new Vector2(0, 120), textPadding));
            AddButton(new GUIButton(Content, guiManager.MediumFont, "RESTART GAME",  new Vector2(0, 180), textPadding));
            AddButton(new GUIButton(Content, guiManager.MediumFont, "BACK TO START", new Vector2(0, 240), textPadding));
            AddButton(new GUIButton(Content, guiManager.MediumFont, "EXIT GAME",     new Vector2(0, 300), textPadding));

            TweakButtons(true, true, true, true, 20);

            Alpha = 180;
            BackgroundColor = CustomColor.DarkBlue;
            SecondaryColor = CustomColor.LightBlue;
        }

        public override void Update(Game game, ContentManager Content, GUIManager guiManager, EnemySpawner spawner)
        {
            base.Update(game, Content, guiManager, spawner);

            if (ButtonPressed(0))
            {
                if (!guiManager.WindowOpen(typeof(UpgradeMenu)))
                {
                    GameManager.State = GameState.Game;
                }
                Disabled = true;
            }
            else if (ButtonPressed(1))
            {
                SoundManager.MuteSound();
            }
            else if (ButtonPressed(2))
            {
                SoundManager.MuteMusic();
            }
            else if (ButtonPressed(3))
            {
                GameManager.Restart(Content, guiManager, spawner);
                GameManager.State = GameState.Game;
                Disabled = true;
            }
            else if (ButtonPressed(4))
            {
                GameManager.Restart(Content, guiManager, spawner);
                GameManager.State = GameState.StartMenu;
                guiManager.AddWindow(new MainMenu(Content, guiManager));
                Disabled = true;
            }
            else if (ButtonPressed(5))
            {
                game.Exit();
            }
        }
    }
}
