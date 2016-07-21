using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace coolgame
{
    public class ReptilianSaucer : Enemy
    {
        private Rectangle detectionBox;
        private Building target;
        private double pathLeft, pathRight;
        private float altitudeVariation;

        public ReptilianSaucer(ContentManager Content) : base(Content)
        {
            SetTexture("ufo");
            Y = GameManager.RNG.Next(40, 200);
            detectionBox = new Rectangle();
            detectionBox.Y = (int)Y;
            detectionBox.Height = Game.GAME_HEIGHT - detectionBox.Y;
            detectionBox.Width = Width;
            movingSpeed = 10;
            attackPower = 50;
            attackSound = "enemylaser";
        }

        public override double X
        {
            get
            {
                return base.X;
            }

            set
            {
                base.X = value;
                detectionBox.X = (int)value;
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            altitudeVariation += deltaTime;

            attackCooldown += deltaTime;

            target = CollisionManager.CollidesWithForcefield(detectionBox);
            if (target == null)
                target = CollisionManager.CollidesWithBuilding(detectionBox);

            if (target == null)
            {
                pathLeft = Width / 2;
                pathRight = Game.GAME_WIDTH - Width / 2;
            }
            else
            {
                if (attackCooldown >= 1000f / attackSpeed)
                {
                    double projectileX = X + Width / 2;
                    double projectileY = Y + Height;
                    float projectileDirection = (float)Math.PI / 2;
                    EnemyProjectile p = new EnemyProjectile(content, projectileX, projectileY, projectileDirection, attackPower);
                    attackCooldown = 0;
                    if (attackSound != null)
                        SoundManager.PlayClip(attackSound);
                }

                pathLeft = target.X;
                pathRight = target.X + target.Width;
            }

            if (direction == EnemyDirection.ToLeft)
            {
                X -= movingSpeed / 100 * deltaTime;

                if (pathLeft >= X + Width / 2)
                    direction = EnemyDirection.ToRight;
            }
            else
            {
                X += movingSpeed / 100 * deltaTime;

                if (pathRight <= X + Width / 2)
                    direction = EnemyDirection.ToLeft;
            }
            Y = Y - 0.7f * Math.Sin(altitudeVariation / 300);
        }
    }
}
