using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class Ground
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle collisionBox;
        private float layerDepth = LayerManager.GetLayerDepth(Layer.Ground);

        public int Top
        {
            get
            {
                return (int)position.Y;
            }
        }

        public Ground(ContentManager content)
        {
            texture = content.Load<Texture2D>("ground");
            position = new Vector2(0, Game.GAME_HEIGHT - texture.Height);
            collisionBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public Ground(ContentManager content, int height)
        {
            texture = content.Load<Texture2D>("ground");
            position = new Vector2(0, Game.GAME_HEIGHT - height);
            collisionBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, height);
        }

        public bool Collides(Entity e)
        {
            return e.Collides(collisionBox);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
        }
    }
}
