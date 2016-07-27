using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame.UI
{
    public class GUILabel : GUIElement
    {
        public GUILabel(ContentManager Content, SpriteFont textFont, string text, Vector2 position) : base(Content)
        {
            SetText(textFont, text);
            Position = position;
            BackgroundAlpha = 0;
        }
    }
}
