using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame.UI
{
    public class GUILabel : GUIElement
    {
        private float lifeTime;
        private float timer;
        private float fadeTime = 1000;

        public GUILabel(SpriteFont textFont, string text, Vector2 position) : base()
        {
            Initialize(textFont,text, position);
        }

        public GUILabel(SpriteFont textFont, string text, Vector2 position, float lifeTime) : base()
        {
            Initialize(textFont, text, position);
            Alpha = 0;
            this.lifeTime = lifeTime;
            if (fadeTime > lifeTime / 2)
            {
                fadeTime = lifeTime / 2;
            }
        }

        public void Initialize(SpriteFont textFont, string text, Vector2 position)
        {
            font = textFont;
            SetText(text);
            Position = position;
            BackgroundAlpha = 0;
        }

        public void Update(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= lifeTime)
            {
                Disabled = true;
            }
            else if(timer <= fadeTime)
            {
                Alpha += deltaTime / (fadeTime / 255);
            }

            else if (timer >= lifeTime - fadeTime)
            {
                Alpha -= deltaTime / (fadeTime / 255);
            }
            if(Alpha < 0)
            {
                Alpha = 0;
            }
        }
    }
}
