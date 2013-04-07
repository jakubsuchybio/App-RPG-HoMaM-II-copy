using System;
using Microsoft.Xna.Framework;
using HoMM.GameComponents;
using HoMM.CharacterClasses;

namespace HoMM.TileEngine
{
    [Serializable]
    public class Camera
    {
        #region Fields and Properties

        //Fieldy
        public Vector2 Position;
        float speed = 8f;

        //Property
        public float Speed
        {
            get { return speed; }
            set { speed = MathHelper.Clamp(speed, 1.0f, 16.0f); }
        }
        public Matrix TransformMatrix
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-Position + new Vector2(24,24), 0f));
            }
        }

        #endregion

        #region Constructors

        public Camera()
        {
            Position = Vector2.Zero;
        }
        public Camera(Vector2 position)
        {
            Position = position;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Uzamkne kameru na okraje mapy, aby neprejela za mapu
        /// </summary>
        public void LockCamera()
        {
            Position.X = MathHelper.Clamp(Position.X, 0, Session.FrontMap.WidthInPixels - Session.ViewportRect.Width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, Session.FrontMap.HeightInPixels - Session.ViewportRect.Height);
        }

        /// <summary>
        /// Uzamkne kameru na hrdinu 
        /// </summary>
        /// <param name="sprite"></param>
        public void LockToSprite(Hero sprite)
        {
            Position.X = sprite.Position.X + sprite.Sprite.Width / 2 - (Session.ViewportRect.Width / 2);
            Position.Y = sprite.Position.Y + sprite.Sprite.Height / 2 - (Session.ViewportRect.Height / 2);
        }

        #endregion
    }
}
