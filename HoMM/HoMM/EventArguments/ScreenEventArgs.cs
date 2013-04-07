using System;
using HoMM.GameComponents;

namespace HoMM.EventArguments
{
    /// <summary>
    /// EventArgument pro rizeni prepinani oken a vyslani zprav
    /// </summary>
    public class ScreenEventArgs : EventArgs
    {
        #region Fields and Properties

        //Fieldy
        GameScreen gameScreen;
        bool isMessage;

        //Property
        public GameScreen GameScreen
        {
            get { return gameScreen; }
            private set { gameScreen = value; }
        }
        public bool IsMessage
        {
            get { return isMessage; }
            private set { isMessage = value; }
        }

        #endregion

        #region Constructors

        public ScreenEventArgs(GameScreen gameScreen, bool isMessage)
        {
            GameScreen = gameScreen;
            IsMessage = isMessage;
        }

        #endregion
    }
}
