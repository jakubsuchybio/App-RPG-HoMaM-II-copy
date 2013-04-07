using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HoMM.Controls;
using HoMM.CharacterClasses;
using HoMM.GameComponents;
using HoMM.TileEngine;

namespace HoMM.GameScreens
{

    public class CharacterGeneratorScreen : GameScreen
    {
        #region Fields and Properties

        //Fieldy
        ScreenManager manager;
        ControlManager Controls;
        BackgroundComponent background;
        Texture2D malePicture;
        Texture2D femalePicture;
        PictureBox picture;
        LeftRightSelector genderSelector;
        string[] genderItems = { "Male", "Female" };

        #endregion

        #region Constructors

        public CharacterGeneratorScreen(Game game, ScreenManager screenManager)
            : base(game)
        {
            Content = Game.Content;
            manager = screenManager;
            Controls = new ControlManager();
        }

        #endregion

        #region Methods

        protected override void LoadContent()
        {

            //Vytvoreni pozadi
            background = new BackgroundComponent(GameRef, Content.Load<Texture2D>(@"Backgrounds\characterscreen"), DrawMode.Center);

            //Nahrani textur pro sipky a font
            SpriteFont spriteFont = Content.Load<SpriteFont>(@"Fonts\startFont");
            Texture2D leftArrow = Content.Load<Texture2D>(@"Controls\leftarrow");
            Texture2D rightArrow = Content.Load<Texture2D>(@"Controls\rightarrow");

            //Nacteni obrazkuu pohlavi
            malePicture = Content.Load<Texture2D>(@"Pictures\male");
            femalePicture = Content.Load<Texture2D>(@"Pictures\female");

            //Inicializace pictureboxu
            picture = new PictureBox(malePicture, new Rectangle((GameRef.Window.ClientBounds.Width - 300) / 2, 150, 300, 300));

            //Vyvoreni leftrightselectoru pro pohlavi
            genderSelector = new LeftRightSelector(
            spriteFont,
            leftArrow,
            rightArrow);
            genderSelector.SetItems(genderItems, 350);
            genderSelector.Position = new Vector2((GameRef.Window.ClientBounds.Width - genderSelector.Size.X) / 2, 500);
            genderSelector.HasFocus = true;
            //genderSelector.Size;
            Controls.Add(genderSelector);

            //Vytvoreni tlacitka pro potvrzeni vyberu
            LinkLabel linkLabel1 = new LinkLabel(spriteFont);
            linkLabel1.Text = "Vyber";
            linkLabel1.Size = linkLabel1.SpriteFont.MeasureString(linkLabel1.Text);
            linkLabel1.Position = new Vector2((GameRef.Window.ClientBounds.Width - linkLabel1.Size.X) / 2, 650);
            Controls.Add(linkLabel1);

            //Pridani eventu pro vyber
            linkLabel1.Selected += new EventHandler(linkLabel1_Selected);
            Controls.selectedControl = 0;

            base.LoadContent();
        }

        /// <summary>
        /// Obsluha tlacitka potvrzujici vstup do hry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void linkLabel1_Selected(object sender, EventArgs e)
        {
            //Nacteni vsech veci okolo mapy, hrdiny a inicializace map
            Session.LoadMapAndTextures("map.xml");
            Session.LoadHeroSprite(genderSelector.SelectedItem);
            Session.Camera = new Camera(new Vector2(0,0));

            //Nastaveni jednotek do tilemapy
            foreach (Unit item in Session.Units)
            {
                Session.FrontMap.GetTile(item.Cell).obj = item;
            }

            //Nastaveni klicu do tilemapy
            foreach (GameKey item in Session.Keys)
            {
                Session.FrontMap.GetTile(item.Cell).obj = item;
            }

            //Nastaveni vrstvy mapy vyuzivajici pathengine pro vykresleni oznamovaci cesty
            MapLayer frontPath = new MapLayer(Session.MapProps.FrontSizeX, Session.MapProps.FrontSizeY);
            for (int y = 0; y < frontPath.Height; y++)
            {
                for (int x = 0; x < frontPath.Width; x++)
                {
                    Tile tile = new Tile(-1, -1, y, x);
                    frontPath.SetTile(x, y, tile);
                }
            }
            Session.FrontMap.mapLayers.Add(frontPath);

            //Vytvoreni noveho hrdiny
            CreateNewHero();

            //Pridani dat pro ulozeni
            Session.CurrentHero.Gender = genderSelector.SelectedItem;
            if(Session.CurrentHero.Gender == "Female")
                Session.CurrentHero.Sprite.texture = Content.Load<Texture2D>(@"Sprites\femalefighter");

            //Autosave
            Session.SaveGame("AutoSave.xml");

            //Vraceni na hlavni menu a okamzity prehozeni na herni okno
            manager.PopScreen();
            manager.PushScreen(GameRef.GamePlayScreen);
        }

        /// <summary>
        /// Vytvoreni noveho hrdiny, nastaveni pozice.
        /// </summary>
        private void CreateNewHero()
        {
            Session.CurrentHero = new Hero(0, 0, Session.playerSpriteHero);
            Session.CurrentHero.Position = new Vector2(0, 0);
            Session.CurrentHero.Cell = new Point(0, 0);
            Session.CurrentHero.Sprite.Position = Session.CurrentHero.Position;
        }

        public override void Update(GameTime gameTime)
        {
            //Update ovladacich prvkuu
            Controls.Update(gameTime);

            //Nastaveni obrazku pohlavi
            if (genderSelector.SelectedItem == genderSelector.Items[0])
                picture.Image = malePicture;
            else
                picture.Image = femalePicture;

            //Kontrola stisku Esc pro vraceni na predchozi obrazovku (menu)
            if (KeyboardInput.KeyPressed(Keys.Escape))
            {
                KeyboardInput.Flush();
                manager.PopScreen();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.SpriteBatch.Begin();

            //Vykresleni vsech ovladacich prvku a pozadi
            background.Draw(Game1.SpriteBatch);
            picture.Draw(Game1.SpriteBatch);
            Controls.Draw(Game1.SpriteBatch);
            base.Draw(gameTime);

            Game1.SpriteBatch.End();
        }

        #endregion
    }
}