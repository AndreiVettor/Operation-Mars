using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace coolgame.GUI
{
    public class GUISprite : GUIElement
    {
        public GUISprite (ContentManager Content, string textureName, Vector2 position) : base (Content, textureName)
        {
            Position = position;
            TextAlpha = 0;
            text = "";
        }
    }
}
