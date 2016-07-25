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
        private float cooldownTime;
        private Random random;

        private Vector2 velocity;
        private int defaultX, defaultY;
        private int recoilOffset;
        private float recoilAcceleration;
        private float recoilRecovery;

        private int auxiliaryProjectiles;
        private float maxSpread;
        private float cooldown;

        private int attackPower;
        public int AttackPower
        {
            get { return attackPower; }
            set
            {
                attackPower = value * 10;
            }
        }

        public LaserGun(ContentManager content, int x, int y) : base(content)
        {
            SetTexture("laser");
            Width = texture.Width;
            Height = texture.Height;
            isUpgradeable = true;

            defaultX = x;
            defaultY = y;
            recoilOffset = 6;
            recoilAcceleration = -1;
            recoilRecovery = 0.3f;

            X = x;
            Y = y;
            this.content = content;
            layerDepth = LayerManager.GetLayerDepth(Layer.Buildings);
            random = new Random();
            cooldown = 200f;

            attackPower = 10;

        }

        public void Upgrade()
        {
            SetSpread(GameManager.laser_spread);
            SetCooldown(GameManager.laser_speed);
            AttackPower = GameManager.laser_damage;
            Debug.Log(auxiliaryProjectiles);
        }

        public void SetCooldown(int level)
        {
            cooldown /= level * 1.05f;

            recoilRecovery += level * 0.3f;
            if (recoilOffset < 0) recoilOffset = 0;
        }

        public void SetSpread(int level)
        {
            auxiliaryProjectiles = level;
            maxSpread = (float)Math.PI / 40 * auxiliaryProjectiles;
        }

        public void PointAt(int targetX, int targetY)
        {
            Rotation = (float)Math.Atan2(targetY - Y - Height / 2, targetX - X - Width / 2);
            if(Rotation < -Math.PI/2 || Rotation > Math.PI/2)
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
                LaserProjectile p = new PlayerProjectile(content, projectileX, projectileY, Rotation, attackPower);

                //Recoil
                X -= recoilOffset * Math.Cos(Rotation);
                Y -= recoilOffset * Math.Sin(Rotation);

                velocity = new Vector2(recoilAcceleration * (float)Math.Cos(Rotation), recoilAcceleration * (float)Math.Sin(Rotation));

                for (int i = 0; i < auxiliaryProjectiles; ++i)
                {
                    p = new PlayerProjectile(content, projectileX, projectileY, Rotation + ((float)random.NextDouble() - .5f) * maxSpread, attackPower);
                }
            }
        }

        public override void Update(float deltaTime)
        {
            if (InputManager.KeyPress(Keys.D1))
            {
                auxiliaryProjectiles++;
                maxSpread = (float)Math.PI / 40 * auxiliaryProjectiles;
            }
            if (InputManager.KeyPress(Keys.D2))
            {
                cooldown /= 1.05f;

                recoilRecovery += 0.3f;
                if (recoilOffset < 0) recoilOffset = 0;
            }

            cooldownTime += deltaTime;

            X += velocity.X * deltaTime * 6 / 100;
            Y += velocity.Y * deltaTime * 6 / 100;

            float recoveryAngle = (float)Math.Atan2(Y - defaultY, X - defaultX);
            velocity -= new Vector2(recoilRecovery * (float)Math.Cos(recoveryAngle), recoilRecovery * (float)Math.Sin(recoveryAngle)) *
                deltaTime * 6 / 100;

            //If laser is close enough to default position lock into place
            if (Math.Abs(defaultX - X) < 3)
            {
                X = defaultX;
            }
            if (Math.Abs(defaultY - Y) < 3)
            {
                Y = defaultY;
            }
            if (Math.Abs(defaultX - X) < 3 && Math.Abs(defaultY - Y) < 3)
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
