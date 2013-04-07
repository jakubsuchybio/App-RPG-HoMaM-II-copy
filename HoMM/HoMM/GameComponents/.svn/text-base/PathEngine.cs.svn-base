using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HoMM.Libraries;
using HoMM.TileEngine;

namespace HoMM.GameComponents
{
    public class PathEngine
    {
        #region Fields and Properties

        //Fieldy
        static List<Mriz.Vrchol> lastpath;
        Tile clickedTile;

        #endregion

        #region Methods

        /// <summary>
        /// Odstrani policko signalizujici oznaceni cesty
        /// </summary>
        /// <param name="t"></param>
        public static void DeleteTile(Point t)
        {
            Tile tmp = new Tile(-1, -1, t.X, t.Y);
            Session.FrontMap.mapLayers[2].SetTile(tmp.X, tmp.Y, tmp);
        }

        /// <summary>
        /// Smaze vsechny policka signalizujici oznaceni cesty
        /// </summary>
        public static void DeletePath()
        {
            if (lastpath != null)
                foreach (Mriz.Vrchol item in lastpath)
                {
                    Tile tmp = new Tile(-1, -1, item.X, item.Y);
                    Session.FrontMap.mapLayers[2].SetTile(item.X, item.Y, tmp);
                }
            lastpath = null;
        }

        public void Update()
        {
            //Pokud uzivatel klikl do prostoru mapy hry, tak se vykresli signalizovany policka pro nalezenou cestu
            //Pokud uzivatel klikl na stejne policko jako predtim, pak se zmeni stav hry na Pohyb a zacne presun hrdiny na danne policko
            if (MouseInput.MouseOver(Session.ViewportRect.X, Session.ViewportRect.Y, Session.ViewportRect.Width, Session.ViewportRect.Height) && MouseInput.LeftButtonPressed())
            {
                Tile start = Session.FrontMap.GetTile(Session.CurrentHero.Cell);
                Tile end = Session.FrontMap.GetTile(Session.FrontMap.MouseToPoint());

                if (clickedTile == end && lastpath != null)
                {
                    foreach (Mriz.Vrchol item in lastpath)
                    {
                        Session.path.Push(item);
                    }
                    lastpath = null;
                    clickedTile = null;
                    Session.state = GameState.Moving;
                }
                else
                {
                    DeletePath();
                    List<Mriz.Vrchol> path = Mriz.PathFind(start, end, Session.FrontMap.mapLayers[0].map, Session.MapProps.FrontSizeX);
                    if (path != null)
                    {
                        foreach (Mriz.Vrchol item in path)
                        {
                            Tile tmp = new Tile(1, 2, item.X, item.Y);
                            Session.FrontMap.mapLayers[2].SetTile(item.X, item.Y, tmp);
                        }
                        lastpath = path;
                        clickedTile = end;
                    }
                }
            }
        }

        #endregion
    }
}
