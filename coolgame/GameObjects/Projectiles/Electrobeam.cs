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
        private List<ElectrobeamComponent> components;
        private float componentSpawnTime;
        private int x;
        private int y;
        private int targetx;
        private bool forceField;
        private float soundLoopTime;

        public Electrobeam(ContentManager content, int x, int y, int targetx, bool forceField)
        {
            this.x = x;
            this.y = y;
            this.targetx = targetx;
            this.content = content;
            this.forceField = forceField;

            components = new List<ElectrobeamComponent>();

            if (x < targetx)
            {
                for (int i = x; i < targetx; i += 10)
                {
                    components.Add(new ElectrobeamComponent(content, i, y, targetx, forceField));
                }
            }
            else
            {
                for (int i = x; i > targetx; i -= 10)
                {
                    components.Add(new ElectrobeamComponent(content, i, y, targetx, forceField));
                }
            }
            

        }

        public void Update(float deltaTime)
        {
            componentSpawnTime += deltaTime;
            soundLoopTime += deltaTime;

            if (componentSpawnTime > 100)
            {
                components.Add(new ElectrobeamComponent(content, x, y, targetx, forceField));
                componentSpawnTime = 0;
            }
            if (soundLoopTime > 200)
            {
                SoundManager.PlayClip("electrobeam");
                soundLoopTime = 0;
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
