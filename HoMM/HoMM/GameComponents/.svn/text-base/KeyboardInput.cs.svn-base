using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HoMM.GameComponents
{
    public class KeyboardInput : GameComponent
    {
        #region Fields and Properties

        //Fieldy
        static KeyboardState keyboardState;
        static KeyboardState lastKeyboardState;

        //Property
        /// <summary>
        /// Aktualni stav klavesnice
        /// </summary>
        public static KeyboardState KeyboardState
        {
            get { return keyboardState; }
        }

        /// <summary>
        /// Status klavesnice o frame zpet
        /// </summary>
        public static KeyboardState LastKeyboardState
        {
            get { return lastKeyboardState; }
        }

        #endregion

        #region Constructors

        public KeyboardInput(Game game) : base(game)
        {
            //Zjisti a ulozi si prvni stav klavesnice
            keyboardState = Keyboard.GetState();
        }

        #endregion

        #region Methods

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            lastKeyboardState = keyboardState;

            //Obnoveni noveho stavu klavesnice
            keyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// Kontrola pusteni tlacitka klavesnice.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyReleased(Keys key)
        {
            return keyboardState.IsKeyUp(key) &&
            lastKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Konstrola stlaceni tlacitka klavesnice
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) &&
            lastKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Signalizuje jestli je tlacitko stlacene
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Nahradi minuly stav za aktualni (napr pri zmene screenu)
        /// </summary>
        public static void Flush()
        {
            lastKeyboardState = keyboardState;
        }

        #endregion
    }
}
