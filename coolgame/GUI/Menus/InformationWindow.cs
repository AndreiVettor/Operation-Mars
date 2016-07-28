using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace coolgame.GUI.Menus
{
    class InformationWindow : GUIWindow
    {
        public InformationWindow(ContentManager Content, string textureName, SpriteFont textFont) : base(Content, textureName)
        {
            borderPadding = new Vector2(30, 30);
            Center();
            AddButton(new GUIButton(Content, textFont, "X", new Vector2(0, 0)));
            //NormalizeButtonLength(false, false, 0);
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
