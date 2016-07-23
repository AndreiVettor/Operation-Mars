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
        private LaserBeam beam;

        public Murderbot(ContentManager Content) : base(Content)
        {
            rangeBox = new Rectangle();

            SetTexture("murderbot");
            //Width = 58;
            //Height = 80;
            //EnableAnimation = true;

            healthBar.MaxHealth = 500;
            movingSpeed = 9f;
            attackSpeed = 1f;
            attackPower = 30;

            Range = GameManager.RNG.Next(100, 250);

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

            target = CollisionManager.CollidesWithBuilding(rangeBox);

            if (target == null)
            {
                if (Direction == EnemyDirection.ToLeft)
                    X -= movingSpeed / 100 * deltaTime;
                else if (Direction == EnemyDirection.ToRight)
                    X += movingSpeed / 100 * deltaTime;
                EnableAnimation = true;

                if (beam != null)
                {
                    beam.Alive = false;
                }
            }
            else
            {
                if (beam == null)
                    beam = new LaserBeam(content, this);

                int beamWidth;

                if (direction == EnemyDirection.ToLeft)
                {
                    beam.X = target.X + target.Width;
                    beamWidth = (int)X - (int)beam.X;
                }
                else
                {
                    beam.X = X + Width;
                    beamWidth = (int)target.X - (int)beam.X;
                }

                beam.Y = (int)Y + Height / 2 - beam.Height / 2;

                beam.Scale = new Vector2((float)beamWidth / beam.Width, 1);

                EnableAnimation = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //spriteBatch.Draw(content.Load<Texture2D>("tile"), rangeBox, Color.White);
        }
    }
}
