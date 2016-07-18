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
    public class Base : Entity
    {
        public Base(ContentManager content, int groundLevel) : base(content)
        {
            SetTexture(content, "base");
            Width = texture.Width;
            Height = texture.Height;
            X = Game.GAME_WIDTH / 2 - Width / 2;
            Y = groundLevel - Height;
            EnableHealthBar = true;
            healthBar.MaxHealth = 1000;
            healthBar.Width = 100;
            healthBar.Height = 10;
        }

        public override void Update(float deltaTime, InputManager input)
        {
            base.Update(deltaTime, input);
        }
    }
}
