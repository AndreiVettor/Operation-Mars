using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace coolgame
{
    public class ElectrobeamComponent : Entity
    {
        private int targetx;
        private bool toLeft;

        public ElectrobeamComponent(ContentManager content, int x, int y, int targetx) : base(content)
        {
            SetTexture("electrobeamring");
            X = x - Width / 2;
            Y = y = Height / 2;
            this.targetx = targetx;
            toLeft = x > targetx;
            layerDepth = LayerManager.GetLayerDepth(Layer.Projectiles);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (toLeft)
                X -= .1f * deltaTime;
            else
                X += .1f * deltaTime;

            if ((X > targetx && !toLeft) || (X < targetx && toLeft))
                color = new Color(color, Math.Abs((float)(1 / (X - targetx))));
        }
    }
}
