using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoMM.CharacterClasses
{
    [Serializable]
    public class Hero : AnimatedCharacter
    {
        #region Fields and Properties

        //Fieldy
        public string Gender;
        public List<int> keys;
        public List<Unit> units;

        //Property
        [XmlIgnore] //Znamena, ze se pri Serializaci(ukladani) neulozi
        public AnimatedSprite Sprite
        {
            get { return animatedSprite; }
            set { animatedSprite = value; }
        }

        #endregion

        #region Constructors

        public Hero() { }

        /// <summary>
        /// Slouzi pro deserializaci(nacteni)
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="animatedSprite"></param>
        public Hero(Hero hero, AnimatedSprite animatedSprite)
        {
            keys = new List<int>();
            units = new List<Unit>();
            Gender = hero.Gender;
            Cell.X = hero.Cell.X;
            Cell.Y = hero.Cell.Y;
            Position = hero.Position;
            Direction = hero.Direction;
            Sprite = animatedSprite;
            Sprite.Position = Position;
            keys = hero.keys;
        }

        public Hero(int x, int y, AnimatedSprite animatedSprite)
            : base(x, y)
        {
            keys = new List<int>();
            units = new List<Unit>();
            Sprite = animatedSprite;
        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch); 
        }

        public void CheckUnits()
        {
            int k = 0;
            while (k < units.Count)
            {
                if (units[k].isDead)
                    units.Remove(units[k]);
                else
                    k++;
            }
        }

        #endregion
    }
}