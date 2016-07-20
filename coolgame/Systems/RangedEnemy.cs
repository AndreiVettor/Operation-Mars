using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace coolgame
{
    public abstract class RangedEnemy : Enemy
    {
        private int minRange;
        private int maxRange;
        private Rectangle rangeBox;
        private Random random;

        protected int MinRange
        {
            get { return minRange; }
            set
            {
                minRange = value;
                rangeBox = new Rectangle((int)X + Width, 0, random.Next(value, maxRange + 1), Height);
            }
        }

        protected int MaxRange
        {
            get { return maxRange; }
            set { rangeBox = new Rectangle((int)X + Width, 0, random.Next(minRange, value + 1), Height); }
        }

        public RangedEnemy(ContentManager Content) : base(Content)
        {
            rangeBox = new Rectangle();
        }

        public override void Update(float deltaTime, InputManager input)
        {
            base.Update(deltaTime, input);


        }
    }
}
