using System;
using System.Collections.Generic;
using HoMM.CharacterClasses;
using HoMM.TileEngine;

namespace HoMM.Serialization
{
    /// <summary>
    /// Tridicka ktera rika co vsechno se ulozi kdyz se uklada hra
    /// </summary>
    [Serializable]
    public class GameSerialize
    {
        #region Fields and Properties

        //Fieldy
        public string mapName;
        public Hero hero;
        public Camera cam;
        public List<GameKey> keys;
        public List<Unit> units;
        public MapLayer frontSplatter;

        #endregion
    }
}