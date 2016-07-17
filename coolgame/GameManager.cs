using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame
{
    public static class GameManager
    {
        private static List<Entity> entities = new List<Entity>();

        public static List<Entity> GetEntityList()
        {
            return entities;
        }

        public static void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public static void UpdateEntities(float deltaTime, InputManager input)
        {
            foreach(Entity e in entities)
            {
                e.Update(deltaTime, input);
            }
        }

        public static void DrawEntities(SpriteBatch spriteBatch)
        {
            foreach(Entity e in entities)
            {
                e.Draw(spriteBatch);
            }
        }
    }
}
