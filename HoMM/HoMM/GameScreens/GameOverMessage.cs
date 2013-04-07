using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HoMM.GameComponents;
using HoMM.Controls;

namespace HoMM.GameScreens
{
    public class GameOverMessage : GameScreen
    {
        #region Fields and Properties

        //Fieldy
        SpriteFont spriteFont;
        ScreenManager manager;
        BackgroundComponent background;
        ControlManager Controls;

        #endregion

        #region Constructors

        public GameOverMessage(Game game, ScreenManager manager)
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
            spriteFont = Content.Load<SpriteFont>(@"Fonts\editorFont");

            //Vytvoreni pozadi
            background = new BackgroundComponent(GameRef, Content.Load<Texture2D>(@"Backgrounds\gameovermessage"), DrawMode.Center);

            //Vytvoreni tlacitka pro potvrzeni vyberu
            LinkLabel Button = new LinkLabel(spriteFont);
            Button.Text = "Konec";
            Button.Size = Button.SpriteFont.MeasureString(Button.Text);
            Button.Position = new Vector2(240, 450);
            Controls.Add(Button);
            //Pridani eventu pro vyber
            Button.Selected += new EventHandler(Button_Selected);

            base.LoadContent();
        }

        /// <summary>
        /// Odkliknuti zpravicky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_Selected(object sender, EventArgs e)
        {
            manager.ChangeScreens(Session.GameRef.StartScreen);
        }

        public override void Update(GameTime gameTime)
        {
            //Osetreni escapu ktery nastavi novy okno startscreen
            if (KeyboardInput.KeyReleased(Keys.Escape))
            {
                manager.ChangeScreens(Session.GameRef.StartScreen);
            }
            Controls.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.SpriteBatch.Begin();    //Zacatek vykreslovani

            //Vykresleni pozadi a ovladacich prvkuu
            background.Draw(Game1.SpriteBatch);
            Controls.Draw(Game1.SpriteBatch);
            base.Draw(gameTime);

            Game1.SpriteBatch.End();  //Konec vykreslovani
        }

        #endregion
    }
}
