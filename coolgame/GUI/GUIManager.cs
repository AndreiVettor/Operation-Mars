using coolgame.GUI;
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
    class GUIManager
    {
        private List<GUIWindow> windows;
        private GUISprite crossHair;
        private GUISprite spaceCash;

        private SpriteFont textFont;
        public SpriteFont TextFont
        {
            get { return textFont; }
            set { textFont = value; }
        }

        public GUIManager(ContentManager Content)
        {
            textFont = Content.Load<SpriteFont>("textFont");
            crossHair = new GUISprite(Content, "crosshair", Vector2.Zero);
            windows = new List<GUIWindow>();
        }

        public void AddWindow(GUIWindow window)
        {
            windows.Add(window);
        }

        public void CloseWindow(GUIWindow window)
        {
            windows.Remove(window);
        }

        public void Update(Game game, ContentManager Content, GUIManager guiManager)
        {
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].Closing)
                {
                    windows.Remove(windows[i]);
                    continue;
                }
                windows[i].Update(game, Content, guiManager);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(GUIWindow window in windows)
            {
                window.Draw(spriteBatch, textFont);
            }
            spriteBatch.Draw(
                crossHair.BackgroundTexture,
                new Vector2(
                    InputManager.MouseX - crossHair.Width/2,
                    InputManager.MouseY - crossHair.Height/2),
                Color.White);
        }
    }
}
