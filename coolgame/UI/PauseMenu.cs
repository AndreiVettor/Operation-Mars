using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class PauseMenu : UIWindow
    {
        public PauseMenu(ContentManager Content) : base(Content, Vector2.Zero, 0, 0)
        {
            spacing = 20;
            AddItem(new Button(Content, Vector2.Zero, 140, 40, "RESUME"));
            AddItem(new Button(Content, Vector2.Zero, 140, 40, "EXIT GAME"));
        }
    }
}
