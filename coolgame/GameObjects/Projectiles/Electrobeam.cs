using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace coolgame
{
    public class Electrobeam
    {
        private ContentManager content;
        private Murderbot shooter;
        private List<ElectrobeamComponent> components;
        private float componentSpawnTime;
        private int x;
        private int y;
        private int targetx;

        public Electrobeam(ContentManager content, Murderbot shooter, int x, int y, int targetx)
        {
            this.x = x;
            this.y = y;
            this.targetx = targetx;
            this.shooter = shooter;
            this.content = content;

            components = new List<ElectrobeamComponent>();

            if (x < targetx)
            {
                for (int i = x; i < targetx; i += 10)
                {
                    components.Add(new ElectrobeamComponent(content, i, y, targetx));
                }
            }
            else
            {
                for (int i = x; i > targetx; i -= 10)
                {
                    components.Add(new ElectrobeamComponent(content, i, y, targetx));
                }
            }
            

        }

        public void Update(float deltaTime)
        {
            componentSpawnTime += deltaTime;

            if (componentSpawnTime > 100)
            {
                components.Add(new ElectrobeamComponent(content, x, y, targetx));
                componentSpawnTime = 0;
            }
                

            foreach (ElectrobeamComponent c in components)
                c.Update(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ElectrobeamComponent c in components)
                c.Draw(spriteBatch);
        }
    }
}
