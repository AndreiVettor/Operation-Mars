using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame.UI
{
    class StartMenu : UIWindow
    {
        public StartMenu(ContentManager Content) : base(Content, Vector2.Zero, 0, 0)
        {
            spacing = 20;
            AddItem(new Button(Content, Vector2.Zero, 140, 40, "START GAME"));
            AddItem(new Button(Content, Vector2.Zero, 140, 40, "ABOUT THE GAME"));
            AddItem(new Button(Content, Vector2.Zero, 140, 40, "LOAD GAME?"));
        }
    }
}
