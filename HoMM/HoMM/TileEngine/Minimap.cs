using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HoMM.GameComponents;
using HoMM.Libraries;

namespace HoMM.TileEngine
{
    public class Minimap
    {
        #region Fields and Properties

        //Fieldy
        Tileset set;
        bool isHolding;
        static Rectangle rect = new Rectangle(764, 23, 236, 235);
        Rectangle currentRect;
        Vector2 mouseVector;
        Texture2D minimapImage;

        public static Rectangle Rect
        {
            get { return rect; }
        }

        #endregion

        #region ConstructorS

        public Minimap(Tileset tileset, Texture2D minimapImage)
        {
            set = tileset;
            isHolding = false;
            mouseVector = new Vector2();
            this.minimapImage = minimapImage;
        }

        #endregion

        #region Methods

        public void Update()
        {
            //Nastaveni toho maleho bileho ctverecku v minimape, ktery odpovida velikosti kamery v cely mape
            currentRect = new Rectangle(
                rect.X + (int)(Session.Camera.Position.X * rect.Width) / Session.FrontMap.WidthInPixels,
                rect.Y + (int)(Session.Camera.Position.Y * rect.Height) / Session.FrontMap.HeightInPixels,
                (719 * rect.Width) / Session.FrontMap.WidthInPixels,
                (720 * rect.Height) / Session.FrontMap.HeightInPixels);

            //Posun maleho bileho ramecku po minimape ( vlastne posun kamery po mape )
            if (Session.state != GameState.Moving)
                if (!isHolding && MouseInput.MouseOver(rect.X, rect.Y, rect.Width, rect.Height) && MouseInput.LeftButtonPressed())
                    isHolding = true;
                else if (isHolding && !MouseInput.LeftButtonReleased())
                {
                    mouseVector.X = (Session.MapProps.FrontSizeX * (MouseInput.MouseState.X - rect.X) * Session.FrontMap.TileWidth) / rect.Width - Session.ViewportRect.Width / 2;
                    mouseVector.Y = (Session.MapProps.FrontSizeY * (MouseInput.MouseState.Y - rect.Y) * Session.FrontMap.TileHeight) / rect.Height - Session.ViewportRect.Height / 2;

                    Session.Camera.Position = mouseVector;
                    Session.Camera.LockCamera();
                }
                else
                    isHolding = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Vykresleni textury minimapy
            spriteBatch.Draw(minimapImage, rect, Color.White);
            
            //Vykresleni vsech policek v minimape ( ale ne texturovany, jenom barevny )
            for (int x = 0; x < Session.MapProps.FrontSizeX; x++)
            {
                for (int y = 0; y < Session.MapProps.FrontSizeY; y++)
                {
                    Tile tile = Session.FrontMap.mapLayers[0].GetTile(x, y);
                    if (tile.Tileset == 1) 
                    {
                        Rectangle dest = Library.Scale(new Rectangle(
                            x * Session.MapProps.FrontEngineX,
                            y * Session.MapProps.FrontEngineY,
                            Session.MapProps.FrontEngineX,
                            Session.MapProps.FrontEngineY));
                        dest.X += rect.X;
                        dest.Y += rect.Y;

                        switch (tile.TileIndex)
                        {
                            case 1:
                                {
                                    spriteBatch.Draw(set.Texture, dest, set.SourceRectangles[1], Color.Blue);
                                    break;
                                }
                            case 2:
                                {
                                    spriteBatch.Draw(set.Texture, dest, set.SourceRectangles[1], Color.DarkGreen);
                                    break;
                                }
                            default:
                                {
                                    spriteBatch.Draw(set.Texture, dest, set.SourceRectangles[1], Color.White);
                                    break;
                                }
                        }
                    }
                }
            }

            //Kresleni ctverecku okna
             spriteBatch.Draw(
                        set.Texture,
                        currentRect,
                        set.SourceRectangles[0],
                        Color.White);

        }

        #endregion
    }
}
