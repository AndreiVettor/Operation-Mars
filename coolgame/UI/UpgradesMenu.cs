using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    class UpgradesMenu : UIWindow
    {
        public UpgradesMenu(ContentManager Content, int newWidth, int newHeight) : base(Content, Vector2.Zero, newWidth, newHeight)
        {
            spacing = 15;

            BackgroundColor = Color.YellowGreen;
            position.X = Game.GAME_WIDTH / 2 - newWidth / 2;
            position.Y = Game.GAME_HEIGHT / 2 - newHeight / 2;
            background.Position = position;
            background.Width = newWidth;
            background.Height = newHeight;

            AddItem(new Button(Content, "up_laserDamage", new Vector2(0,0)));
            AddItem(new Button(Content, "up_laserSpeed", new Vector2(50, 0)));
            AddItem(new Button(Content, "up_laserSpread", new Vector2(100, 0)));
        }

        public new void ArrangeMenu()
        {
            for (int i = 1; i < menuButtons.Count; ++i)
            {
                menuButtons[i].Position = new Vector2(position.X + menuButtons[i].Position.X + spacing, position.Y + menuButtons[i].Position.Y + spacing);

            }
        }
    }
}
