using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace coolgame.GUI.Menus
{
    class UpgradeMenu : GUIWindow
    {
        public UpgradeMenu(ContentManager Content, GUIManager guiManager, EnemySpawner enemySpawner) : base(Content)
        {
            Width = 500;
            Height = 410;
            textPadding = new Vector2(15, 15);
            borderPadding = new Vector2(30, 30);
            Center();

            AddButton(new GUIButton(Content, guiManager.MediumFont, "  NEXT WAVE  ", new Vector2(270, 290),textPadding));
            AddLabel(new GUILabel(
                guiManager.BigFont, "Day " + enemySpawner.Wave + " survived",
                new Vector2(0, 0)));

            //Lasers
            AddLabel(new GUILabel(guiManager.MediumFont, "Upgrade Lasers", new Vector2(25, 50)));

            AddButton(new GUIButton(Content, "up_laserDamage", new Vector2(0, 80),"Upgrade Laser Damage"));
            AddButton(new GUIButton(Content, "up_laserSpeed", new Vector2(65, 80), "Upgrade Laser Speed"));
            AddButton(new GUIButton(Content, "up_laserSpread", new Vector2(130, 80), "Upgrade Laser Spread"));

            AddLabel(new GUILabel(guiManager.SmallFont, "100 SC", new Vector2(5, 135)));
            AddLabel(new GUILabel(guiManager.SmallFont, "100 SC", new Vector2(70, 135)));
            AddLabel(new GUILabel(guiManager.SmallFont, "100 SC", new Vector2(135, 135)));

            //Upgrade Buildings
            AddLabel(new GUILabel(guiManager.MediumFont, "Upgrade Buildings", new Vector2(15, 170)));

            AddButton(new GUIButton(Content, "up_turret", new Vector2(0, 200), "Upgrade Left Turret Health"));
            AddButton(new GUIButton(Content, "up_base", new Vector2(65, 200), "Upgrade Base Health"));
            AddButton(new GUIButton(Content, "up_turret", new Vector2(130, 200), "Upgrade Right Turret Health"));
            AddButton(new GUIButton(Content, "up_forceField", new Vector2(40, 275), "Upgrade Force Field Strength"));
            AddButton(new GUIButton(Content, "up_forceFieldRegen", new Vector2(105, 275), "Upgrade Force Field Regeneration"));

            AddLabel(new GUILabel(guiManager.SmallFont, "100 SC", new Vector2(5, 255)));
            AddLabel(new GUILabel(guiManager.SmallFont, "100 SC", new Vector2(70, 255)));
            AddLabel(new GUILabel(guiManager.SmallFont, "100 SC", new Vector2(135, 255)));
            AddLabel(new GUILabel(guiManager.SmallFont, "100 SC", new Vector2(45, 330)));
            AddLabel(new GUILabel(guiManager.SmallFont, "100 SC", new Vector2(110, 330)));

            //Build
            AddLabel(new GUILabel(guiManager.MediumFont, "Build", new Vector2(330, 50)));

            AddButton(new GUIButton(Content, "build_turret", new Vector2(260, 80), "Build Left Turret"));
            AddButton(new GUIButton(Content, "build_forceField", new Vector2(325, 80), "Build Force Field Generator"));
            AddButton(new GUIButton(Content, "build_turret", new Vector2(390, 80), "Build Right Turret"));

            AddLabel(new GUILabel(guiManager.SmallFont, "100 SC", new Vector2(265, 135)));
            AddLabel(new GUILabel(guiManager.SmallFont, "100 SC", new Vector2(330, 135)));
            AddLabel(new GUILabel(guiManager.SmallFont, "100 SC", new Vector2(395, 135)));

            //Repair
            AddLabel(new GUILabel(guiManager.MediumFont, "Repair", new Vector2(320, 170)));

            AddButton(new GUIButton(Content, "repair_turret", new Vector2(255, 200), "Repair Left Turret"));
            AddButton(new GUIButton(Content, "repair_base", new Vector2(320, 200), "Repair Base"));
            AddButton(new GUIButton(Content, "repair_turret", new Vector2(385, 200), "Repair Right Turret"));

            AddLabel(new GUILabel(guiManager.SmallFont, "100 SC", new Vector2(260, 255)));
            AddLabel(new GUILabel(guiManager.SmallFont, "100 SC", new Vector2(325, 255)));
            AddLabel(new GUILabel(guiManager.SmallFont, "100 SC", new Vector2(390, 255)));

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
                GameManager.ApplyUpgrade(0);
            }
            else if (ButtonPressed(2))
            {
                GameManager.ApplyUpgrade(1);
            }
            else if (ButtonPressed(3))
            {
                GameManager.ApplyUpgrade(2);
            }
            else if (ButtonPressed(4))
            {
                GameManager.ApplyUpgrade(7);
            }
            else if (ButtonPressed(5))
            {
                GameManager.ApplyUpgrade(5);
            }
            else if (ButtonPressed(6))
            {
                GameManager.ApplyUpgrade(6);
            }
            else if (ButtonPressed(7))
            {
                GameManager.ApplyUpgrade(4);
            }
            else if (ButtonPressed(8))
            {
                GameManager.ApplyUpgrade(3);
            }
            else if (ButtonPressed(9))
            {
                GameManager.ApplyUpgrade(10);
            }
            else if (ButtonPressed(10))
            {
                GameManager.ApplyUpgrade(8);
            }
            else if (ButtonPressed(11))
            {
                GameManager.ApplyUpgrade(9);
            }
            else if (ButtonPressed(12))
            {
                GameManager.ApplyUpgrade(12);
            }
            else if (ButtonPressed(13))
            {
                GameManager.ApplyUpgrade(11);
            }
            else if (ButtonPressed(14))
            {
                GameManager.ApplyUpgrade(13);
            }
        }
    }
}
