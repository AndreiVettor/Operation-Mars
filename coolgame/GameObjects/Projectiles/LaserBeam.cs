using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace coolgame
{
    public class LaserBeam : Entity
    {
        Murderbot shooter;

        public LaserBeam(ContentManager content, Murderbot shooter) : base(content)
        {
            SetTexture("electrobeam");
            color = Color.Blue;
            GameManager.AddEntity(this);
            layerDepth = LayerManager.GetLayerDepth(Layer.Buildings);
            this.shooter = shooter;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            this.Alive = shooter.Alive;
        }
    }
}
