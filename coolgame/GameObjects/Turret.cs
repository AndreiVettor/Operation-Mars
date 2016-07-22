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
        private Vector2 projectileOrigin;
        private float cooldownTime;

        public Turret(ContentManager content, int groundLevel, Enemy.EnemyDirection enemyDirection) : base(content, groundLevel)
        {
            SetTexture("shitturret");

            Y = groundLevel - Height;

            if (enemyDirection == Enemy.EnemyDirection.ToLeft)
            {
                X = Game.GAME_WIDTH / 2 - Width / 2 + 260;
                Effects = SpriteEffects.FlipHorizontally;
                projectileOrigin = new Vector2((int)X + Width, (int)Y + 50);
            }
            else
            {
                X = Game.GAME_WIDTH / 2 - Width / 2 - 260;
                Effects = SpriteEffects.None;
                projectileOrigin = new Vector2((int)X, (int)Y + 50);
            }

            healthBar.MaxHealth = 500;

            if (enemyDirection == Enemy.EnemyDirection.ToLeft)
            {
                view = new Rectangle((int)X + Width, 0, Game.GAME_WIDTH - (int)X - Width, (int)Y + Height);
            }
            else
            {
                view = new Rectangle(0, 0, (int)X, (int)Y + Height);
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            cooldownTime += deltaTime;

            target = CollisionManager.CollidesWithEnemy(view);

            if (target != null)
            {
                if (cooldownTime >= 200.0f)
                {
                    float projectileAngle = (float)Math.Atan2(target.Y + target.Height / 2 - projectileOrigin.Y,
                    target.X + target.Width / 2 - projectileOrigin.X);

                    PlayerProjectile p = new PlayerProjectile(content, projectileOrigin.X, projectileOrigin.Y, projectileAngle);
                    SoundManager.PlayClip("laser");

                    cooldownTime = 0;
                }
            }
        }
    }
}
