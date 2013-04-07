using System;
using System.Collections.Generic;
using HoMM.CharacterClasses;
using HoMM.TileEngine;

namespace HoMM.Serialization
{    
    /// <summary>
    /// Tridicka ktera rika co vsechno se ulozi kdyz se uklada mapa
    /// </summary>
    [Serializable]
    public class MapSerialize
    {
        #region Fields and Properties

        //Fieldy
        public List<GameKey> keys;
        public List<Unit> units;
        public MapProps mapProps;
        public MapLayer backLayer;
        public MapLayer backSplatter;
        public MapLayer frontLayer;
        public MapLayer frontSplatter;

        #endregion
    }
}
