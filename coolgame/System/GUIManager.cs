using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace coolgame.GUI
{
    public class GUIManager
    {
        private List<GUIWindow> windows;
        private List<GUILabel> labels;
        private List<GUISprite> sprites;
        private GUITooltip toolTip;
        public GUITooltip ToolTip
        {
            get { return toolTip; }
        }

        private GUISprite crossHair;
        private GUILabel scoreLabel;

        private SpriteFont mediumFont;
        public SpriteFont MediumFont
        {
            get { return mediumFont; }
            set { mediumFont = value; }
        }

        private SpriteFont hugeFont;
        public SpriteFont HugeFont
        {
            get { return hugeFont; }
            set { hugeFont = value; }
        }

        private SpriteFont smallFont;
        public SpriteFont SmallFont
        {
            get { return smallFont; }
            set { smallFont = value; }
        }

        private SpriteFont bigFont;
        public SpriteFont BigFont
        {
            get { return bigFont; }
            set { bigFont = value; }
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
            mediumFont = Content.Load<SpriteFont>("textFont");
            hugeFont = Content.Load<SpriteFont>("messageFont");
            hudFont = Content.Load<SpriteFont>("hudFont");
            smallFont = Content.Load<SpriteFont>("upgradeFont");
            bigFont = Content.Load<SpriteFont>("bigFont");

            toolTip = new GUITooltip(Content, mediumFont);

            windows = new List<GUIWindow>();
            labels = new List<GUILabel>();
            sprites = new List<GUISprite>();

            crossHair = new GUISprite(Content, "crosshair", Vector2.Zero);
            scoreLabel = new GUILabel(hudFont, GameManager.SpaceCash.ToString(), new Vector2(Game.GAME_WIDTH - 80, 37));
            sprites.Add(new GUISprite(Content, "spaceCash", new Vector2(Game.GAME_WIDTH - 140, 30)));

            messagePosition = new Vector2(Game.GAME_WIDTH/2, 100);
            messageDuration = 5000;
        }

        public bool WindowOpen(Type windowType)
        {
            GUIWindow temp = windows.Find(x => x.GetType() == windowType);
            if (temp != null)
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
            labels.Add(new GUILabel(hugeFont, text, new Vector2(messagePosition.X - hugeFont.MeasureString(text).X / 2, messagePosition.Y), messageDuration));
        }

        public void DisplayMessage(string text, float duration)
        {
            labels.Add(new GUILabel(hugeFont, text, new Vector2(messagePosition.X - hugeFont.MeasureString(text).X / 2, messagePosition.Y), duration));
        }

        public void AddWindow(GUIWindow window)
        {
            windows.Add(window);
        }

        public void CloseWindow(Type windowType)
        {
            GUIWindow temp = windows.Find(x => x.GetType() == windowType);
            if(temp != null)
            {
                temp.Disabled = true;
            }
        }

        public void Restart()
        {
            windows.Clear();
        }

        public void Update(Game game, float deltaTime, ContentManager Content, GUIManager guiManager, EnemySpawner spawner)
        {
            scoreLabel.Update(deltaTime);
            scoreLabel.SetText(GameManager.SpaceCash.ToString());

            toolTip.Update();

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

            toolTip.Draw(spriteBatch);

            //Draw Crosshair
            if (!toolTip.Visible)
            {
                spriteBatch.Draw(
                    crossHair.BackgroundTexture,
                        new Vector2(
                            InputManager.MouseX - crossHair.Width / 2,
                            InputManager.MouseY - crossHair.Height / 2),
                        Color.White);
            }
        }
    }
}
