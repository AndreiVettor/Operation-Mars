using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public abstract class Building : Entity
    {
        private int maxHealth;
        private int health;
        private HealthBar healthBar;

        public Building(ContentManager content) : base()
        {
            healthBar = new HealthBar(content);
            healthBar.X = X + Width / 2;
            healthBar.Y = Y - 20;
        }

        public override int X
        {
            get { return base.X; }
            set
            {
                base.X = value;
                healthBar.X = value + Width / 2;
            }
        }

        public override int Y
        {
            get { return base.Y; }
            set
            {
                base.Y = value;
                healthBar.Y = Y - 20;
            }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
            set
            {
                maxHealth = value;
                healthBar.Value = (float)health / maxHealth;
            }
        }

        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                healthBar.Value = (float)health / maxHealth;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            healthBar.Draw(spriteBatch);
        }
    }
}
