using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoMM.Controls
{
    /// <summary>
    /// Proste PictureBox.
    /// </summary>
    public class PictureBox : Control
    {
        #region Fields and Properties

        //Fieldy
        Texture2D image;
        Rectangle sourceRect;
        Rectangle destRect;

        //Property
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }
        public Rectangle SourceRectangle
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }
        public Rectangle DestinationRectangle
        {
            get { return destRect; }
            set { destRect = value; }
        }

        #endregion

        #region Constructors

        public PictureBox(Texture2D image, Rectangle destination)
        {
            Image = image;
            DestinationRectangle = destination;
            SourceRectangle = new Rectangle(0, 0, image.Width, image.Height);
        }
        public PictureBox(Texture2D image, Rectangle destination, Rectangle source)
        {
            Image = image;
            DestinationRectangle = destination;
            SourceRectangle = source;
        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, destRect, sourceRect, Color.White);
        }

        public override void HandleInput() { }

        #endregion
    }
}
