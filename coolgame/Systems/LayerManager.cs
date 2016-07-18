using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame
{
    public enum Layer
    {
        Debugging,
        UI,
        Projectiles,
        EnemiesHealthbar,
        Enemies,
        BuildingsHealthbar,
        Buildings,
        Ground,
        Background
    }

    public static class LayerManager
    {
        public static float GetLayerDepth(Layer layer)
        {
            switch (layer)
            {
                case Layer.Debugging: return 0;
                case Layer.UI: return .1f;
                case Layer.Projectiles: return .2f;
                case Layer.EnemiesHealthbar: return .3f;
                case Layer.Enemies: return .31f;
                case Layer.BuildingsHealthbar: return .4f;
                case Layer.Buildings: return .41f;
                case Layer.Ground: return .5f;
                case Layer.Background: return 1f;
                default: return 0;
            }
        }
    }
}
