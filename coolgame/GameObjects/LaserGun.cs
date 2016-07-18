﻿using System;
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
        private float cooldownTime;

        public LaserGun(ContentManager content, int x, int y) : base(content)
        {
            SetTexture(content, "laser");
            Width = texture.Width;
            Height = texture.Height;
            X = x;
            Y = y;
            projectiles = new List<LaserProjectile>();
            this.content = content;
            layerDepth = LayerManager.GetLayerDepth(Layer.Buildings);
        }

        public override void Update(float deltaTime, InputManager input)
        {
            Rotation = (float)Math.Atan2(input.MouseY - Y - Height / 2, input.MouseX - X - Width / 2);

            cooldownTime += deltaTime;
            if (input.MouseLeft == ButtonState.Pressed && cooldownTime > 100f)
            {
                SoundManager.PlayClip("laser");
                cooldownTime = 0;
                projectiles.Add(new LaserProjectile(content, X + Width / 2, Y + Height / 2, Rotation));
                CollisionManager.AddProjectile(projectiles[projectiles.Count - 1]);
            }

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update(deltaTime, input);
                if (!projectiles[i].Alive)
                    projectiles.RemoveAt(i);
            }

            base.Update(deltaTime, input);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (LaserProjectile p in projectiles)
                p.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}