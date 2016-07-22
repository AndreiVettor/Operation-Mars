using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace coolgame
{
    public class Turret : Building
    {
        public Turret(ContentManager content, int groundLevel, Enemy.EnemyDirection enemyDirection) : base(content, groundLevel)
        {
            SetTexture("shitturret");

            if (enemyDirection == Enemy.EnemyDirection.ToLeft)
            {
                X = Game.GAME_WIDTH / 2 - Width / 2 + 260;
                Effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                X = Game.GAME_WIDTH / 2 - Width / 2 - 260;
                Effects = SpriteEffects.None;
            }

            Y = groundLevel - Height;

            healthBar.MaxHealth = 500;
        }
    }
}
