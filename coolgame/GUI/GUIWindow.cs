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

        protected int borderPadding = 15;

        public bool ButtonPressed(int id)
        {
            return buttons[id].Pressed;
        }

        public bool Closing;

        public GUIWindow (ContentManager Content) : base (Content) 
        {
            Closing = false;
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

        public void Center()
        {
            Position = new Vector2(
                Game.GAME_WIDTH / 2 - Width / 2,
                Game.GAME_HEIGHT / 2 - Height / 2);

        }

        public virtual void Update(Game game, ContentManager Content, GUIManager guiManager)
        {
            foreach(GUIButton button in buttons)
            {
                button.Update();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont textFont)
        {
            base.Draw(spriteBatch, textFont);

            foreach(GUIButton button in buttons)
            {
                button.Draw(spriteBatch, textFont);
            }
            foreach (GUILabel label in labels)
            {
                label.Draw(spriteBatch, textFont);
            }
        }
    }
}
