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
        private double pathLeft, pathRight;
        private float altitudeVariation;
        private float altitudeVariationModifier;
        private float ebriety;

        public ReptilianSaucer(ContentManager Content) : base(Content)
        {
            SetTexture("ufo");
            Y = GameManager.RNG.Next(40, 200);
            detectionBox = new Rectangle();
            detectionBox.Y = (int)Y;
            detectionBox.Height = Game.GAME_HEIGHT - detectionBox.Y;
            detectionBox.Width = Width;
            movingSpeed = 10;
            attackSpeed = 1;
            attackPower = 100;
            healthBar.MaxHealth = 150;
            attackSound = "enemylaser";
            ebriety = GameManager.RNG.Next(0, 200);
            altitudeVariationModifier = (float)GameManager.RNG.NextDouble() / 2 + .5f;
            spaceCash = 6;
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

            if (target != null && !target.Alive)
                target = null;

            if (target == null)
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

                pathLeft = target.X - ebriety;
                pathRight = target.X + target.Width + ebriety;
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
            Y = Y - altitudeVariationModifier * Math.Sin(altitudeVariation / 300) * deltaTime * 6 / 100;
        }
    }
}
