using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HoMM.GameComponents;

namespace HoMM.CharacterClasses
{
    [Serializable]
    public class GameKey
    {
        #region Fields and Properties

        //Fieldy
        protected AnimatedSprite animatedSprite;
        public Vector2 Position;
        public Point Cell;
        public int index;

        #endregion

        #region Constructors

        public GameKey() { }
        public GameKey(int index, AnimatedSprite sprite, int x, int y)
        {
            this.index = index;
            Cell = new Point(x, y);
            Position = Session.FrontMap.CellToVector(Cell) + Session.Camera.Position;
            animatedSprite = sprite;
            animatedSprite.IsAnimating = true;
        }
        public GameKey(GameKey klic, int x, int y)
        {
            this.index = klic.index;
            Cell = new Point(x, y);
            Position = Session.FrontMap.CellToVector(Cell) + Session.Camera.Position;
            animatedSprite = klic.animatedSprite;
        }
        
        #endregion

        #region Methods

        public virtual void Update(GameTime gameTime)
        {
            //Nastaveni pozice obrazku pro vykresleni a update jeho animace
            animatedSprite.Position = Position;
            animatedSprite.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Vykresleni
            animatedSprite.Draw(spriteBatch);
        }

        /// <summary>
        /// Naklonovani klice s urcenim novych souradnice
        /// </summary>
        /// <param name="x">Nova souradnice X</param>
        /// <param name="y">Nova souradnice Y</param>
        /// <returns></returns>
        public GameKey Clone(int x, int y)
        {
            GameKey unitClone = new GameKey(index, this.animatedSprite, x, y);
            GameKey unitClone2 = new GameKey(unitClone, x, y);

            return unitClone2;
        }

        #endregion
    }
}