using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using HoMM.EventArguments;

namespace HoMM.GameComponents
{
    public abstract class GameScreen : DrawableGameComponent
    {
        #region Fields and Properties

        //Fieldy
        List<GameComponent> childComponents;
        protected ContentManager Content;
        protected Game1 GameRef;

        //Properta
        public List<GameComponent> Components
        {
            get { return childComponents; }
        }

        #endregion

        #region Constructors

        public GameScreen(Game game) : base(game)
        {
            childComponents = new List<GameComponent>();
            GameRef = (Game1)game;
        }

        #endregion

        #region Methods

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //Updatovani vsech komponent
            foreach (GameComponent component in childComponents)
            {
                if (component.Enabled)
                    component.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //Vykresleni vsech komponent pokud jsou viditelne
            DrawableGameComponent drawComponent;
            foreach (GameComponent component in childComponents)
            {
                if (component is DrawableGameComponent)
                {
                    drawComponent = component as DrawableGameComponent;
                    if (drawComponent.Visible)
                        drawComponent.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

        /// <summary>
        /// Pri zmene screenu se delegatem zavola tato metoda aby Skryla nebo Zviditelnila nebo Pozastavila danny screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal protected virtual void ScreenChange(object sender, ScreenEventArgs e)
        {
            if (e.GameScreen == this)
                Show();
            else if (e.IsMessage)
                Disable();
            else
                Hide();
        }

        /// <summary>
        /// Zviditelneni screenu
        /// </summary>
        private void Show()
        {
            Visible = true;
            Enabled = true;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }
        }

        /// <summary>
        /// Pozastaveni screenu
        /// </summary>
        private void Disable()
        {
            Visible = true;
            Enabled = false;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }
        }
        
        /// <summary>
        /// Skryti screenu
        /// </summary>
        private void Hide()
        {
            Visible = false;
            Enabled = false;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = false;
            }
        }

        #endregion
    }
}
