using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame
{
    class Button : UIElement
    {
        public Button (Vector2 position, int width, int height) : base (position,  width,  height) {
            this.text = "Button";
        }
    }
}
