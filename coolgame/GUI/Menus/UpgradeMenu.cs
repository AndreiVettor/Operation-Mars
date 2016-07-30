using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace coolgame.GUI.Menus
{
    class UpgradeMenu : GUIWindow
    {
        public UpgradeMenu(ContentManager Content, GUIManager guiManager) : base(Content)
        {
            Width = 500;
            Height = 400;
            textPadding = new Vector2(10, 10);
            borderPadding = new Vector2(30, 30);
            Center();

            AddButton(new GUIButton(Content, guiManager.TextFont, "Next Wave", new Vector2(145, 250),textPadding));

            //Lasers
            AddLabel(new GUILabel(guiManager.TextFont, "Upgrade Lasers", new Vector2(30, 0)));

            AddButton(new GUIButton(Content, "up_laserDamage", new Vector2(0,30)));
            AddLabel(new GUILabel(guiManager.TextFont, "100 SC", new Vector2(0, 50)));

            AddButton(new GUIButton(Content, "up_laserSpeed", new Vector2(65, 30)));
            AddButton(new GUIButton(Content, "up_laserSpread", new Vector2(130, 30)));

            //Upgrade Buildings
            AddButton(new GUIButton(Content, "up_turret", new Vector2(300, 0)));
            AddButton(new GUIButton(Content, "up_base", new Vector2(360, 0)));
            AddButton(new GUIButton(Content, "up_turret", new Vector2(300, 0)));

            AddButton(new GUIButton(Content, "up_forceField", new Vector2(360, 0)));
            AddButton(new GUIButton(Content, "up_forceFieldRegen", new Vector2(360, 0)));

            //Build
            AddButton(new GUIButton(Content, "build_turret", new Vector2(180, 0)));
            AddButton(new GUIButton(Content, "build_forceField", new Vector2(240, 0)));
            AddButton(new GUIButton(Content, "build_turret", new Vector2(180, 0)));

            //Repair
            AddButton(new GUIButton(Content, "repair_turret", new Vector2(480, 0)));
            AddButton(new GUIButton(Content, "repair_base", new Vector2(420, 0)));
            AddButton(new GUIButton(Content, "repair_turret", new Vector2(480, 0)));

            Alpha = 220;
            BackgroundColor = CustomColor.DarkBlue;
            SecondaryColor = CustomColor.LightBlue;
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
                GameManager.UpgradeTurretPower();
            }
            if (ButtonPressed(2))
            {
                GameManager.UpgradeLaserSpeed();
                GameManager.UpgradeTurretSpeed();
            }
            if (ButtonPressed(3))
            {
                GameManager.UpgradeLaserSpread();
                GameManager.UpgradeTurretSpread();
            }
            if (ButtonPressed(4))
            {
                GameManager.ActivateTurret();
            }
            if (ButtonPressed(5))
            {
                GameManager.ActivateForcefield();
            }
            if (ButtonPressed(6))
            {
                GameManager.UpgradeTurretHealth();
            }
            if (ButtonPressed(8))
            {
                GameManager.RepairBuilding("base");
            }
            if (ButtonPressed(9))
            {
                GameManager.RepairBuilding("leftturret");
                GameManager.RepairBuilding("rightturret");
            }
        }
    }
}
