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
            healthBar.MaxHealth = 1000;
            healthBar.Width = 100;
            healthBar.Height = 10;
            layerDepth = LayerManager.GetLayerDepth(Layer.Buildings);

            tower = new Tower(content, groundLevel, (int)X);

            collisionBox.X = Math.Min(collisionBox.X, tower.CollisionBox.X);
            collisionBox.Y = Math.Min(collisionBox.Y, tower.CollisionBox.Y);
            collisionBox.Width = Math.Max(collisionBox.Width, tower.CollisionBox.Width);
            collisionBox.Height = Math.Max(collisionBox.Height, tower.CollisionBox.Height);

            EnableHealthBar = true;
            healthBar.X -= 20;
            //healthBar.X = collisionBox.X + collisionBox.Width / 2;
            //healthBar.Y = collisionBox.Y - 20;
        }

        public override void Update(float deltaTime, InputManager input)
        {
            base.Update(deltaTime, input);

            if (Alive)
                tower.Update(deltaTime, input);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Alive)
                tower.Draw(spriteBatch);
        }
    }
}
