using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HoMM.GameComponents;
using HoMM.TileEngine;

namespace HoMM.CharacterClasses
{
    [Serializable]
    public abstract class Character
    {
        #region Fields and Properties

        //Fieldy
        public AttributePair HitPoints;
        protected int attack;
        protected int defense;
        public Vector2 Position;
        public Point Cell;
        public Vector2 Direction;

        [XmlIgnore] //Znamena, ze se pri Serializaci(ukladani) neulozi
        public AnimatedSprite animatedSprite;

        //Property
        public virtual int Attack
        {
            get { return attack; }
            set { attack = value; }
        }
        public virtual int Defense
        {
            get { return defense; }
            set { defense = value; }
        }

        #endregion

        #region Constructors

        public Character() { }
        public Character(
        int attack,
        int defense,
        AnimatedSprite sprite,
            int x,
            int y)
        {
            Attack = attack;
            Defense = defense;
            animatedSprite = sprite;
            animatedSprite.IsAnimating = true;
            HitPoints = new AttributePair();
            Cell = new Point(x, y);
            Position = Session.FrontMap.CellToVector(Cell) + Session.Camera.Position;
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

            //V zavislosti na Smeru meni smer kam postavicka kouka
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
        /// Spoji dva charaktery do jednoho
        /// </summary>
        /// <param name="ch">Charakter ke spojeni</param>
        public void Fuse(Character ch)
        {
            this.Attack += ch.Attack;
            this.Defense += ch.Defense;
            this.HitPoints.Enhance(Convert.ToUInt16(ch.HitPoints.CurrentValue), Convert.ToUInt16(ch.HitPoints.MaximumValue));
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