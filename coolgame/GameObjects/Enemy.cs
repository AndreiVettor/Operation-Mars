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

        protected EnemyDirection direction;
        protected EnemyDirection spriteFacing;

        public EnemyDirection Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                if (spriteFacing == value)
                    Effects = Microsoft.Xna.Framework.Graphics.SpriteEffects.None;
                else
                    Effects = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
            }
        }

        public Enemy (ContentManager Content) : base(Content)
        {
            Direction = spriteFacing = EnemyDirection.ToLeft;
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
