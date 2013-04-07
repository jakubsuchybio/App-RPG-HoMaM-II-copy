using Microsoft.Xna.Framework;
using HoMM.GameComponents;
using HoMM.Libraries;

namespace HoMM.TileEngine
{
    public class Engine
    {
        #region Fields and Properties

        //Fieldy
        int tileWidth;
        int tileHeight;

        //Property
        public int TileWidth
        {
            get { return tileWidth; }
        }
        public int TileHeight
        {
            get { return tileHeight; }
        }
        
        #endregion

        #region Constructors

        public Engine(int tileWidth, int tileHeight)
        {
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight; 
        }

        #endregion

        #region Methods

        /// <summary>
        /// Upraveni policka, pokud by se vychylilo z mapy
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Tile NormalizeTile(Tile p)
        {
            Point tmp = NormalizePoint(new Point(p.X, p.Y));
            return new Tile(-1,0,tmp.X,tmp.Y);
        }
        public Point NormalizePoint(Point p)
        {
            Point tmp = p;
            if (tmp.X < 0)
                tmp.X = 0;
            if (tmp.Y < 0)
                tmp.Y = 0;
            return tmp;
        }

        /// <summary>
        /// Prevede proste aktualni stav mysi na Point
        /// </summary>
        /// <returns></returns>
        public Point MouseToPoint()
        {
            Vector2 position = new Vector2(MouseInput.MouseState.X,MouseInput.MouseState.Y);
            position += new Vector2(-24, -24);
            Point tmp = new Point((int)(position.X + Session.Camera.Position.X) / tileWidth, (int)(position.Y + Session.Camera.Position.Y) / tileHeight);
            return NormalizePoint(tmp);
        }

        /// <summary>
        /// Prevede Vektor2 na Point nebo na Tile
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Point VectorToCell(Vector2 position)
        {
            Point tmp = new Point((int)(position.X + Session.Camera.Position.X) / tileWidth, (int)(position.Y + Session.Camera.Position.Y) / tileHeight);
            return NormalizePoint(tmp);
        }
        public Tile VectorToTile(Vector2 position)
        {
            Tile tmp = new Tile(-1, 0, (int)(position.X + Session.Camera.Position.X) / tileWidth, (int)(position.Y + Session.Camera.Position.Y) / tileHeight);
            return NormalizeTile(tmp);
        }

        /// <summary>
        /// Prevede Point, Mriz.Vrchol, Tile nebo Vector2 na Vector2
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public Vector2 CellToVector(Point cell)
        {
            return CellToVector(new Vector2(cell.X, cell.Y));
        }
        public Vector2 CellToVector(Mriz.Vrchol cell)
        {
            return CellToVector(new Vector2(cell.X, cell.Y));
        }
        public Vector2 CellToVector(Tile cell)
        {
            return CellToVector(new Vector2(cell.X,cell.Y));
        }
        public Vector2 CellToVector(Vector2 cell)
        {
            return new Vector2(cell.X * tileWidth - Session.Camera.Position.X, cell.Y * tileHeight - Session.Camera.Position.Y);
        }

        /// <summary>
        /// Vrati policko uplne vlevo nahore ( slouzi pro vykresleni jenom toho co je na kamere, vsechno ostatni se nevykresli )
        /// </summary>
        /// <returns></returns>
        public Point TopLeftVisibleTile()
        {
            Point tmp = new Point(((int)Session.Camera.Position.X) / tileWidth, ((int)Session.Camera.Position.Y) / tileHeight);
            if(tmp.X < 0)
                tmp.X = 0;
            if (tmp.Y < 0)
                tmp.Y = 0;
            return tmp;
        }

        /// <summary>
        /// Vrati policko uplne vpravo dole ( slouzi pro vykresleni jenom toho co je na kamere, vsechno ostatni se nevykresli )
        /// </summary>
        /// <returns></returns>
        public Point DownRightVisibleTile(int Xmax, int Ymax)
        {
            Point tmp = new Point(1 + ((int)Session.Camera.Position.X + Session.ViewportRect.Width + Session.ViewportRect.X + tileWidth) / tileWidth, 1 + ((int)Session.Camera.Position.Y + Session.ViewportRect.Height + Session.ViewportRect.Y) / tileHeight);
            if (tmp.X >= Xmax)
                tmp.X = Xmax;
            if (tmp.Y >= Ymax)
                tmp.Y = Ymax;
            return tmp;
        }

        #endregion
    }
}