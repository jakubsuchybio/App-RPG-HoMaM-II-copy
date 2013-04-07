using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HoMM.GameComponents;

namespace HoMM.Controls
{
    /// <summary>
    /// Manazer pro pobyb mezi vsema ovladacima prvkama
    /// </summary>
    public class ControlManager : List<Control>
    {
        #region Fields and Properties

        //Fieldy
        public int selectedControl = 0;
        public bool editor;

        #endregion

        #region Constructors

        public ControlManager() : base() { editor = false; }
        public ControlManager(int capacity) : base(capacity) { editor = false; }
        public ControlManager(IEnumerable<Control> collection) : base(collection) { editor = false; }

        #endregion

        #region Methods

        /// <summary>
        /// Ovladani vstupu z klavesnice a mysi, a update controluu
        /// Pokud je zaply editor, pak menu funguje jako radiobutton, ktery se signalizuje fieldem clickedIndex
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (Count == 0)
                return;
            if (editor)
                foreach (Control d in this)
                    d.HasFocus = false;
            foreach (Control c in this)
            {
                if (MouseInput.MouseOver(c.Position.X, c.Position.Y, c.Size.X, c.Size.Y))
                {
                    selectedControl = this.IndexOf(c);
                    if (this[selectedControl].TabStop && this[selectedControl].Enabled)
                    {
                        foreach (Control d in this)
                            d.HasFocus = false;
                        this[selectedControl].HasFocus = true;
                    }
                } 
                if (c.Enabled)
                    c.Update(gameTime);
                if (c.HasFocus)
                    c.HandleInput();
            }
            if (KeyboardInput.KeyPressed(Keys.Up))
                PreviousControl();
            if (KeyboardInput.KeyPressed(Keys.Down))
                NextControl();

        }

        /// <summary>
        /// Vykresleni vsech viditelnych casti
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Control c in this)
            {
                if (c.Visible)
                    c.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Metoda slouzici pro presun na dalsi ovladaci prvek a s tim spojene operace
        /// </summary>
        public void NextControl()
        {
            if (Count == 0)
                return;
            int currentControl = selectedControl;
            this[selectedControl].HasFocus = false;
            do
            {
                selectedControl++;
                if (selectedControl == Count)
                    selectedControl = 0;
                if (this[selectedControl].TabStop && this[selectedControl].Enabled)
                    break;
            } while (currentControl != selectedControl);
            this[selectedControl].HasFocus = true;
        }

        /// <summary>
        /// Metoda slouzici pro presun na predchozi ovladaci prvek a s tim spojene operace
        /// </summary>
        public void PreviousControl()
        {
            if (Count == 0)
                return;
            int currentControl = selectedControl;
            this[selectedControl].HasFocus = false;
            do
            {
                selectedControl--;
                if (selectedControl < 0)
                    selectedControl = Count - 1;
                if (this[selectedControl].TabStop && this[selectedControl].Enabled)
                    break;
            } while (currentControl != selectedControl);
            this[selectedControl].HasFocus = true;
        }

        #endregion
    }
}