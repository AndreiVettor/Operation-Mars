using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class Steelroach : MeleeEnemy
    {
        public static new float spawnRateMultiplier = .5f;
        public static new int level = 2;

        public Steelroach(ContentManager Content) : base(Content)
        {
            SetTexture(Content, "steelroach");
            Width = Height = 64;
            EnableAnimation = true;

            healthBar.MaxHealth = 50;
            movingSpeed = 6f;
            attackSpeed = 1f;
            attackPower = 15;
        }

        protected override EnemyDirection SpriteDirection
        {
            get
            {
                return EnemyDirection.ToLeft;
            }
        }
    }
}
