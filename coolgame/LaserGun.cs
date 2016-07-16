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
    public class LaserGun : Entity
    {
        private List<LaserProjectile> projectiles;
        private ContentManager content;

        public LaserGun(ContentManager content, int x, int y) : base(content)
        {
            SetTexture(content, "laser");
            Width = texture.Width;
            Height = texture.Height;
            X = x;
            Y = y;
            projectiles = new List<LaserProjectile>();
            this.content = content;
        }

        public override void Update(GameTime gameTime, InputManager input)
        {
            Rotation = (float)Math.Atan2(input.MouseY - Y - Height / 2, input.MouseX - X - Width / 2);

            if (input.LeftClick)
            {
                projectiles.Add(new LaserProjectile(content, X + Width / 2, Y + Height / 2, Rotation));
            }
                

            foreach (LaserProjectile p in projectiles)
                p.Update(gameTime, input);

            base.Update(gameTime, input);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (LaserProjectile p in projectiles)
                p.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
