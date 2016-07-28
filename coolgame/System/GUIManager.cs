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
    public class GUIManager
    {
        private List<GUIWindow> windows;
        private List<GUILabel> labels;

        private GUISprite crossHair;
        private GUISprite spaceCash;

        private SpriteFont textFont;
        public SpriteFont TextFont
        {
            get { return textFont; }
            set { textFont = value; }
        }

        private SpriteFont messageFont;
        public SpriteFont MessageFont
        {
            get { return messageFont; }
            set { messageFont = value; }
        }

        private Vector2 messagePosition;
        private int messageDuration;

        public GUIManager(ContentManager Content)
        {
            textFont = Content.Load<SpriteFont>("textFont");
            messageFont = Content.Load<SpriteFont>("messageFont");
            crossHair = new GUISprite(Content, "crosshair", Vector2.Zero);

            windows = new List<GUIWindow>();
            labels = new List<GUILabel>();

            messagePosition = new Vector2(Game.GAME_WIDTH/2, 100);
            messageDuration = 5000;
        }

        public bool WindowOpen(GUIWindow window)
        {
            if (windows.Contains(window))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DisplayMessage(string text)
        {
            labels.Add(new GUILabel(messageFont, text, new Vector2(messagePosition.X - messageFont.MeasureString(text).X / 2, messagePosition.Y), messageDuration));
        }

        public void DisplayMessage(string text, float duration)
        {
            labels.Add(new GUILabel(messageFont, text, new Vector2(messagePosition.X - messageFont.MeasureString(text).X / 2, messagePosition.Y), duration));
        }

        public void AddWindow(GUIWindow window)
        {
            windows.Add(window);
        }

        public void CloseWindow(GUIWindow window)
        {
            windows.Remove(window);
        }

        public void Restart()
        {
            windows.Clear();
        }

        public void Update(Game game, float deltaTime, ContentManager Content, GUIManager guiManager, EnemySpawner spawner)
        {
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].Disabled)
                {
                    windows.Remove(windows[i]);
                    continue;
                }
                windows[i].Update(game, Content, guiManager, spawner);
            }

            for (int i = 0; i < labels.Count; i++)
            {
                if (labels[i].Disabled)
                {
                    labels.Remove(labels[i]);
                    continue;
                }
                labels[i].Update(deltaTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Windows
            foreach(GUIWindow window in windows)
            {
                window.Draw(spriteBatch);
            }

            //Draw Labels
            foreach(GUILabel label in labels)
            {
                label.Draw(spriteBatch);
            }

            //Draw Crosshairs
            spriteBatch.Draw(
                crossHair.BackgroundTexture,
                new Vector2(
                    InputManager.MouseX - crossHair.Width/2,
                    InputManager.MouseY - crossHair.Height/2),
                Color.White);
        }
    }
}
