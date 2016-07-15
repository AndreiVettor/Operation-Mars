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

        public Building(ContentManager content, int width, int height) : base(null, width, height)
        {
            healthBar = new HealthBar(content);
        }
    }
}
