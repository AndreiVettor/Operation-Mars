using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame
{
    public static class EnemyFactory
    {
        private static Enemy[] enemies;


        public static void LoadContent(ContentManager Content)
        {
            enemies[0] = new Enemy1(Content);
        }

        public static Enemy CreateEnemy(string enemyType)
        {
            switch(enemyType)
            {
                case "Steve":
                    {
                        return enemies[0];
                        break;
                    }
                default:
                    {
                        Debug.Log("Tried to create an invalid enemy type!");
                        break;
                    }
            }
            return null;
        }
    }
}
