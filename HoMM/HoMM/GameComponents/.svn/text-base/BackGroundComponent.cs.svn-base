using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoMM.GameComponents
{
    /// <summary>
    /// Enum pro urceni jestli se ma pozadi vykreslit na celej obraz a nebo jen jeho skutecna velikost
    /// </summary>
    public enum DrawMode { Center, Fill }

    public class BackgroundComponent
    {
        #region Fields and Properties

        //Fieldy
        Rectangle screenRectangle;
        Rectangle destination;
        Texture2D image;
        DrawMode drawMode;

        //Properta
        public bool Visible
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public BackgroundComponent(Game game, Texture2D image, DrawMode drawMode)
        {
            Visible = true;
            this.image = image;
            this.drawMode = drawMode;
            screenRectangle = new Rectangle(
            0,
            0,
            game.Window.ClientBounds.Width,
            game.Window.ClientBounds.Height);

            //Nastaveni Rectanglu pro vykresleni bud na cely obraz nebo jen podle velikosti textury
            switch (drawMode)
            {
                case DrawMode.Center:
                    destination = new Rectangle(
                    (screenRectangle.Width - image.Width) / 2,
                    (screenRectangle.Height - image.Height) / 2,
                    image.Width,
                    image.Height);
                    break;
                case DrawMode.Fill:
                    destination = new Rectangle(
                    screenRectangle.X,
                    screenRectangle.Y,
                    screenRectangle.Width,
                    screenRectangle.Height);
                    break;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Vykresleni pokud je viditelny
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
                spriteBatch.Draw(image, destination, Color.White);
        }

        #endregion
    }
}