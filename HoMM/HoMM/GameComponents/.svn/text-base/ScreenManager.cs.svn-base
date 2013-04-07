using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using HoMM.EventArguments;

namespace HoMM.GameComponents
{
    public class ScreenManager : GameComponent
    {
        #region Fields and Properties

        //Fieldy a konstanty
        Stack<GameScreen> gameScreens = new Stack<GameScreen>();
        public event EventHandler<ScreenEventArgs> OnScreenChange;
        const int startDrawOrder = 5000;
        const int drawOrderInc = 100;
        int drawOrder;

        //Properta
        public GameScreen CurrentScreen
        {
            get { return gameScreens.Peek(); }
        }

        #endregion

        #region Constructors

        public ScreenManager(Game game)
            : base(game)
        {
            drawOrder = startDrawOrder;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Smaze z vykreslovani aktualni screen a nacte predchozi ze zasobniku
        /// </summary>
        public void PopScreen()
        {
            RemoveScreen();
            drawOrder -= drawOrderInc;
            if (OnScreenChange != null)
                OnScreenChange(this, new ScreenEventArgs(gameScreens.Peek(), false));
        }

        /// <summary>
        /// Smaze screen z komponent hry pro vykresleni a smaze ho ze zasobniku
        /// </summary>
        private void RemoveScreen()
        {
            GameScreen screen = gameScreens.Peek();
            OnScreenChange -= screen.ScreenChange;
            Game.Components.Remove(screen);
            gameScreens.Pop();
        }

        /// <summary>
        /// Vtlaceni noveho okna na zasobnik a jeho nasledne vykresleni a stare okno odvykreslit
        /// </summary>
        /// <param name="newScreen"></param>
        public void PushScreen(GameScreen newScreen)
        {
            drawOrder += drawOrderInc;
            newScreen.DrawOrder = drawOrder;
            AddScreen(newScreen);
            if (OnScreenChange != null)
                OnScreenChange(this, new ScreenEventArgs(newScreen, false));
        }

        /// <summary>
        /// Vtlaceni noveho okna na zasobnik a jeho nasledne vykresleni a stare okno pauznout
        /// </summary>
        /// <param name="newScreen"></param>
        public void PushMessage(GameScreen newScreen)
        {
            drawOrder += drawOrderInc;
            newScreen.DrawOrder = drawOrder;
            gameScreens.Push(newScreen);
            Game.Components.Add(newScreen);
            OnScreenChange += newScreen.ScreenChange;
            if (OnScreenChange != null)
                OnScreenChange(this, new ScreenEventArgs(newScreen, true));
        }

        /// <summary>
        /// Pridani okna do zasobniku a nastaveni delegata pro zmenu okna
        /// </summary>
        /// <param name="newScreen"></param>
        private void AddScreen(GameScreen newScreen)
        {
            gameScreens.Push(newScreen);
            Game.Components.Add(newScreen);
            OnScreenChange += newScreen.ScreenChange;
        }

        /// <summary>
        /// Smazani zasobniku oken a nastaveni prvniho noveho okna na zasobnik a jeho vykresleni
        /// </summary>
        /// <param name="newScreen"></param>
        public void ChangeScreens(GameScreen newScreen)
        {
            MouseInput.Flush();
            KeyboardInput.Flush();
            while (gameScreens.Count > 0)
                RemoveScreen();
            newScreen.DrawOrder = startDrawOrder;
            drawOrder = startDrawOrder;
            AddScreen(newScreen);
            if (OnScreenChange != null)
                OnScreenChange(this, new ScreenEventArgs(newScreen, false));
        }

        #endregion
    }
}