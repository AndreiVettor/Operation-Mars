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
    public class Base : Building
    {
        private Tower tower;

        public Base(ContentManager content, int groundLevel) : base(content, groundLevel)
        {
            layerDepth += .02f;

            SetTexture(content, "base");
            X = Game.GAME_WIDTH / 2 - Width / 2 - 50;
            Y = groundLevel - Height;
            healthBar.MaxHealth = 100;

            tower = new Tower(content, groundLevel, (int)X);

            collisionBox.X = Math.Min(collisionBox.X, tower.CollisionBox.X);
            collisionBox.Y = Math.Min(collisionBox.Y, tower.CollisionBox.Y);
            collisionBox.Width = Math.Max(collisionBox.X + collisionBox.Width, tower.CollisionBox.X + tower.CollisionBox.Width) - collisionBox.X;
            collisionBox.Height = Math.Max(collisionBox.Height, tower.CollisionBox.Height);

            healthBar.X -= 20;
            //healthBar.X = collisionBox.X + collisionBox.Width / 2;
            //healthBar.Y = collisionBox.Y - 20;
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
