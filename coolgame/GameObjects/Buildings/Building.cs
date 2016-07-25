using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class Building : Entity
    {
        public Building(ContentManager content, int groundLevel) : base(content)
        {
            healthBar.Width = 100;
            healthBar.Height = 10;
            layerDepth = LayerManager.GetLayerDepth(Layer.Buildings);
            EnableHealthBar = true;
        }

        public virtual void Upgrade() { }
    }
}
