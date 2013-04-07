using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HoMM.GameComponents
{
    public class MouseInput : GameComponent
    {
        #region Fields and Properties

        //Fieldy
        static MouseState mouseState;
        static MouseState lastMouseState;

        //Property
        /// <summary>
        /// Vraci aktualni stav mysi
        /// </summary>
        public static MouseState MouseState
        {
            get { return mouseState; }
        }
        /// <summary>
        /// Vraci stav mysi minuly frame
        /// </summary>
        public static MouseState LastMouseState
        {
            get { return lastMouseState; }
        }

        #endregion

        #region Constructors

        public MouseInput(Game game) : base(game)
        {
            //Ziska prvni stav mysi pri inicializaci
            mouseState = Mouse.GetState();
        }

        #endregion

        #region Methods

        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Pokud je hra aktivni(ma focus ve windows), pak se updatuje stav mysi.       Divim se, ze tohle nezvlada samotnej framework...
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Session.GameRef.IsActive)
            {
                lastMouseState = mouseState;
                mouseState = Mouse.GetState();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Metoda pro kontrolu prejeti mysi nad rectanglem
        /// </summary>
        /// <param name="x">x pozice objektu</param>
        /// <param name="y">y pozice objektu</param>
        /// <param name="width">sirka objektu</param>
        /// <param name="height">vyska objektu</param>
        /// <returns>True pokud je nad objektem</returns>
        public static bool MouseOver(float x, float y, float width, float height)
        {
            if ((mouseState.X >= x) && (mouseState.X <= x + width) && (mouseState.Y >= y) && (mouseState.Y <= y + height))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Kontrola pusteni leveho tlacitka.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool LeftButtonReleased()
        {
            return mouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Konstrola stisku leveho tlacitka.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool LeftButtonPressed()
        {
            return mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// Kontrola pusteni praveho tlacitka.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool RightButtonReleased()
        {
            return mouseState.RightButton == ButtonState.Released && lastMouseState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Konstrola stisku praveho tlacitka.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool RightButtoPressed()
        {
            return mouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Released;
        }

        /// <summary>
        /// Kontrola pusteni prostredniho tlacitka.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool MiddleButtonReleased(Keys key)
        {
            return mouseState.MiddleButton == ButtonState.Released && lastMouseState.MiddleButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Konstrola stisku prostredniho tlacitka.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool MiddleButtonPressed(Keys key)
        {
            return mouseState.MiddleButton == ButtonState.Pressed && lastMouseState.MiddleButton == ButtonState.Released;
        }
        
        /// <summary>
        /// Nahradni minuly stav mysi za aktualni. (Napr pri zmene screenu)
        /// </summary>
        public static void Flush()
        {
            lastMouseState = mouseState;
        }

        /// <summary>
        /// Prevede do stringu aktualni pozici mysi
        /// </summary>
        /// <returns></returns>
        public static string ToStringg()
        {
            return (new Point(MouseState.X, MouseState.Y)).ToString();
        }

        #endregion
    }
}
