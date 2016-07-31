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
        protected float attackCooldown;
        protected float movingSpeed;
        protected float attackSpeed;
        protected int attackPower;
        protected string attackSound;
        protected string hitSound;
        protected Building target;
        protected int spaceCash;

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
            movingSpeed = 1f;
            attackSpeed = 1f;
            spaceCash = 10;
        }

        public override void InflictDamage(int hitpoints)
        {
            if (GameManager.godMode == 3)
                base.InflictDamage(int.MaxValue);
            else
                base.InflictDamage(hitpoints);

            if (healthBar.Health <= 0)
            {
                GameManager.SpaceCash += spaceCash;
            }
        }
    }
}
