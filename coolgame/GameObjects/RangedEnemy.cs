using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace coolgame
{
    public abstract class RangedEnemy : Enemy
    {
        private int minRange;
        private int maxRange;
        private int actualRange;
        private Rectangle rangeBox;
        private Random random = new Random();

        protected int MinRange
        {
            get { return minRange; }
            set
            {
                minRange = value;

                if (value < maxRange)
                {
                    actualRange = random.Next(value, maxRange + 1);
                    rangeBox.X = (int)X - actualRange;
                    rangeBox.Width = Width + 2 * actualRange;
                }      
            }
        }

        protected int MaxRange
        {
            get { return maxRange; }
            set
            {
                maxRange = value;

                if (minRange < value)
                {
                    actualRange = random.Next(value, maxRange + 1);
                    rangeBox.X = (int)X - actualRange;
                    rangeBox.Width = Width + 2 * actualRange;
                }
            }
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
                rangeBox.X = (int)X - actualRange;
            }
        }

        public override double Y
        {
            get
            {
                return base.Y;
            }

            set
            {
                base.Y = value;
                rangeBox.Y = (int)Y;
            }
        }

        public override int Width
        {
            get
            {
                return base.Width;
            }

            set
            {
                base.Width = value;
                rangeBox.Width = Width + 2 * actualRange;
            }
        }

        public override int Height
        {
            get
            {
                return base.Height;
            }

            set
            {
                base.Height = value;
                rangeBox.Height = value;
            }
        }

        public RangedEnemy(ContentManager Content) : base(Content)
        {
            rangeBox = new Rectangle();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            attackCooldown += deltaTime;
            target = CollisionManager.CollidesWithBuilding(rangeBox);

            if (target == null)
            {
                if (Direction == EnemyDirection.ToLeft)
                    X -= movingSpeed / 100 * deltaTime;
                else if (Direction == EnemyDirection.ToRight)
                    X += movingSpeed / 100 * deltaTime;
                EnableAnimation = true;
            }
            else
            {
                EnableAnimation = false;
                if (attackCooldown >= 1000f / attackSpeed)
                {
                    double projectileX;
                    float projectileDirection;
                    if (direction == EnemyDirection.ToLeft)
                    {
                        projectileX = X - 50;
                        projectileDirection = (float)Math.PI;
                    }
                    else
                    {
                        projectileX = X + Width + 50;
                        projectileDirection = 0;
                    }
                    EnemyProjectile p = new EnemyProjectile(content, projectileX, Y + Height / 2, projectileDirection, attackPower);
                    attackCooldown = 0;
                    if (attackSound != null)
                        SoundManager.PlayClip(attackSound);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //spriteBatch.Draw(content.Load<Texture2D>("tile"), rangeBox, Color.White);
        }
    }
}
