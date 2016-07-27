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
    public class LaserGun : Entity
    {
        private const float OPTIMAL_UPDATES_PER_MILLISECOND = (float)60 / 1000;

        private float cooldownTime;
        private Random random;

        private Vector2 velocity;
        private int defaultX, defaultY;
        private int recoilOffset;
        private float recoilAcceleration;
        private float recoilRecovery;
        private float lockDistance;

        private int auxiliaryProjectiles;
        private float maxSpread;
        private float cooldown;

        private int attackPower;

        private int powerLevel;
        private int speedLevel;
        private int spreadLevel;

        public int AttackPowerLevel
        {
            get { return attackPower; }
            set
            {
                powerLevel = Math.Min(4, value);
                attackPower = powerLevel * 10;
            }
        }

        public int SpeedLevel
        {
            get { return speedLevel; }
            set
            {
                speedLevel = value;
                cooldown = (int)(200 / Math.Pow(1.5f, value - 1));
            }
        }

        public int SpreadLevel
        {
            get { return spreadLevel; }
            set
            {
                spreadLevel = value;
                auxiliaryProjectiles = value - 1;
                maxSpread = (float)Math.PI / 40 * (value - 1);
            }
        }

        public LaserGun(ContentManager content, int x, int y) : base(content)
        {
            SetTexture("laserGun1");
            Width = texture.Width;
            Height = texture.Height;

            X = x;
            Y = y;
            this.content = content;
            layerDepth = LayerManager.GetLayerDepth(Layer.Buildings);
            random = new Random();

            //Laser Specific
            defaultX = x;
            defaultY = y;
            recoilOffset = 10;
            recoilAcceleration = -1;
            recoilRecovery = 0.018f;
            lockDistance = 3;

            attackPower = 10;
            cooldown = 200f;
        }

        public void PointAt(int targetX, int targetY)
        {
            Rotation = (float)Math.Atan2(targetY - Y - Height / 2, targetX - X - Width / 2);

            //Flip Direction
            if (Rotation < -Math.PI/2 || Rotation > Math.PI/2)
            {
                Effects = SpriteEffects.FlipVertically;
            }
            else
            {
                Effects = SpriteEffects.None;
            }
        }

        public void Shoot()
        {
            if (cooldownTime > cooldown)
            {
                SoundManager.PlayClip("laser");
                cooldownTime = 0;
                double projectileX = X + Width / 2 + Math.Cos(Rotation) * (Width / 4);
                double projectileY = Y + Height / 2 + Math.Sin(Rotation) * (Width / 4);
                LaserProjectile p = new PlayerProjectile(content, projectileX, projectileY, Rotation, attackPower, powerLevel);

                //Recoil
                //initial offset
                X -= recoilOffset * Math.Cos(Rotation);
                Y -= recoilOffset * Math.Sin(Rotation);

                //travel
                velocity = new Vector2(recoilAcceleration * (float)Math.Cos(Rotation), recoilAcceleration * (float)Math.Sin(Rotation));

                for (int i = 0; i < auxiliaryProjectiles; ++i)
                {
                    p = new PlayerProjectile(content, projectileX, projectileY, Rotation + ((float)random.NextDouble() - .5f) * maxSpread, attackPower, powerLevel);
                }
            }
        }

        public override void Update(float deltaTime)
        {
            cooldownTime += deltaTime;

            X += velocity.X * deltaTime * OPTIMAL_UPDATES_PER_MILLISECOND;
            Y += velocity.Y * deltaTime * OPTIMAL_UPDATES_PER_MILLISECOND;

            float recoveryAngle = (float)Math.Atan2(Y - defaultY, X - defaultX);
            velocity -= new Vector2(recoilRecovery * (float)Math.Cos(recoveryAngle), recoilRecovery * (float)Math.Sin(recoveryAngle)) *
                deltaTime;

            //Lock gun into position if distance allows
            if (Math.Abs(defaultX - X) < lockDistance)
            {
                X = defaultX;
            }
            if (Math.Abs(defaultY - Y) < lockDistance)
            {
                Y = defaultY;
            }
            if (Math.Abs(defaultX - X) < lockDistance && Math.Abs(defaultY - Y) < lockDistance)
            {
                velocity = Vector2.Zero;
            }

            //Clamp velocity
            if (velocity.X > -recoilAcceleration)
            {
                velocity.X = -recoilAcceleration;
            }
            if (velocity.Y > -recoilAcceleration)
            {
                velocity.Y = -recoilAcceleration;
            }

            //Outer bounds
            if (Math.Abs(defaultX - X) > 10)
            {
                if (defaultX - X > 0)
                {
                    X = defaultX - 10;
                }
                else {
                    X = defaultX + 10;
                }
            }
            if (Math.Abs(defaultY - Y) > 10)
            {
                if (defaultY - Y > 0)
                {
                    Y = defaultY - 10;
                }
                else
                {
                    Y = defaultY + 10;
                }
            }

            base.Update(deltaTime);
        }
    }
}
