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
        private static ContentManager content;

        public static void LoadContent(ContentManager Content)
        {
            content = Content;
        }

        public static Enemy CreateEnemy(string enemyType)
        {
            switch(enemyType.ToLower())
            {
                case "steve":
                    {
                        return new Steve(content);
                    }
                case "crawler":
                    {
                        return new Crawler(content);
                    }
                case "steelroach":
                    {
                        return new Steelroach(content);
                    }
                case "reptilian":
                    {
                        return new Reptilian(content);
                    }
                case "mwat":
                    {
                        return new Mwat(content);
                    }
                case "reptiliansaucer":
                    {
                        return new ReptilianSaucer(content);
                    }
                case "demolitionroverunit":
                    {
                        return new DemolitionRoverUnit(content);
                    }
                case "murderbot":
                    {
                        return new Murderbot(content);
                    }
                case "emag":
                    {
                        return new MotherShip(content);
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
