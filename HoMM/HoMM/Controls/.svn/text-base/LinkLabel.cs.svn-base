using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HoMM.GameComponents;

namespace HoMM.Controls
{
    /// <summary>
    /// Obdoba buttonu, akorat je jenom jako text
    /// </summary>
    public class LinkLabel : Control
    {
        #region Fields and Properties

        //Field
        Color selectedColor = Color.Red;

        //Properta
        public Color SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; }
        }

        #endregion

        #region Constructors

        public LinkLabel(SpriteFont spriteFont)
        {
            SpriteFont = spriteFont;
            Enabled = true;
            Visible = true;
            TabStop = true;
            HasFocus = false;
            Color = Color.White;
        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime) { }

        /// <summary>
        /// Vykresleni, zase podle focusu cerveny nebo normalni
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (hasFocus)
                spriteBatch.DrawString(SpriteFont, Text, Position, selectedColor);
            else
                spriteBatch.DrawString(SpriteFont, Text, Position, Color);
        }

        /// <summary>
        /// Kontrola odkliknuti buttonu
        /// </summary>
        public override void HandleInput()
        {
            if (!HasFocus)
                return;

            //Kontrola klavesnice
            if (KeyboardInput.KeyReleased(Keys.Enter))
                base.OnSelected(null);

            //Kontrola mysi
            if (MouseInput.MouseOver(Position.X, Position.Y, Size.X, Size.Y) && MouseInput.LeftButtonPressed())
                base.OnSelected(null);
        }

        #endregion

    }
}