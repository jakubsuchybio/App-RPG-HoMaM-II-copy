using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HoMM.GameComponents;
using HoMM.Controls;

namespace HoMM.GameScreens
{
    public class endMessage : GameScreen
    {
        #region Fields and Properties

        //Fieldy
        SpriteFont spriteFont;
        ScreenManager manager;
        BackgroundComponent background;
        ControlManager Controls;

        #endregion

        #region Constructor

        public endMessage(Game game, ScreenManager manager)
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
            background = new BackgroundComponent(GameRef, Content.Load<Texture2D>(@"Backgrounds\endMessage"), DrawMode.Center);

            //Vytvoreni tlacitka pro potvrzeni vyberu
            LinkLabel AnoButton = new LinkLabel(spriteFont);
            AnoButton.Text = "Ano";
            AnoButton.Size = AnoButton.SpriteFont.MeasureString(AnoButton.Text);
            AnoButton.Position = new Vector2(245,450);
            Controls.Add(AnoButton);
            //Pridani eventu pro vyber
            AnoButton.Selected += new EventHandler(OnAno_Selected);

            //Vytvoreni tlacitka pro potvrzeni vyberu
            LinkLabel SaveButton = new LinkLabel(spriteFont);
            SaveButton.Text = "Ulozit";
            SaveButton.Size = SaveButton.SpriteFont.MeasureString(SaveButton.Text);
            SaveButton.Position = new Vector2(345,450);
            Controls.Add(SaveButton);
            //Pridani eventu pro vyber
            SaveButton.Selected += new EventHandler(OnSave_Selected);

            //Vytvoreni tlacitka pro potvrzeni vyberu
            LinkLabel NeButton = new LinkLabel(spriteFont);
            NeButton.Text = "Ne";
            NeButton.Size = NeButton.SpriteFont.MeasureString(NeButton.Text);
            NeButton.Position = new Vector2(445,450);
            Controls.Add(NeButton);
            //Pridani eventu pro vyber
            NeButton.Selected += new EventHandler(OnNe_Selected);

            base.LoadContent();
        }

        /// <summary>
        /// Metoda obsluhujici kliknuti na tlacitko Ano
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnAno_Selected(object sender, EventArgs e)
        {
            manager.ChangeScreens(Session.GameRef.StartScreen);
        }

        /// <summary>
        /// Metoda obsluhujici kliknuti na tlacitko Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSave_Selected(object sender, EventArgs e)
        {
            Session.SaveGame("EndSave.xml");
            MouseInput.Flush();
            manager.ChangeScreens(Session.GameRef.StartScreen);
        }

        /// <summary>
        /// Metoda obsluhujici kliknuti na tlacitko Ne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnNe_Selected(object sender, EventArgs e)
        {
            manager.PopScreen();
        }

        public override void Update(GameTime gameTime)
        {
            //Kontrola stistu klavesy escape pro vraceni do hry
            if (KeyboardInput.KeyReleased(Keys.Escape))
            {
                manager.PopScreen();
            }
            Controls.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.SpriteBatch.Begin();    //Zacatek vykreslovani

            //Vykresleni mapy a ovladacich prvkuu
            background.Draw(Game1.SpriteBatch);
            Controls.Draw(Game1.SpriteBatch);
            base.Draw(gameTime);

            Game1.SpriteBatch.End();  //Konec vykreslovani
        }

        #endregion
    }
}
