using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HoMM.GameComponents;

namespace HoMM.Controls
{
    /// <summary>
    /// LeftRightSelector je vlastne menu, ktere ukazuje aktualni polozku a posouva se v nem doleva a doprava.
    /// </summary>
    public class LeftRightSelector : Control
    {
        #region Fields and Properties

        //Fieldy
        List<string> items = new List<string>();
        Texture2D leftSelection;
        Texture2D rightSelection;
        Color hiliteColor = Color.Red;
        int maxItemWidth;
        int selectedItem;

        //Property
        public int SelectedIndex
        {
            get { return selectedItem; }
            set { selectedItem = (int)MathHelper.Clamp(value, 0f, items.Count); }
        }
        public string SelectedItem
        {
            get { return Items[selectedItem]; }
        }
        public List<string> Items
        {
            get { return items; }
        }

        #endregion

        #region Constructors

        public LeftRightSelector(SpriteFont spriteFont, Texture2D leftArrow, Texture2D rightArrow)
        {
            SpriteFont = spriteFont;
            this.leftSelection = leftArrow;
            this.rightSelection = rightArrow;
            Enabled = true;
            Visible = true;
            TabStop = true;
            Color = Color.White;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Nastaveni polozek menu
        /// </summary>
        /// <param name="items">pole stringu polozek</param>
        /// <param name="maxWidth">pocet polozek</param>
        public void SetItems(string[] items, int maxWidth)
        {
            this.items.Clear();
            foreach (string s in items)
                this.items.Add(s);
            maxItemWidth = maxWidth;
            Size = new Vector2(leftSelection.Width + maxItemWidth + rightSelection.Width, spriteFont.MeasureString(items[selectedItem]).Y);
        }

        public override void Update(GameTime gameTime) { }

        /// <summary>
        /// Ovladani vstupu z klavesnice a mysi doleva a doprava
        /// </summary>
        public override void HandleInput()
        {
            if (items.Count == 0)
                return;

            //Ovladani klavesnice
            if (KeyboardInput.KeyReleased(Keys.Left))
            {
                selectedItem--;
                if (selectedItem < 0)
                    selectedItem = 0;
            }
            if (KeyboardInput.KeyReleased(Keys.Right))
            {
                selectedItem++;
                if (selectedItem >= items.Count)
                    selectedItem = items.Count - 1;
            }

            //Ovladani mysi
            float itemHeight = position.Y + (spriteFont.MeasureString(items[selectedItem]).Y - leftSelection.Height) / 2;
            if (MouseInput.MouseOver(Position.X, itemHeight, leftSelection.Width, leftSelection.Height) && MouseInput.LeftButtonPressed())
            {
                selectedItem--;
                if (selectedItem < 0)
                    selectedItem = 0;
            }
            if (MouseInput.MouseOver(Position.X + leftSelection.Width + maxItemWidth, itemHeight, rightSelection.Width, rightSelection.Height) && MouseInput.LeftButtonPressed())
            {
                selectedItem++;
                if (selectedItem >= items.Count)
                    selectedItem = items.Count - 1;
            }
        }

        /// <summary>
        /// Vykresleni tohoto typu menu.
        /// Pokud ma focus, vykresli se cervene
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 drawTo = position;
            float itemHeight = spriteFont.MeasureString(items[selectedItem]).Y;
            drawTo.Y = position.Y + (itemHeight - leftSelection.Height) / 2;
            spriteBatch.Draw(leftSelection, drawTo, Color.White);

            drawTo = position;
            drawTo.X += leftSelection.Width;
            float itemWidth = spriteFont.MeasureString(items[selectedItem]).X;
            float offset = (maxItemWidth - itemWidth) / 2;
            drawTo.X += offset;
            if (hasFocus)
                spriteBatch.DrawString(spriteFont, items[selectedItem], drawTo, hiliteColor);
            else
                spriteBatch.DrawString(spriteFont, items[selectedItem], drawTo, Color);

            drawTo.X += -1 * offset + maxItemWidth;
            drawTo.Y = position.Y + (itemHeight - leftSelection.Height) / 2;
            spriteBatch.Draw(rightSelection, drawTo, Color.White);
        }

        #endregion

    }
}