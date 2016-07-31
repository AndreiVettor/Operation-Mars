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
    class GameOverWindow : GUIWindow
    {
        public GameOverWindow(ContentManager Content, GUIManager guiManager, EnemySpawner enemySpawner) : base(Content)
        {
            Width = Game.GAME_WIDTH;
            Height = Game.GAME_HEIGHT;
            int spacing = 20;
            int offset = 100;
            //borderPadding = new Vector2(20, 20);
            textPadding = new Vector2(30, 7);
            Center();

            AddButton(new GUIButton(Content, guiManager.MediumFont, "TRY AGAIN", new Vector2(0, 0), textPadding));
            AddButton(new GUIButton(Content, guiManager.MediumFont, "BACK TO MENU", new Vector2(0, 60), textPadding));
            AddButton(new GUIButton(Content, guiManager.MediumFont, "EXIT TO DESKTOP", new Vector2(0, 240), textPadding));
            AddLabel(new GUILabel(
                guiManager.HugeFont,
                "GAME OVER",
                new Vector2(
                    Game.GAME_WIDTH / 2 - guiManager.HugeFont.MeasureString("GAME OVER").X/2,
                    Game.GAME_HEIGHT/2 - 100)));
            TweakButtons(true, true, false,true, spacing);

            AddLabel(new GUILabel(
                guiManager.MediumFont,
                "Days Survived: " + (enemySpawner.Wave - 1),
                new Vector2(
                    Game.GAME_WIDTH / 2 - guiManager.MediumFont.MeasureString("Days Survived: " + enemySpawner.Wave).X / 2,
                    Game.GAME_HEIGHT / 2 - 40)));
            TweakButtons(true, true, false, true, spacing);

            int totalHeight = 0;
            foreach(GUIButton button in buttons)
            {
                totalHeight += button.Height;
                totalHeight += spacing;
            }
            for(int i = 0; i< buttons.Count; i++)
            {
                buttons[i].Y = Game.GAME_HEIGHT / 2 - totalHeight / 2 + i * (buttons[0].Height + spacing ) + offset;
            }

            Alpha = 180;
            BackgroundColor = Color.Black;
            SecondaryColor = CustomColor.LightBlue;
        }

        public override void Update(Game game, ContentManager Content, GUIManager guiManager, EnemySpawner spawner)
        {
            base.Update(game, Content, guiManager, spawner);

            if (ButtonPressed(0))
            {
                GameManager.GameOver = false;
                GameManager.Restart(Content, guiManager, spawner);
                GameManager.State = GameState.Game;
                Disabled = true;
            }
            else if (ButtonPressed(1))
            {
                GameManager.GameOver = false;
                GameManager.State = GameState.StartMenu;
                guiManager.AddWindow(new MainMenu(Content, guiManager));
                Disabled = true;
            }
            else if (ButtonPressed(2))
            {
                game.Exit();
            }
        }
    }
}
