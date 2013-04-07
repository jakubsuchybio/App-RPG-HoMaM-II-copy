using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HoMM.GameComponents
{
    public class MenuComponent
    {
        #region Fields and Properties

        //Fieldy
        public string[] menuItems;
        int selectedIndex;
        private int index;
        float width;
        float height;
        Vector2 position;
        SpriteFont spriteFont;

        //Property
        public int clickedIndex
        {
            get 
            {
                return index;
            }
            set
            {
                if (value < 0)
                    index = 0;
                else
                    index = value;
            }
        }
        public Color NormalColor
        {
            get;
            set;
        }
        public Color HiliteColor
        {
            get;
            set;
        }
        public Color SelectedColor
        {
            get;
            set;
        }
        public bool Centered
        {
            get;
            set;
        }
        public float Width
        {
            get { return width; }
        }
        public float Height
        {
            get { return height; }
        }
        public int SelectedIndex
        {
            get { return selectedIndex; }
        }

        #endregion

        #region Constructors

        public MenuComponent(SpriteFont spriteFont, string[] items, bool centered)
        {
            this.spriteFont = spriteFont;
            SetMenuItems(items);
            NormalColor = Color.White;
            HiliteColor = Color.Red;
            Centered = centered;
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Nastavuje novou pozici celeho menu.
        /// </summary>
        /// <param name="position">Vektor nove pozice</param>
        public void SetPostion(Vector2 position)
        {
            this.position = position;
        }

        /// <summary>
        /// Nastavuje polozky menu.
        /// Zmeri a ulozi velikost menu
        /// </summary>
        /// <param name="items">pole stringu polozek menu</param>
        public void SetMenuItems(string[] items)
        {
            menuItems = (string[])items.Clone();
            MeasureMenu();
        }

        /// <summary>
        /// Zmeri a ulozi velikosti menu
        /// </summary>
        private void MeasureMenu()
        {
            width = 0;
            height = 0;
            foreach (string s in menuItems)
            {
                if (width < spriteFont.MeasureString(s).X)
                    width = spriteFont.MeasureString(s).X;
                height += spriteFont.LineSpacing;
            }
        }

        /// <summary>
        /// Metoda urcujici jestli je mys nad komponentou ci nikoliv
        /// </summary>
        /// <returns></returns>
        public bool isMouseOver()
        {
            if (MouseInput.MouseOver(position.X, position.Y, this.Width, this.Height))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Vola se kazdy frame
        /// Nastavuje selectedIndex
        /// </summary>
        public void Update()
        {
            if (KeyboardInput.KeyPressed(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }
            if (KeyboardInput.KeyPressed(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex >= menuItems.Length)
                    selectedIndex = 0;
            }
            if (MouseInput.MouseOver(position.X, position.Y, this.Width, this.Height))
            {
                selectedIndex = (int)(((float)MouseInput.MouseState.Y - position.Y) / ((float)spriteFont.LineSpacing + spriteFont.Spacing));
                if (selectedIndex >= menuItems.Length)
                    selectedIndex = menuItems.Length - 1;
            }
        }

        /// <summary>
        /// Vykresli menu kazdy frame
        /// Barva kazdeho prvku zalesi na selectedIndexu a clickedIndexu
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 menuPosition = position;
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (Centered)
                    menuPosition.X = position.X + this.Width / 2 - spriteFont.MeasureString(menuItems[i]).X / 2;
                if (i == selectedIndex)
                    spriteBatch.DrawString(spriteFont, menuItems[i], menuPosition, HiliteColor);
                else
                    spriteBatch.DrawString(spriteFont, menuItems[i], menuPosition, NormalColor);

                if (i == clickedIndex && SelectedColor != null)
                    spriteBatch.DrawString(spriteFont, menuItems[i], menuPosition, SelectedColor);

                menuPosition.Y += spriteFont.LineSpacing;
            }
        }

        #endregion
    }
}
