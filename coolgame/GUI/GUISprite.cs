using coolgame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame.GUI
{
    class GUISprite : GUIElement
    {
        public GUISprite (ContentManager Content, string textureName, Vector2 position) : base (Content, textureName)
        {
            Position = position;
            TextAlpha = 0;
            text = "";
        }
    }
}
