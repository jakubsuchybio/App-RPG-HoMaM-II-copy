using System;
using System.Xml.Serialization;
using HoMM.Libraries;

namespace HoMM.TileEngine
{
    [Serializable]
    public class Tile : Mriz.Vrchol
    {
        #region Fields and Properties

        //Fieldy
        int tileIndex;
        int tileset;

        [XmlIgnore]
        public object obj { get; set; }

        //Property
        public int TileIndex
        {
            get { return tileIndex; }
            set
            {
                if (value < 0)
                    tileIndex = 0;
                else
                    tileIndex = value;
            }
        }
        public int Tileset
        {
            get { return tileset; }
            set
            {
                if (value < 0)
                    tileset = 0;
                else
                    tileset = value;
            }
        }

        #endregion

        #region Constructors

        public Tile() { }

        public Tile(int x, int y)
            : base(x, y)
        {
            TileIndex = 2;
            Tileset = 2;
        }
        public Tile(int tileIndex, int tileSet, int x, int y)
            : base(x, y)
        {
            TileIndex = tileIndex;
            Tileset = tileSet;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Override vypsani tilu jako text
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return TileIndex + " " + Tileset + " " + X + " " + Y;
        }

        #endregion
    }
}