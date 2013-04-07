using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HoMM.Libraries;

namespace HoMM.TileEngine
{
    public class TileMap : Engine
    {
        #region Fields and Properties

        //Filedy
        public List<Tileset> tilesets;
        public List<MapLayer> mapLayers;
        public int mapWidth;
        public int mapHeight;
        
        //Property
        public int WidthInPixels
        {
            get { return mapWidth * TileWidth; }
        }
        public int HeightInPixels
        {
            get { return mapHeight * TileHeight; }
        }

        #endregion

        #region Constructors

        public TileMap(List<Tileset> tilesets, List<MapLayer> layers, int mapWidth, int mapHeight, int tileWidth, int tileHeight)
            : base(tileWidth, tileHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;

            this.tilesets = tilesets;
            this.mapLayers = layers;
        }
        public TileMap(Tileset tileset, MapLayer layer, int tileWidth, int tileHeight)
            : base(tileWidth, tileHeight)
        {
            mapWidth = layer.Width;
            mapHeight = layer.Height;

            tilesets = new List<Tileset>();
            tilesets.Add(tileset);
            mapLayers = new List<MapLayer>();
            mapLayers.Add(layer);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Pridani vrstvy
        /// </summary>
        /// <param name="layer"></param>
        internal void AddLayer(MapLayer layer)
        {
            if (layer.Width != mapWidth || layer.Height != mapHeight)
                throw new Exception("Error in layer size.");
            mapLayers.Add(layer);
        }

        /// <summary>
        /// Vykresleni vsech vrstev dany mapy
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destination = new Rectangle(0, 0, TileWidth, TileHeight);
            Tile tile;

            foreach (MapLayer layer in mapLayers)
            {
                //Vykresleni pouze casi mapy, ktera je viditelna kamerou + 1 policko navic, kvuli tomu aby na kamere nebylo prazdny misto mapy
                Point start = TopLeftVisibleTile();
                Point end = DownRightVisibleTile(mapWidth, mapHeight);

                for (int y = start.Y; y < end.Y; y++)
                {
                    destination.Y = y * TileHeight;
                    for (int x = start.X; x < end.X; x++)
                    {
                        tile = layer.GetTile(x, y);
                        if (tile.TileIndex == -1)
                            continue;
                        destination.X = x * TileWidth;

                        spriteBatch.Draw(
                        tilesets[tile.Tileset].Texture,
                        destination,
                        tilesets[tile.Tileset].SourceRectangles[tile.TileIndex],
                        Color.White);
                    }
                }
            }
        }

        /// <summary>
        /// Vrati Tile z mapy ze souradnic, Point nebo Mriz.Vrcholu
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
            return mapLayers[0].map[x, y];
        }

        #endregion
    }
}