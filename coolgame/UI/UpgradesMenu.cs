using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    class UpgradesMenu : UIWindow
    {
        public UpgradesMenu(ContentManager Content) : base(Content, Vector2.Zero, 0, 0)
        {
            spacing = 15;
            AddItem(new Button(Content, Vector2.Zero, "Increase Laser Power"));
            AddItem(new Button(Content, Vector2.Zero, "Increase Laser Speed"));
            AddItem(new Button(Content, Vector2.Zero, "Increase Laser Spread"));
        }
    }
}
