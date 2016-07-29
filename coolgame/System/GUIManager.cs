using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace coolgame.GUI
{
    public class GUIManager
    {
        private List<GUIWindow> windows;
        private List<GUILabel> labels;
        private List<GUISprite> sprites;

        private GUISprite crossHair;
        private GUILabel scoreLabel;

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

        private SpriteFont hudFont;
        public SpriteFont HUDFont
        {
            get { return hudFont; }
            set { hudFont = value; }
        }

        private Vector2 messagePosition;
        private int messageDuration;

        public GUIManager(ContentManager Content)
        {
            textFont = Content.Load<SpriteFont>("textFont");
            messageFont = Content.Load<SpriteFont>("messageFont");
            hudFont = Content.Load<SpriteFont>("hudFont");

            windows = new List<GUIWindow>();
            labels = new List<GUILabel>();
            sprites = new List<GUISprite>();

            crossHair = new GUISprite(Content, "crosshair", Vector2.Zero);
            scoreLabel = new GUILabel(hudFont, GameManager.SpaceCash.ToString(), new Vector2(Game.GAME_WIDTH - 60, 37));
            sprites.Add(new GUISprite(Content, "spaceCash", new Vector2(Game.GAME_WIDTH - 120, 30)));

            messagePosition = new Vector2(Game.GAME_WIDTH/2, 100);
            messageDuration = 5000;
        }

        public bool WindowOpen(GUIWindow testWindow)
        {
            foreach(GUIWindow window in windows)
            {
                if(testWindow.GetType() == window.GetType())
                {
                    return true;
                }
            }
            return false;
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
            scoreLabel.Update(deltaTime);
            scoreLabel.SetText(GameManager.SpaceCash.ToString());

            if (GameManager.State != GameState.Paused)
            {
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

            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].Disabled)
                {
                    windows.Remove(windows[i]);
                    continue;
                }
                windows[i].Update(game, Content, guiManager, spawner);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(GameManager.State != GameState.StartMenu)
            {
                //Draw Labels
                foreach (GUILabel label in labels)
                {
                    label.Draw(spriteBatch);
                }

                scoreLabel.Draw(spriteBatch);

                //Draw Sprites
                foreach (GUISprite sprite in sprites)
                {
                    sprite.Draw(spriteBatch);
                }
            }

            //Draw Windows
            foreach (GUIWindow window in windows)
            {
                window.Draw(spriteBatch);
            }

            //Draw Crosshair
            spriteBatch.Draw(
                crossHair.BackgroundTexture,
                new Vector2(
                    InputManager.MouseX - crossHair.Width/2,
                    InputManager.MouseY - crossHair.Height/2),
                Color.White);
        }
    }
}
