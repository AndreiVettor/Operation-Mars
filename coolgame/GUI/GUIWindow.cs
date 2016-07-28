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
    public class GUIWindow : GUIElement
    {
        protected List<GUIButton> buttons;
        protected List<GUILabel> labels;

        private Color secondaryColor;
        public Color SecondaryColor
        {
            get { return secondaryColor; }
            set
            {
                secondaryColor = value;
                foreach (GUIButton button in buttons)
                {
                    button.BackgroundColor = value;
                }
            }
        }


        protected int borderPadding = 15;

        public bool ButtonPressed(int id)
        {
            if(id > buttons.Count - 1)
            {
                Debug.Log("Tried to access invalid button " + id);
                return false;
            }
            return buttons[id].Pressed;
        }

        public GUIWindow (ContentManager Content) : base (Content) 
        {
            Initialize();
        }

        public GUIWindow(ContentManager Content, string textureName) : base(Content, textureName)
        {
            Initialize();
        }

        private new void Initialize()
        {
            buttons = new List<GUIButton>();
            labels = new List<GUILabel>();
        }

        public void AddButton(GUIButton button)
        {
            //Add padding value to button position
            button.Position = new Vector2(Position.X + button.Position.X + borderPadding, Position.Y + button.Position.Y + borderPadding);

            //If button is in the menu
            if (button.Rectangle.Intersects(this.Rectangle))
            {
                buttons.Add(button);
            }
            else
            {
                buttons.Add(button);
                Debug.Log("Button out of bounds"
                    + " X: " + button.Position.X
                    + " Y: " + button.Position.Y
                    + " W: " + button.Width
                    + " H: " + button.Height);
            }
        }

        public void AddLabel(GUILabel label)
        {
            //Add padding value to label position
            label.Position = new Vector2(Position.X + label.Position.X + borderPadding, Position.Y + label.Position.Y + borderPadding);

            //If label is in the menu
            if (label.Rectangle.Intersects(this.Rectangle))
            {
                labels.Add(label);
            }
            else
            {
                labels.Add(label);
                Debug.Log("Button out of bounds"
                    + " X: " + label.Position.X
                    + " Y: " + label.Position.Y
                    + " W: " + label.Width
                    + " H: " + label.Height);
            }
        }

        protected void Center()
        {
            Position = new Vector2(
                Game.GAME_WIDTH / 2 - Width / 2,
                Game.GAME_HEIGHT / 2 - Height / 2);

        }

        protected void NormalizeButtonLength(bool centerButtons, bool resizeMenu, int spacing)
        {
            int maxWidth = 0;

            //Find Max Width
            foreach (GUIButton button in buttons)
            {
                maxWidth = Math.Max(maxWidth, button.Width);
            }

            //Resize Menu
            if (resizeMenu)
            {
                Width = maxWidth + borderPadding * 2;
                Height = (buttons[0].Height + spacing) * buttons.Count + borderPadding * 2;
            }

            //Apply Max Width to All Buttons
            foreach (GUIButton button in buttons)
            {
                button.Width = maxWidth;
            }

            if (centerButtons)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].Y = Y + borderPadding + i * (spacing + buttons[i].Height);
                    buttons[i].X = X + Width / 2 - buttons[i].Width / 2;
                }
            }
        }

        public virtual void Update(Game game, ContentManager Content, GUIManager guiManager, EnemySpawner spawner)
        {
            foreach(GUIButton button in buttons)
            {
                button.Update();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach(GUIButton button in buttons)
            {
                button.Draw(spriteBatch);
            }
            foreach (GUILabel label in labels)
            {
                label.Draw(spriteBatch);
            }
        }
    }
}
