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

        public Turret(ContentManager content, int groundLevel, Enemy.EnemyDirection enemyDirection) : base(content, groundLevel)
        {
            SetTexture("turret");

            Y = groundLevel - Height;

            if (enemyDirection == Enemy.EnemyDirection.ToLeft)
            {
                X = Game.GAME_WIDTH / 2 - Width / 2 + 290;
                view = new Rectangle((int)X + Width, 0, Game.GAME_WIDTH - (int)X - Width, (int)Y + Height);
            }
            else
            {
                X = Game.GAME_WIDTH / 2 - Width / 2 - 290;
                view = new Rectangle(0, 0, (int)X, (int)Y + Height);
            }

            laserGun = new LaserGun(content, (int)X, (int)Y + Height / 3);

            if (enemyDirection == Enemy.EnemyDirection.ToRight)
                laserGun.PointAt(0, (int)laserGun.Y + laserGun.Height / 2);

            healthBar.MaxHealth = 500;
            healthBar.Height /= 2;
            healthBar.Width /= 2;

            layerDepth += .01f;
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
            laserGun.Draw(spriteBatch);
        }
    }
}
