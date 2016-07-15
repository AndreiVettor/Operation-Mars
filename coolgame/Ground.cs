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
        Texture2D texture;
        Vector2 position;

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
        }

        public Ground(ContentManager content, int height)
        {
            texture = content.Load<Texture2D>("ground");
            position = new Vector2(0, Game.GAME_HEIGHT - height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
