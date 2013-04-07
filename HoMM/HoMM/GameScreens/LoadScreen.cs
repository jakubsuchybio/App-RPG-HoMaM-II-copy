using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HoMM.Controls;
using HoMM.GameComponents;

namespace HoMM.GameScreens
{
    public class LoadScreen : GameScreen
    {
        #region Fields and Properties

        //Fieldy
        ScreenManager manager;
        BackgroundComponent background;
        ControlManager Controls;

        #endregion

        #region Constructors

        public LoadScreen(Game game, ScreenManager manager)
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
            //Nacteni pozadi a fontu
            background = new BackgroundComponent(GameRef, Content.Load<Texture2D>(@"Backgrounds\startscreen"), DrawMode.Center);
            SpriteFont spriteFont = Content.Load<SpriteFont>(@"Fonts\menuFont");

            //Ziskani vsech ulozenych her ze slozky Saves 
            System.IO.DirectoryInfo dir = new DirectoryInfo(Session.GameRef.Content.RootDirectory + "\\Saves");
            FileInfo[] files = dir.GetFiles();

            //Nastaveni vsech tlacitek na tlacitka ulozenych her pro nahrani hry
            for (int i = 0; i < 10; i++)
            {
                if (i < files.Length)
                {
                    LinkLabel linkLabel = new LinkLabel(spriteFont);
                    linkLabel.Text = files[i].Name.Substring(0, files[i].Name.Length - 4);
                    linkLabel.Size = linkLabel.SpriteFont.MeasureString(linkLabel.Text);
                    linkLabel.Position = new Vector2((GameRef.Window.ClientBounds.Width - linkLabel.Size.X) / 2, 250 + i * 25);
                    linkLabel.Selected += new EventHandler(linkLabel_Selected);
                    Controls.Add(linkLabel);
                }
                else
                {
                    LinkLabel linkLabel = new LinkLabel(spriteFont);
                    linkLabel.Text = "null";
                    linkLabel.Size = linkLabel.SpriteFont.MeasureString(linkLabel.Text);
                    linkLabel.Position = new Vector2((GameRef.Window.ClientBounds.Width - linkLabel.Size.X) / 2, 250 + i * 25);
                    linkLabel.Enabled = false;
                    linkLabel.Selected += new EventHandler(linkLabel_Selected);
                    Controls.Add(linkLabel);
                }
            }
        }

        /// <summary>
        /// Obecna metoda pro stisknuti tlacitka
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void linkLabel_Selected(object sender, EventArgs e)
        {
            KeyboardInput.Flush();

            //Zjisteni vybraneho tlacitka
            LinkLabel s = sender as LinkLabel;

            //A nahrani jeho nazvu hry
            Session.LoadGame(s.Text + ".xml");

            //Vraceni na hlavni menu a okamzity prehozeni na herni okno
            manager.PopScreen();
            manager.PushScreen(GameRef.GamePlayScreen);
        }

        public override void Update(GameTime gameTime)
        {
            //Osetreni escapu pro vraceni na startscreen
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

            //Vykresleni pozadi a vsech ovladacich prvkuu
            background.Draw(Game1.SpriteBatch);
            Controls.Draw(Game1.SpriteBatch);
            base.Draw(gameTime);

            Game1.SpriteBatch.End();  //Konec vykreslovani
        }

        #endregion
    }
}
