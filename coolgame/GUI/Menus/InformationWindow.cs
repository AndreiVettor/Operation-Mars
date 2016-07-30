using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace coolgame.GUI.Menus
{
    class InformationWindow : GUIWindow
    {
        public InformationWindow(ContentManager Content, string textureName, GUIManager guiManager) : base(Content, textureName)
        {
            borderPadding = new Vector2(0, 0);
            textPadding = new Vector2(18, 10);
            Center();
            AddButton(new GUIButton(Content, guiManager.MediumFont, "x", new Vector2(566, 7),textPadding));
            TweakButtons(false, true, false, 0);
            SecondaryColor = Color.DarkSlateBlue;
        }

        public override void Update(Game game, ContentManager Content, GUIManager guiManager, EnemySpawner spawner)
        {
            base.Update(game, Content, guiManager, spawner);

            if (ButtonPressed(0))
            {
                Disabled = true;
            }
        }
    }
}
