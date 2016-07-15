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
        protected int maxHealth;
        protected int health;
        private HealthBar healthBar;

        public Building(ContentManager content) : base()
        {
            healthBar = new HealthBar(content);
            healthBar.X = X + Width / 2;
            healthBar.Y = Y - 20;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            healthBar.Draw(spriteBatch);
        }
    }
}
