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
            Height = 380;
            textPadding = new Vector2(15, 15);
            borderPadding = new Vector2(30, 30);
            Center();

            AddButton(new GUIButton(Content, guiManager.TextFont, "  Next Wave  ", new Vector2(280, 255),textPadding));

            //Lasers
            AddLabel(new GUILabel(guiManager.TextFont, "Upgrade Lasers", new Vector2(25, 0)));

            AddButton(new GUIButton(Content, "up_laserDamage", new Vector2(0, 30)));
            AddButton(new GUIButton(Content, "up_laserSpeed", new Vector2(65, 30)));
            AddButton(new GUIButton(Content, "up_laserSpread", new Vector2(130, 30)));

            AddLabel(new GUILabel(guiManager.UpgradeFont, "100 SC", new Vector2(5, 85)));
            AddLabel(new GUILabel(guiManager.UpgradeFont, "100 SC", new Vector2(70, 85)));
            AddLabel(new GUILabel(guiManager.UpgradeFont, "100 SC", new Vector2(135, 85)));

            //Upgrade Buildings
            AddLabel(new GUILabel(guiManager.TextFont, "Upgrade Buildings", new Vector2(15, 120)));

            AddButton(new GUIButton(Content, "up_turret", new Vector2(0, 150)));
            AddButton(new GUIButton(Content, "up_base", new Vector2(65, 150)));
            AddButton(new GUIButton(Content, "up_turret", new Vector2(130, 150)));
            AddButton(new GUIButton(Content, "up_forceField", new Vector2(40, 225)));
            AddButton(new GUIButton(Content, "up_forceFieldRegen", new Vector2(105, 225)));

            AddLabel(new GUILabel(guiManager.UpgradeFont, "100 SC", new Vector2(5, 205)));
            AddLabel(new GUILabel(guiManager.UpgradeFont, "100 SC", new Vector2(70, 205)));
            AddLabel(new GUILabel(guiManager.UpgradeFont, "100 SC", new Vector2(135, 205)));
            AddLabel(new GUILabel(guiManager.UpgradeFont, "100 SC", new Vector2(45, 280)));
            AddLabel(new GUILabel(guiManager.UpgradeFont, "100 SC", new Vector2(110, 280)));

            //Build
            AddLabel(new GUILabel(guiManager.TextFont, "Build", new Vector2(330, 0)));

            AddButton(new GUIButton(Content, "build_turret", new Vector2(260, 30)));
            AddButton(new GUIButton(Content, "build_forceField", new Vector2(325, 30)));
            AddButton(new GUIButton(Content, "build_turret", new Vector2(390, 30)));

            AddLabel(new GUILabel(guiManager.UpgradeFont, "100 SC", new Vector2(265, 85)));
            AddLabel(new GUILabel(guiManager.UpgradeFont, "100 SC", new Vector2(330, 85)));
            AddLabel(new GUILabel(guiManager.UpgradeFont, "100 SC", new Vector2(395, 85)));

            //Repair
            AddLabel(new GUILabel(guiManager.TextFont, "Repair", new Vector2(320, 120)));

            AddButton(new GUIButton(Content, "repair_turret", new Vector2(255, 150)));
            AddButton(new GUIButton(Content, "repair_base", new Vector2(320, 150)));
            AddButton(new GUIButton(Content, "repair_turret", new Vector2(385, 150)));

            AddLabel(new GUILabel(guiManager.UpgradeFont, "100 SC", new Vector2(260, 205)));
            AddLabel(new GUILabel(guiManager.UpgradeFont, "100 SC", new Vector2(325, 205)));
            AddLabel(new GUILabel(guiManager.UpgradeFont, "100 SC", new Vector2(390, 205)));

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
            else if (ButtonPressed(1))
            {
                GameManager.UpgradeLaserPower();
                GameManager.UpgradeTurretPower();
            }
            else if (ButtonPressed(2))
            {
                GameManager.UpgradeLaserSpeed();
                GameManager.UpgradeTurretSpeed();
            }
            else if (ButtonPressed(3))
            {
                GameManager.UpgradeLaserSpread();
                GameManager.UpgradeTurretSpread();
            }
            else if (ButtonPressed(4))
            {
                GameManager.UpgradeTurretHealth(true);
            }
            else if (ButtonPressed(5))
            {
                GameManager.UpgradeBaseHealth();
            }
            else if (ButtonPressed(6))
            {
                GameManager.UpgradeTurretHealth(false);
            }
            else if (ButtonPressed(7))
            {
                GameManager.UpgradeForcefieldStrength();
            }
            else if (ButtonPressed(8))
            {
                GameManager.UpgradeForcefieldRecharge();
            }
            else if (ButtonPressed(9))
            {
                GameManager.ActivateTurret(true);
            }
            else if (ButtonPressed(10))
            {
                GameManager.ActivateForcefield();
            }
            else if (ButtonPressed(11))
            {
                GameManager.ActivateTurret(false);
            }
            else if (ButtonPressed(12))
            {
                GameManager.RepairBuilding("leftturret");
            }
            else if (ButtonPressed(13))
            {
                GameManager.RepairBuilding("base");
            }
            else if (ButtonPressed(14))
            {
                GameManager.RepairBuilding("rightturret");
            }
        }
    }
}
