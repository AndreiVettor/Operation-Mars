using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame
{
    public abstract class Enemy : Entity
    {
        public enum EnemyDirection { ToLeft, ToRight }

        private EnemyDirection direction;

        public EnemyDirection Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                if (SpriteDirection == value)
                    Effects = Microsoft.Xna.Framework.Graphics.SpriteEffects.None;
                else
                    Effects = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
            }
        }

        protected abstract EnemyDirection SpriteDirection { get; }

        public static float spawnRateMultiplier = 1f;
        public static int level = 1;

        public Enemy (ContentManager Content) : base(Content)
        {
            Direction = SpriteDirection;
            layerDepth = LayerManager.GetLayerDepth(Layer.Enemies);
            EnableHealthBar = true;
            GameManager.AddEntity(this);
        }

        public override void InflictDamage(int hitpoints)
        {
            base.InflictDamage(hitpoints);
            if(healthBar.Health <= 0)
                Debug.enemiesKilled++;
        }
    }
}
