using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace coolgame
{
    public class Murderbot : Enemy
    {
        private int range;
        private Rectangle rangeBox;
        private Electrobeam beam;

        public Murderbot(ContentManager Content) : base(Content)
        {
            rangeBox = new Rectangle();

            SetTexture("murderbot");
            //Width = 58;
            //Height = 80;
            //EnableAnimation = true;

            healthBar.MaxHealth = 150;
            movingSpeed = 9f;
            attackSpeed = 30f;
            attackPower = 5;

            Range = GameManager.RNG.Next(100, 200);

            //attackSound = "enemylaser";
        }

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

                beam = null;
            }
            else
            {
                bool forceField = typeof(Forcefield).IsInstanceOfType(target);

                if (beam == null)
                {
                    int beamX;
                    int beamY = Math.Max((int)Y + Height / 2, (int)target.Y + target.Height / 2);
                    int beamTargetX;

                    if (direction == EnemyDirection.ToLeft)
                    {
                        if (forceField)
                        {
                            beamX = (int)target.X + target.Width;
                            beamTargetX = (int)X;
                        }
                        else
                        {
                            beamX = (int)X;
                            beamTargetX = (int)target.X + target.Width;
                        }
                    }
                    else
                    {
                        if (forceField)
                        {
                            beamX = (int)target.X;
                            beamTargetX = (int)X + Width;
                        }
                        else
                        {
                            beamX = (int)X + Width;
                            beamTargetX = (int)target.X;
                        }
                    }

                    beam = new Electrobeam(content, beamX, beamY, beamTargetX, forceField);
                }

                if (attackCooldown >= 1000f / attackSpeed)
                {
                    target.InflictDamage(attackPower);

                    if (forceField)
                    {
                        healthBar.ColorScheme = HealthBar.HealthBarColoring.Forcefield;
                        healthBar.Health += attackPower;
                    }
                    else
                        healthBar.ColorScheme = HealthBar.HealthBarColoring.Normal;

                    attackCooldown = 0;
                }

                EnableAnimation = false;
            }

            if (beam != null)
                beam.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (beam != null)
                beam.Draw(spriteBatch);

            //spriteBatch.Draw(content.Load<Texture2D>("tile"), rangeBox, Color.White);
        }
    }
}
