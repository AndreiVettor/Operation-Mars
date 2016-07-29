using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class MotherShipProjectile : EnemyProjectile
    {
        public MotherShipProjectile(ContentManager content, double x, double y, float direction, int attackPower) : base(content, x, y, direction, attackPower)
        {
            SetTexture("emag_cannon");
            speed /= 6f;
        }
    }
}
