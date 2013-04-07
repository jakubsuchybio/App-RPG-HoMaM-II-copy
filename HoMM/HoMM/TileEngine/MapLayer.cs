using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using HoMM.Libraries;

namespace HoMM.TileEngine
{
    [Serializable]
    public class MapLayer
    {
        #region Fields and Properties

        //Field
        [XmlIgnore]
        public Tile[,] map;

        //Property
        /*Jelikoz XML Serializace ani zadna jina serializace (co jsem hledal) 
        neumi ulozit dvourozmerne pole, 
        tak timto prevadim mapu na jednorozmerne pole a pak se to ulozi.*/
        [XmlElement("map")]
        public Tile[] XmlData
        {
            get 
            {
                Tile[] xmlTmp = new Tile[Width * Height];
                int k = 0;

                for (int y = 0; y < map.GetLength(1); y++)
                {
                    for (int x = 0; x < map.GetLength(0); x++)
                    {
                        xmlTmp[k] = map[x, y];
                        k++;
                    }
                }
                return xmlTmp;
            }
            set 
            {
                Tile[] xmlTmp = value;
                map = new Tile[(int)Math.Sqrt(xmlTmp.Length), (int)Math.Sqrt(xmlTmp.Length)];
                int k = 0;

                for (int y = 0; y < map.GetLength(1); y++)
                {
                    for (int x = 0; x < map.GetLength(0); x++)
                    {
                        map[x, y] = xmlTmp[k];
                        k++;
                    }
                }
            }
        }

        public int Width
        {
            get { return map.GetLength(1); }
        }
        public int Height
        {
            get { return map.GetLength(0); }
        }

        #endregion

        #region Constructors

        public MapLayer() { }
        public MapLayer(Tile[,] map)
        {
            this.map = (Tile[,])map.Clone();
        }
        public MapLayer(int width, int height)
        {
            map = new Tile[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[y, x] = new Tile(0, 0, x, y);
                }
            }
        }

        #endregion

        #region Methods Region

        /// <summary>
        /// Vrati Tile z mapy ze souradnic, z Pointu nebo Mriz.Vrcholu
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Tile GetTile(Mriz.Vrchol p)
        {
            return GetTile(p.X, p.Y);
        }
        public Tile GetTile(Point p)
        {
            return GetTile(p.X, p.Y);
        }
        public Tile GetTile(int x, int y)
        {
            return map[x, y];
        }

        /// <summary>
        /// Prenastavi Tile v mape
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tile"></param>
        public void SetTile(int x, int y, Tile tile)
        {
            map[x, y] = tile;
        }
        public void SetTile(int x, int y, int tileIndex, int tileset, bool walkable)
        {
            map[x, y] = new Tile(tileIndex, tileset, x, y);
            map[x, y].walkable = walkable;
        }

        #endregion
    }
}