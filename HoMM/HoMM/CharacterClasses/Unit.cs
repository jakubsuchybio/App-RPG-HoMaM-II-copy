using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HoMM.GameComponents;

namespace HoMM.CharacterClasses
{
    public enum Notoriety 
    { 
        Friend, 
        Enemy
    }

    [Serializable]
    public class Unit : Character
    {
        #region Fields and Properties

        //Property
        public string Name
        {
            get;
            set;
        }
        public Notoriety Notoriety
        {
            get;
            set;
        }
        public bool isInHero
        {
            get;
            set;
        }
        public bool isDead
        {
            get
            {
                if (HitPoints.CurrentValue == 0)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region Constructors

        public Unit() { }
        public Unit(
            string name,
            int attack,
            int defense,
            AnimatedSprite animatedSprite,
            int x,
            int y,
            AttributePair atribut)
            : base(attack, defense, animatedSprite, x, y)
        {
            Name = name;
            isInHero = false;
            HitPoints = atribut;
        }

        /// <summary>
        /// Slouzi pro klonovani
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="not"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="atribut"></param>
        public Unit(Unit unit, Notoriety not, int x, int y, AttributePair atribut)
            : base(unit.attack, unit.defense, unit.animatedSprite, x, y)
        {
            Name = unit.Name;
            Notoriety = not;
            isInHero = false;
            HitPoints = atribut;
        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime)
        {
            //Pokud neni jednotka v hrdinovi, tak se updatuje
            if (!isInHero)
            {
                base.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Pokud neni jednotka v hrdinovi, tak se nad ni vykresli modry nebo cerveny prouzek oznamujici jeji Notoriety(pritel nebo nepritel)
            if (!isInHero)
            {
                Texture2D NotorietyTexture = new Texture2D(Session.GameRef.GraphicsDevice, 32, 4,true,SurfaceFormat.Color);
                if (Notoriety == Notoriety.Enemy)
                {
                    Color[] color = new Color[32 * 4];
                    for (int i = 0; i < color.Length; i++)
                    {
                        color[i] = Color.Red;
                    }
                    NotorietyTexture.SetData(color);
                }
                else if (Notoriety == Notoriety.Friend)
                {
                    Color[] color = new Color[32 * 4];
                    for (int i = 0; i < color.Length; i++)
                    {
                        color[i] = Color.Blue;
                    }
                    NotorietyTexture.SetData(color);
                }

                //Vykresleni
                spriteBatch.Draw(NotorietyTexture, Position, Color.White);

                base.Draw(gameTime, spriteBatch);
            }
        }

        /// <summary>
        /// Pokud je jednotka v hrdinovi, tak podle pozice okenka ve kterym ma byt se tam vykresli
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void DrawInHero(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isInHero)
            {
                switch (Session.CurrentHero.units.IndexOf(this))
                {
                    case 0:
                        {
                            DrawInHeroCell(new Rectangle(770, 350, 110, 125), spriteBatch);
                            break;
                        }
                    case 1:
                        {
                            DrawInHeroCell(new Rectangle(885, 350, 110, 125), spriteBatch);
                            break;
                        }
                    case 2:
                        {
                            DrawInHeroCell(new Rectangle(770, 480, 110, 125), spriteBatch);
                            break;
                        }
                    case 3:
                        {
                            DrawInHeroCell(new Rectangle(885, 480, 110, 125), spriteBatch);
                            break;
                        }
                    case 4:
                        {
                            DrawInHeroCell(new Rectangle(770, 610, 110, 125), spriteBatch);
                            break;
                        }
                    case 5:
                        {
                            DrawInHeroCell(new Rectangle(885, 610, 110, 125), spriteBatch);
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Vykresleni jednoho okenka signalizujici jednotku v hrdinovi
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="spriteBatch"></param>
        public void DrawInHeroCell(Rectangle pos, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.animatedSprite.texture, new Rectangle(pos.X, pos.Y + 50, 50, 65), animatedSprite.animations[AnimationKey.Down].CurrentFrameRect, Color.White);
            spriteBatch.DrawString(Session.font, "Utok:", new Vector2(pos.X + 50, pos.Y + 50), Color.Black);
            spriteBatch.DrawString(Session.font, Attack.ToString(), new Vector2(pos.X + 65, pos.Y + 65), Color.Black);
            spriteBatch.DrawString(Session.font, "Obrana:", new Vector2(pos.X + 50, pos.Y + 80), Color.Black);
            spriteBatch.DrawString(Session.font, Defense.ToString(), new Vector2(pos.X + 65, pos.Y + 95), Color.Black);
            spriteBatch.DrawString(Session.font, "HP / MaxHP:", new Vector2(pos.X + 10, pos.Y), Color.Black);
            spriteBatch.DrawString(Session.font, HitPoints.CurrentValue.ToString() + " / " + HitPoints.MaximumValue.ToString(), new Vector2(pos.X + 10, pos.Y + 15), Color.Black);

            Texture2D MaxTexture = new Texture2D(Session.GameRef.GraphicsDevice, 100, 10, false, SurfaceFormat.Color);
            int SizeOfHP = (100 * HitPoints.CurrentValue) / HitPoints.MaximumValue;
            Texture2D CurrentTexture = new Texture2D(Session.GameRef.GraphicsDevice, SizeOfHP, 10, false, SurfaceFormat.Color);
            Color[] color = new Color[100 * 10];
            for (int i = 0; i < color.Length; i++)
            {
                color[i] = Color.Black;
            }
            MaxTexture.SetData(color);
            color = new Color[SizeOfHP * 10];
            for (int i = 0; i < color.Length; i++)
            {
                color[i] = Color.Red;
            }
            CurrentTexture.SetData(color);

            spriteBatch.Draw(MaxTexture, new Vector2(pos.X + 5, pos.Y + 35), Color.White);
            spriteBatch.Draw(CurrentTexture, new Vector2(pos.X + 5, pos.Y + 35), Color.White);
        }

        /// <summary>
        /// Klonovani pro novou jednotku
        /// </summary>
        /// <param name="not"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Unit Clone(Notoriety not, int x, int y)
        {
            AnimatedSprite sprite = (AnimatedSprite)this.animatedSprite.Clone();
            Unit unitClone = new Unit(this.Name, this.Attack, this.Defense, sprite, x, y, this.HitPoints);
            Unit unitClone2 = new Unit(unitClone, not, x, y, this.HitPoints);

            return unitClone2;
        }

        /// <summary>
        /// Klonovani pri deserializaci
        /// </summary>
        /// <param name="not"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="attack"></param>
        /// <param name="defense"></param>
        /// <param name="atribut"></param>
        /// <param name="isInHero"></param>
        /// <returns></returns>
        public Unit Clone(Notoriety not, int x, int y, int attack, int defense, AttributePair atribut,bool isInHero)
        {
            AnimatedSprite sprite = (AnimatedSprite)this.animatedSprite.Clone();
            Unit unitClone = new Unit(this.Name, this.Attack, this.Defense, sprite, x, y, atribut);
            Unit unitClone2 = new Unit(unitClone, not, x, y, this.HitPoints);
            unitClone2.attack = attack;
            unitClone2.defense = defense;
            unitClone2.HitPoints = atribut;
            unitClone2.isInHero = isInHero;

            return unitClone2;
        }

        #endregion
    }
}