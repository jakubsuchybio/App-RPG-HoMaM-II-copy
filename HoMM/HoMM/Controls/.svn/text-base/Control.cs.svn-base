using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoMM.Controls
{
    /// <summary>
    /// Abstraktni trida jakyhokoliv ovladaciho prvku
    /// </summary>
    public abstract class Control
    {
        #region Fields and Properties

        //Fieldy
        protected string name;
        protected string text;
        protected Vector2 size;
        protected Vector2 position;
        protected object value;
        protected bool hasFocus;
        protected bool enabled;
        protected bool visible;
        protected bool tabStop;
        protected SpriteFont spriteFont;
        protected Color color;
        public event EventHandler Selected;

        //Property
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }
        public virtual string Text
        {
            get { return text; }
            set { text = value; }
        }
        public virtual Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }
        public virtual Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public virtual object Value
        {
            get { return value; }
            set { this.value = value; }
        }
        public virtual bool HasFocus
        {
            get { return hasFocus; }
            set { hasFocus = value; }
        }        
        public virtual bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }        
        public virtual bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }        
        public virtual bool TabStop
        {
            get { return tabStop; }
            set { tabStop = value; }
        }
        public virtual SpriteFont SpriteFont
        {
            get { return spriteFont; }
            set { spriteFont = value; }
        }
        public virtual Color Color
        {
            get { return color; }
            set { color = value; }
        }
        
        #endregion
        
        #region Methods

        /// <summary>
        /// Abstraktni metody pro Update, Draw, HandleInput.
        /// </summary>
        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void HandleInput();

        /// <summary>
        /// Metoda pro eventy pri vyberu ovladaciho prvku
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSelected(EventArgs e)
        {
            if (Selected != null)
            {
                Selected(this, e);
            }
        }

        #endregion
    }
}