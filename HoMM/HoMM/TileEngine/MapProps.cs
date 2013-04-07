using System;

namespace HoMM.TileEngine
{
    [Serializable]
    public class MapProps
    {
        #region Fields and Properties

        //Fieldy
        int backSizeX;
        int backSizeY;
        int frontSizeX;
        int frontSizeY;
        int backEngineX;
        int backEngineY;
        int frontEngineX;
        int frontEngineY;

        //Property
        public int BackSizeX
        {
            get { return backSizeX; }
            set { backSizeX = value; }
        }
        public int BackSizeY
        {
            get { return backSizeY; }
            set { backSizeY = value; }
        }
        public int FrontSizeX
        {
            get { return frontSizeX; }
            set { frontSizeX = value; }
        }
        public int FrontSizeY
        {
            get { return frontSizeY; }
            set { frontSizeY = value; }
        }
        public int BackEngineX
        {
            get { return backEngineX; }
            set { backEngineX = value; }
        }
        public int BackEngineY
        {
            get { return backEngineY; }
            set { backEngineY = value; }
        }
        public int FrontEngineX
        {
            get { return frontEngineX; }
            set { frontEngineX = value; }
        }
        public int FrontEngineY
        {
            get { return frontEngineY; }
            set { frontEngineY = value; }
        }

        #endregion

        #region Constructors

        public MapProps() { }
        public MapProps(int backSizeX, int backSizeY, int frontSizeX, int frontSizeY, int backEngineX, int backEngineY, int frontEngineX, int frontEngineY)
        {
            this.backSizeX = backSizeX;
            this.backSizeY = backSizeY;
            this.frontSizeX = frontSizeX;
            this.frontSizeY = frontSizeY;
            this.backEngineX = backEngineX;
            this.backEngineY= backEngineY;
            this.frontEngineX = frontEngineX;
            this.frontEngineY = frontEngineY;
        }

        #endregion
    }
}
