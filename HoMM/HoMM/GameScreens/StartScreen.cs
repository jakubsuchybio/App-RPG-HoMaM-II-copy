using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HoMM.GameComponents;

namespace HoMM.GameScreens
{
    public class StartScreen : GameScreen
    {
        #region Fields and Properties

        //Fieldy
        MenuComponent menu;
        SpriteFont spriteFont;
        string[] menuItems = { "New Game", "Load Game", "Editor", "Exit" };
        ScreenManager manager;
        BackgroundComponent background;

        #endregion

        #region Constructors

        public StartScreen(Game game, ScreenManager manager) : base(game)
        {
            Content = Game.Content;
            this.manager = manager;
        }

        #endregion

        #region Methods

        protected override void LoadContent()
        {
            //Nacteni pisma
            spriteFont = Content.Load<SpriteFont>(@"Fonts\startFont");

            //Vytvoreni menu
            menu = new MenuComponent(spriteFont, menuItems, true);

            //Nastaveni pozice
            Vector2 menuPosition = new Vector2(
            (Game.Window.ClientBounds.Width - menu.Width) / 2,
            (Game.Window.ClientBounds.Height - menu.Height) / 2);
            menu.SetPostion(menuPosition);

            //Vytvoreni pozadi
            background = new BackgroundComponent(GameRef, Content.Load<Texture2D>(@"Backgrounds\startscreen"), DrawMode.Center);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //Kontrola odkliknuti nejakeho tlacitka menu
            if (KeyboardInput.KeyReleased(Keys.Enter) || (menu.isMouseOver() && MouseInput.LeftButtonPressed()))
            {
                switch (menu.SelectedIndex)
                {
                    case 0:
                        KeyboardInput.Flush();
                        manager.PushScreen(GameRef.CharacterGenerator);
                        break;
                    case 1:
                        KeyboardInput.Flush();
                        manager.PushScreen(GameRef.LoadScreen);
                        break;
                    case 2:
                        manager.PushScreen(GameRef.EditorScreen);
                        break;
                    case 3:
                        Game.Exit();
                        break;
                }
            }
            menu.Update();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.SpriteBatch.Begin();    //Zacatek vykreslovani

            //Vykresleni pozadi a menicka
            background.Draw(Game1.SpriteBatch);
            menu.Draw(Game1.SpriteBatch);
            base.Draw(gameTime);

            Game1.SpriteBatch.End();  //Konec vykreslovani
        }

        #endregion
    }
}