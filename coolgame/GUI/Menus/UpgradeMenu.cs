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
    class UpgradeMenu : GUIWindow
    {
        public UpgradeMenu(ContentManager Content, SpriteFont textFont) : base(Content)
        {
            Width = 250;
            Height = 350;
            borderPadding = 30;
            Center();

            AddButton(new GUIButton(Content, textFont, "Next Wave", new Vector2(100, 300)));
            AddButton(new GUIButton(Content, "up_laserDamage", new Vector2(0,0)));
            AddButton(new GUIButton(Content, "up_laserSpeed", new Vector2(50, 0)));
            AddButton(new GUIButton(Content, "up_laserSpread", new Vector2(100, 0)));
            AddButton(new GUIButton(Content, "up_buyturret", new Vector2(150, 0)));
            AddButton(new GUIButton(Content, "up_buyshield", new Vector2(200, 0)));

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
            if (ButtonPressed(1))
            {
                GameManager.UpgradeLaserPower();
            }
            if (ButtonPressed(2))
            {
                GameManager.UpgradeLaserSpeed();
            }
            if (ButtonPressed(3))
            {
                GameManager.UpgradeLaserSpread();
            }
            if (ButtonPressed(4))
            {
                GameManager.ActivateTurret();
            }
            if (ButtonPressed(5))
            {
                GameManager.ActivateForcefield();
            }
        }
    }
}
