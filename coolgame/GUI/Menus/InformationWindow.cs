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
            textPadding = new Vector2(13, 5);
            Center();
            AddButton(new GUIButton(Content, guiManager.SmallFont, "Close", new Vector2(Width/2 - 30, Height - 55),textPadding));
            TweakButtons(false, true, false, true,0);
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
