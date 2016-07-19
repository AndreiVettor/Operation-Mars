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
        private Rectangle rectangle;
        private float layerDepth = LayerManager.GetLayerDepth(Layer.Ground);

        public int Top
        {
            get
            {
                return rectangle.Y;
            }
        }

        public Ground(ContentManager content)
        {
            texture = content.Load<Texture2D>("ground");
            rectangle = new Rectangle(0, Game.GAME_HEIGHT - texture.Height, Game.GAME_WIDTH, texture.Height);
        }

        public Ground(ContentManager content, int height)
        {
            texture = content.Load<Texture2D>("ground");
            rectangle = new Rectangle(0, Game.GAME_HEIGHT - height, Game.GAME_WIDTH, height);
        }

        public bool Collides(Entity e)
        {
            return e.Collides(rectangle);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                rectangle,
                null,
                Color.White,
                0,
                Vector2.Zero,
                SpriteEffects.None,
                layerDepth);
        }
    }
}
