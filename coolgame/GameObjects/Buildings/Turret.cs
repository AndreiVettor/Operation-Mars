using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace coolgame
{
    public class Turret : Building
    {
        private Rectangle view;
        private Enemy target;
        private LaserGun laserGun;
        private int healthLevel;
        private int groundLevel;

        public int HealthLevel
        {
            get { return healthLevel; }
            set
            {
                healthLevel = value;
                healthBar.MaxHealth = value * 100;

                if (value >= 3)
                {
                    SetTexture("turret2");
                    Y = groundLevel - Height;
                }
            }
        }

        public LaserGun Gun
        {
            get { return laserGun; }
        }

        public Turret(ContentManager content, int groundLevel, Enemy.EnemyDirection enemyDirection) : base(content, groundLevel)
        {
            SetTexture("turret1");

            Y = groundLevel - Height;
            this.groundLevel = groundLevel;

            
            if (enemyDirection == Enemy.EnemyDirection.ToLeft)
            {
                X = Game.GAME_WIDTH / 2 - Width / 2 + 290;
                view = new Rectangle((int)X + Width, 0, Game.GAME_WIDTH - (int)X - Width, (int)Y + Height);
            }
            else
            {
                X = Game.GAME_WIDTH / 2 - Width / 2 - 290;
                view = new Rectangle(0, 0, (int)X, (int)Y + Height);
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            laserGun = new LaserGun(content, (int)X + 15 * (spriteEffects == SpriteEffects.FlipHorizontally ? 1 : 0), (int)Y + 15, 1);

            if (enemyDirection == Enemy.EnemyDirection.ToRight)
                laserGun.PointAt(0, (int)laserGun.Y + laserGun.Height / 2);

            healthBar.Height /= 2;
            healthBar.Width /= 2;

            layerDepth += .01f;

            HealthLevel = 1;

            Alive = false;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            target = CollisionManager.CollidesWithEnemy(view);

            if (target != null)
            {
                laserGun.PointAt((int)target.X + target.Width / 2, (int)target.Y + target.Height / 2);
                laserGun.Shoot();
            }

            laserGun.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Alive)
                laserGun.Draw(spriteBatch);
        }
    }
}
