using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoMM.Controls
{
    /// <summary>
    /// Proste label
    /// </summary>
    public class Label : Control
    {
        #region Constructors

        public Label(SpriteFont spriteFont)
        {
            SpriteFont = spriteFont;
            TabStop = false;
            Enabled = true;
            Visible = true;
            Color = Color.White;
        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime) { }

        public override void HandleInput() { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(SpriteFont, Text, Position, Color);
        }

        #endregion
    }
}