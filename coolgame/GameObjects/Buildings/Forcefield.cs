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

        private int rechargeLevel;
        private int strengthLevel;

        public bool Activated { get; set; }

        public int RechargeLevel
        {
            get { return rechargeLevel; }
            set
            {
                rechargeLevel = value;
                rechargeRate = value * 2;
                rechargePower = (int)(5 * Math.Pow(1.5f, value));
            }
        }

        public int StrengthLevel
        {
            get { return strengthLevel; }
            set
            {
                strengthLevel = value;

                int currentHealth = healthBar.Health;
                healthBar.MaxHealth = (int)(600 * Math.Pow(1.5f, value - 1));
                if (value > 1)
                    healthBar.Health = currentHealth;

                if (value == 3)
                    SetTexture("forcefield2");
            }
        }

        public Forcefield(ContentManager content, int groundLevel) : base(content, groundLevel)
        {
            SetTexture("forceField1");
            X = Game.GAME_WIDTH / 2 - Width / 2;
            Y = groundLevel - Height + 5;

            healthBar.ColorScheme = HealthBar.HealthBarColoring.Forcefield;

            this.layerDepth = LayerManager.GetLayerDepth(Layer.Forcefields);

            Alive = false;

            RechargeLevel = 1;
            StrengthLevel = 1;
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
