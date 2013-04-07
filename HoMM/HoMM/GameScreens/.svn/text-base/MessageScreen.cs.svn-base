using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HoMM.GameComponents;
using HoMM.Controls;

namespace HoMM.GameScreens
{
    public class MessageScreen : GameScreen
    {
        #region Fields and Properties

        //Fieldy
        SpriteFont spriteFont;
        ScreenManager manager;
        BackgroundComponent background;
        ControlManager Controls;
        Label message;

        #endregion

        #region Constructors

        public MessageScreen(Game game, ScreenManager manager)
            : base(game)
        {
            Content = Game.Content;
            this.manager = manager;
            Controls = new ControlManager();
        }

        #endregion

        #region Methods

        protected override void LoadContent()
        {
            //Nacteni pisma
            spriteFont = Content.Load<SpriteFont>(@"Fonts\menuFont");

            //Vytvoreni pozadi
            background = new BackgroundComponent(GameRef, Content.Load<Texture2D>(@"Backgrounds\Message"), DrawMode.Center);

            //Vytvoreni labelu pro zpravu
            message = new Label(spriteFont);
            message.Text = Session.Message;
            message.Position = new Vector2(215, 250);
            Controls.Add(message);

            //Vytvoreni tlacitka pro potvrzeni vyberu
            LinkLabel Button = new LinkLabel(spriteFont);
            Button.Text = "Ok.";
            Button.Size = Button.SpriteFont.MeasureString(Button.Text);
            Button.Position = new Vector2(215, 450);
            Controls.Add(Button);
            //Pridani eventu pro vyber
            Button.Selected += new EventHandler(Button_Selected);

            base.LoadContent();
        }

        /// <summary>
        /// Odkliknuti vzkazoveho okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_Selected(object sender, EventArgs e)
        {
            manager.PopScreen();
        }

        public override void Update(GameTime gameTime)
        {
            //Kontrola escapu pro zavreni zpravy
            if (KeyboardInput.KeyReleased(Keys.Escape))
            {
                manager.PopScreen();
            }

            //Update textu zpravy
            message.Text = Session.Message;
            Controls.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.SpriteBatch.Begin();    //Zacatek vykreslovani

            //Vykresleni pozadi a vsech ovladacich prvkuu
            background.Draw(Game1.SpriteBatch);
            Controls.Draw(Game1.SpriteBatch);
            base.Draw(gameTime);

            Game1.SpriteBatch.End();  //Konec vykreslovani
        }

        #endregion
    }
}
