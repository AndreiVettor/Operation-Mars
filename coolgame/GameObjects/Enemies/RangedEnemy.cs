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
        private int range;
        private Rectangle rangeBox;
        protected float precision;
        protected bool burstFire;
        protected int burstFireAmmount;
        protected float burstFireSpeed;
        private int currentBurstProjectile;
        protected string laserAssetName;

        protected int Range
        {
            get { return range; }
            set
            {
                range = value;
                rangeBox.X = (int)X - range;
                rangeBox.Width = Width + 2 * range;
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
                rangeBox.X = (int)X - range;
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
                rangeBox.Width = Width + 2 * range;
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
            precision = 1f;
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
                MosCraciun(false);
            }
            else
            {
                MosCraciun(true);
                if ((attackCooldown >= 1000f / attackSpeed) || 
                    (burstFire && (currentBurstProjectile < burstFireAmmount)) && (attackCooldown >= 1000f / burstFireSpeed))
                {
                    if (burstFire)
                    {
                        ++currentBurstProjectile;
                        if (attackCooldown >= 1000f / attackSpeed)
                            currentBurstProjectile = 1;
                    }

                    double projectileX;
                    float projectileDirection = (float)((GameManager.RNG.NextDouble() - .5f) * Math.PI / precision);
                    if (direction == EnemyDirection.ToLeft)
                    {
                        projectileX = X;
                        projectileDirection += (float)Math.PI;
                    }
                    else
                    {
                        projectileX = X + Width;
                        //projectileDirection = 0;
                    }
                    EnemyProjectile p = new EnemyProjectile(content, projectileX, Y + Height / 2, projectileDirection, attackPower, laserAssetName);
                    attackCooldown = 0;
                    if (attackSound != null)
                        SoundManager.PlayClip(attackSound);
                }
            }
        }

        protected abstract void MosCraciun(bool idle);

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //spriteBatch.Draw(content.Load<Texture2D>("tile"), rangeBox, Color.White);
        }
    }
}
