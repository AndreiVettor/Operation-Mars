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
            spacing = 15;
            AddItem(new Button(Content, Vector2.Zero, "RESUME"));
            AddItem(new Button(Content, Vector2.Zero, "MUTE SOUND"));
            AddItem(new Button(Content, Vector2.Zero, "RESTART"));
            AddItem(new Button(Content, Vector2.Zero, "BACK TO START MENU"));
            AddItem(new Button(Content, Vector2.Zero, "EXIT GAME"));
            ArrangeMenu();
        }
    }
}
