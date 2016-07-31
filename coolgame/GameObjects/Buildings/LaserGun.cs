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

        private int baseDamage;

        private Turret turretParent;
        private Tower towerParent;

        public int AttackPowerLevel
        {
            get { return powerLevel; }
            set
            {
                powerLevel = value;
                attackPower = powerLevel * baseDamage;
                if (value == 4)
                {
                    if (speedLevel + spreadLevel < 4)
                        SetTexture("laserGun1_green");
                    else
                        SetTexture("laserGun2_green");
                }
            }
        }

        public int SpeedLevel
        {
            get { return speedLevel; }
            set
            {
                speedLevel = value;
                cooldown = (int)(200 / Math.Pow(1.5f, value - 1));
                recoilRecovery = 0.018f * (float)Math.Pow(1.5f, value - 1);

                if (speedLevel + spreadLevel >= 4)
                {
                    if (powerLevel < 4)
                        SetTexture("laserGun2");
                    else
                        SetTexture("laserGun2_green");

                    if (towerParent != null)
                        towerParent.FixGunPosition();
                    else if (turretParent != null)
                        turretParent.FixGunPosition();
                }
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

                if (speedLevel + spreadLevel >= 4)
                {
                    if (powerLevel < 4)
                        SetTexture("laserGun2");
                    else
                        SetTexture("laserGun2_green");

                    if (towerParent != null)
                        towerParent.FixGunPosition();
                    else if (turretParent != null)
                        turretParent.FixGunPosition();
                }
            }
        }

        public LaserGun(ContentManager content, int x, int y, int baseDamage, Turret parent) : base(content)
        {
            Initialize(content, x, y, baseDamage);
            turretParent = parent;
        }

        public LaserGun(ContentManager content, int x, int y, int baseDamage, Tower parent) : base(content)
        {
            Initialize(content, x, y, baseDamage);
            towerParent = parent;
        }

        private void Initialize(ContentManager content, int x, int y, int baseDamage)
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
            this.baseDamage = baseDamage;

            //Upgrades
            AttackPowerLevel = 1;
            SpeedLevel = 1;
            SpreadLevel = 1;
        }

        public int DefaultX
        {
            get { return defaultX; }
            set
            {
                defaultX = value;
                X = value;
            }
        }

        public int DefaultY
        {
            get { return defaultY; }
            set
            {
                defaultY = value;
                Y = value;
            }
        }

        public string TextureName
        {
            get { return texture.Name; }
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
                LaserProjectile p = new PlayerProjectile(content, projectileX, projectileY, Rotation, attackPower, powerLevel, turretParent != null);

                //Recoil
                //initial offset
                X -= recoilOffset * Math.Cos(Rotation);
                Y -= recoilOffset * Math.Sin(Rotation);

                //travel
                velocity = new Vector2(recoilAcceleration * (float)Math.Cos(Rotation), recoilAcceleration * (float)Math.Sin(Rotation));

                for (int i = 0; i < auxiliaryProjectiles; ++i)
                {
                    p = new PlayerProjectile(content, projectileX, projectileY, Rotation + ((float)random.NextDouble() - .5f) * maxSpread, attackPower, powerLevel, turretParent != null);
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
            if (Math.Abs(defaultX - X) > Math.Abs(10 * Math.Cos(Rotation)))
            {
                X = defaultX - 10 * Math.Cos(Rotation);
            }
            if (Math.Abs(defaultY - Y) > Math.Abs(10 * Math.Sin(Rotation)))
            {
                Y = defaultY - 10 * Math.Sin(Rotation);
            }

            base.Update(deltaTime);
        }
    }
}
