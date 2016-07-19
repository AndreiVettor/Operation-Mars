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
        private Tower tower;

        public Base(ContentManager content, int groundLevel) : base(content)
        {
            SetTexture(content, "base");
            Width = texture.Width;
            Height = texture.Height;
            X = Game.GAME_WIDTH / 2 - Width / 2 - 50;
            Y = groundLevel - Height;
            EnableHealthBar = true;
            healthBar.MaxHealth = 1000;
            healthBar.Width = 100;
            healthBar.Height = 10;
            layerDepth = LayerManager.GetLayerDepth(Layer.Buildings);

            tower = new Tower(content, groundLevel, (int)X);
        }

        public override void Update(float deltaTime, InputManager input)
        {
            base.Update(deltaTime, input);
            tower.Update(deltaTime, input);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            tower.Draw(spriteBatch);
        }
    }
}
