using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace coolgame.UI
{
    class InfoPanel : UIWindow
    {
        public InfoPanel (ContentManager Content, string textureName) : base(Content,textureName, Vector2.Zero)
        {
            position.X = Game.GAME_WIDTH / 2 - Width / 2;
            position.Y = Game.GAME_HEIGHT / 2 - Height / 2;
        }
    }
}
