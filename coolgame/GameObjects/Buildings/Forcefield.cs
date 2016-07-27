using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class Forcefield : Building
    {
        private int rechargePower;
        private float rechargeRate;
        private float rechargeTime;

        public Forcefield(ContentManager content, int groundLevel) : base(content, groundLevel)
        {
            SetTexture("force_field");
            X = Game.GAME_WIDTH / 2 - Width / 2;
            Y = groundLevel - Height;

            healthBar.MaxHealth = 1000;
            healthBar.ColorScheme = HealthBar.HealthBarColoring.Forcefield;
            rechargeRate = 1;
            rechargePower = 5;

            this.layerDepth = LayerManager.GetLayerDepth(Layer.Forcefields);

            Alive = false;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (healthBar.Health < healthBar.MaxHealth)
            {
                rechargeTime += deltaTime;
                if (rechargeTime >= 1000f / rechargeRate )
                {
                    healthBar.Health += rechargePower;
                    rechargeTime = 0;
                }
            }
        }
    }
}
