using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HoMM.GameComponents;
using HoMM.TileEngine;

namespace HoMM.CharacterClasses
{
    [Serializable]
    public abstract class AnimatedCharacter
    {
        #region Fields and Properties

        //Fieldy
        public Vector2 Position;
        protected AnimatedSprite animatedSprite;
        public Vector2 Direction;
        public Point Cell;

        #endregion

        #region Constructors

        public AnimatedCharacter() 
        {
        }

        public AnimatedCharacter(int x, int y)
        {
            Cell = new Point(x, y);
            Position = Session.FrontMap.CellToVector(Cell);
        }

        #endregion

        #region Methods

        public virtual void Update(GameTime gameTime)
        {
            //Kontroluje pozici a prizpusobuje ji souradnice v mape
            Vector2 tmp = Session.FrontMap.CellToVector(Cell);
            if (!isOnPosition(tmp))
            {
                Tile t = Session.FrontMap.VectorToTile(Position - Session.Camera.Position);
                Cell.X = t.X;
                Cell.Y = t.Y;
            }

            //V zavislosti na Smeru a stavu hry meni smer kam postavicka kouka
            switch (Session.state)
            {
                case GameState.Moving:
                    {
                        switch ((int)Direction.X)
                        {
                            case -1:
                                {
                                    animatedSprite.CurrentAnimation = AnimationKey.Left;
                                    break;
                                }
                            case 0:
                                {
                                    if ((int)Direction.Y == -1)
                                        animatedSprite.CurrentAnimation = AnimationKey.Up;
                                    else if ((int)Direction.Y == 1)
                                        animatedSprite.CurrentAnimation = AnimationKey.Down;
                                    break;
                                }
                            case 1:
                                {
                                    animatedSprite.CurrentAnimation = AnimationKey.Right;
                                    break;
                                }
                        }
                        break;
                    }
                case GameState.Stable:
                    {
                        animatedSprite.CurrentAnimation = AnimationKey.Down;
                        break;
                    }
            }

            //Update pozice obrazku pro vykresleni a update
            animatedSprite.Position = Position;
            animatedSprite.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Vykresleni
            animatedSprite.Draw(spriteBatch);
        }

        /// <summary>
        /// Pomocna trida pro kontrolu jestli se postavicka za minuly frame neposunula k jinymu policku na maximalni vzdalenost jeji rychlosti
        /// </summary>
        /// <param name="endPosition">pozice kde by mela byt</param>
        /// <returns>jestli je na dany pozici</returns>
        public bool isOnPosition(Vector2 endPosition)
        {
            return ((MathHelper.Distance(Position.X, endPosition.X) <= animatedSprite.Speed) && (MathHelper.Distance(Position.Y, endPosition.Y) <= animatedSprite.Speed));
        }

        #endregion
    }
}
