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
        private int groundLevel;
        private int healthLevel;
        
        public LaserGun Gun
        {
            get { return tower.Gun; }
        }

        public int HealthLevel
        {
            get { return healthLevel; }
            set
            {
                healthLevel = value;
                healthBar.MaxHealth = value * 200;

                if (value == 3)
                {
                    SetTexture("base2");
                    tower.SetTexture("tower2");
                    tower.Y = groundLevel - tower.Height;
                    X = Game.GAME_WIDTH / 2 - Width / 2 - 50;
                    Y = groundLevel - Height;
                    layerDepth -= .02f;
                    collisionBox.X = Math.Min(collisionBox.X, tower.CollisionBox.X);
                    collisionBox.Y = Math.Min(collisionBox.Y, tower.CollisionBox.Y);
                    collisionBox.Width = Math.Max(collisionBox.X + collisionBox.Width, tower.CollisionBox.X + tower.CollisionBox.Width) - collisionBox.X;
                    collisionBox.Height = Math.Max(collisionBox.Height, tower.CollisionBox.Height);
                }
            }
        }

        public Base(ContentManager content, int groundLevel) : base(content, groundLevel)
        {
            layerDepth += .02f;

            SetTexture("base1");
            X = Game.GAME_WIDTH / 2 - Width / 2 - 50;
            Y = groundLevel - Height;
            this.groundLevel = groundLevel;

            tower = new Tower(content, groundLevel, (int)X);

            collisionBox.X = Math.Min(collisionBox.X, tower.CollisionBox.X);
            collisionBox.Y = Math.Min(collisionBox.Y, tower.CollisionBox.Y);
            collisionBox.Width = Math.Max(collisionBox.X + collisionBox.Width, tower.CollisionBox.X + tower.CollisionBox.Width) - collisionBox.X;
            collisionBox.Height = Math.Max(collisionBox.Height, tower.CollisionBox.Height);

            healthBar.X -= 20;
            //healthBar.X = collisionBox.X + collisionBox.Width / 2;
            //healthBar.Y = collisionBox.Y - 20;

            HealthLevel = 1;
        }

        public override void InflictDamage(int hitpoints)
        {
            base.InflictDamage(hitpoints);

            if(!Alive)
            {
                GameManager.GameOver = true;
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            tower.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            tower.Draw(spriteBatch);
        }
    }
}
