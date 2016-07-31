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
        public float Damage
        {
            get { return (float)(healthBar.MaxHealth - healthBar.Health) / healthBar.MaxHealth; }
        }

        public override bool Alive
        {
            get
            {
                return base.Alive;
            }

            set
            {
                base.Alive = value;

                if (value)
                    healthBar.MaxHealth = healthBar.MaxHealth;
            }
        }

        public Building(ContentManager content, int groundLevel) : base(content)
        {
            healthBar.Width = 100;
            healthBar.Height = 10;
            layerDepth = LayerManager.GetLayerDepth(Layer.Buildings);
            EnableHealthBar = true;
        }

        public virtual void Reset()
        {
            healthBar.Health = healthBar.MaxHealth;
        }

        public override void InflictDamage(int hitpoints)
        {
            if(!GameManager.godMode)
            {
                base.InflictDamage(hitpoints);
            }
        }
    }
}
