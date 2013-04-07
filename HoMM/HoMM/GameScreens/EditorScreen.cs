using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HoMM.Controls;
using HoMM.GameComponents;
using HoMM.CharacterClasses;
using HoMM.TileEngine;

namespace HoMM.GameScreens
{
    /// <summary>
    /// Enum pro urceni aktualni akce editoru
    /// </summary>
    public enum Action
    {
        Add,
        Delete,
        Move,
        Pick
    }

    public class EditorScreen : GameScreen
    {
        #region Fields and Properties

        //Fieldy
        SpriteFont spriteFont;
        SpriteFont font;
        MenuComponent LayerMenu;
        MenuComponent ActionMenu;
        MenuComponent TileSetMenu;
        MenuComponent CharacterMenu;
        MenuComponent KeyMenu;
        ScreenManager manager;
        BackgroundComponent background;
        MapLayer CurrentLayer;
        TileMap CurrentMap;
        ControlManager Controls;
        Action action;
        Tile toMove;
        Tile CurrentTile;
        Tileset CurrentTileSet;
        Texture2D ramTexture;
        Rectangle ram;
        Rectangle[] sourceRectangles;
        Label label1;
        TimeSpan end;
        bool lvisible;

        #endregion

        #region Constructors

        public EditorScreen(Game game, ScreenManager screenManager)
            : base(game)
        {
            Content = Game.Content;
            manager = screenManager;
            Controls = new ControlManager();
            toMove = null;
        }

        #endregion

        #region Methods

        protected override void LoadContent()
        {
            ram = new Rectangle(770, 250, 220, 220);

            //Nacteni fontu, ramecku pro tileset a pozadi
            font = Content.Load<SpriteFont>(@"Fonts\editorFont");
            ramTexture = Content.Load<Texture2D>(@"Pictures\ram");
            background = new BackgroundComponent(GameRef, Content.Load<Texture2D>(@"Backgrounds\editorscreen"), DrawMode.Center);

            #region ovladaci prvky

            spriteFont = Content.Load<SpriteFont>(@"Fonts\menuFont");
            string[] LayermenuItems = { "Back Map", "Back Splatter", "Front Map", "Front Splatter" };
            LayerMenu = new MenuComponent(font, LayermenuItems, false);
            LayerMenu.SetPostion(new Vector2(770, 30));
            LayerMenu.SelectedColor = Color.Yellow;
            LayerMenu.HiliteColor = Color.White;

            spriteFont = Content.Load<SpriteFont>(@"Fonts\menuFont");
            string[] ActionmenuItems = { "Add", "Move", "Pick", "Delete" };
            ActionMenu = new MenuComponent(font, ActionmenuItems, false);
            ActionMenu.SetPostion(new Vector2(900, 30));
            ActionMenu.SelectedColor = Color.Yellow;
            ActionMenu.HiliteColor = Color.White;

            spriteFont = Content.Load<SpriteFont>(@"Fonts\menuFont");

            LinkLabel linkLabel1 = new LinkLabel(spriteFont);
            linkLabel1.Text = "Save";
            linkLabel1.Size = linkLabel1.SpriteFont.MeasureString(linkLabel1.Text);
            linkLabel1.Position = new Vector2(770,720);
            linkLabel1.Selected += new EventHandler(linkLabel1_Selected);
            Controls.Add(linkLabel1); 
            
            LinkLabel linkLabel2 = new LinkLabel(spriteFont);
            linkLabel2.Text = "Load";
            linkLabel2.Size = linkLabel2.SpriteFont.MeasureString(linkLabel2.Text);
            linkLabel2.Position = new Vector2(940,720);
            linkLabel2.Selected += new EventHandler(linkLabel2_Selected);
            Controls.Add(linkLabel2);
            Controls.editor = true;

            label1 = new Label(spriteFont);
            label1.Position = new Vector2(845, 720);
            label1.Visible = false;
            Controls.Add(label1);

            Session.LoadMapAndTextures("map.xml");
            
            string[] CharacterStrings = new string[Session.baseUnits.Keys.Count];
            Session.baseUnits.Keys.CopyTo(CharacterStrings, 0);

            CharacterMenu = new MenuComponent(font, CharacterStrings, false);
            CharacterMenu.SetPostion(new Vector2(770, 200));
            CharacterMenu.SelectedColor = Color.Yellow;
            CharacterMenu.HiliteColor = Color.White;

            int[] KeyInts = new int[Session.baseKeys.Keys.Count];
            Session.baseKeys.Keys.CopyTo(KeyInts, 0);
            string[] KeyStrings = new string[Session.baseKeys.Keys.Count];
            for (int i = 0; i < KeyInts.Length; i++)
            {
                KeyStrings[i] = KeyInts[i].ToString();
            }

            KeyMenu = new MenuComponent(font, KeyStrings, false);
            KeyMenu.SetPostion(new Vector2(770, 200));
            KeyMenu.SelectedColor = Color.Yellow;
            KeyMenu.HiliteColor = Color.White;


            string[] TileSetmenuItems = new string[Session.FrontMap.tilesets.Count + 2];
            for (int i = 0; i < Session.FrontMap.tilesets.Count; i++)
            {
                TileSetmenuItems[i] = Session.FrontMap.tilesets[i].Texture.Name;  //files[i].Name.Substring(0, files[i].Name.Length - 4);
            }
            TileSetmenuItems[TileSetmenuItems.Length - 2] = "Characters";
            TileSetmenuItems[TileSetmenuItems.Length - 1] = "Keys";

            TileSetMenu = new MenuComponent(font, TileSetmenuItems, false);
            TileSetMenu.SetPostion(new Vector2(770, 100));
            TileSetMenu.SelectedColor = Color.Yellow;
            TileSetMenu.HiliteColor = Color.White;

            #endregion
            
            //Nastaveni prvniho itemu u LayerMenu
            switch (LayerMenu.clickedIndex)
            {
                case 0:
                    CurrentMap = Session.BackMap;
                    CurrentLayer = Session.BackMap.mapLayers[0];
                    break;
                case 1:
                    CurrentMap = Session.BackMap;
                    CurrentLayer = Session.BackMap.mapLayers[1];
                    break;
                case 2:
                    CurrentMap = Session.FrontMap;
                    CurrentLayer = Session.FrontMap.mapLayers[0];
                    break;
                case 3:
                    CurrentMap = Session.FrontMap;
                    CurrentLayer = Session.FrontMap.mapLayers[1];
                    break;
            }

            //Nastaveni bazoveho policka pro akce
            CurrentTile = new Tile(0, 0, -1, -1);
            CurrentTileSet = Session.FrontMap.tilesets[CurrentTile.Tileset];

            //Nacteni vstupnich ramecku pro obraz TileSetu
            int tiles = CurrentTileSet.TilesWide * CurrentTileSet.TilesHigh;
            sourceRectangles = new Rectangle[tiles];
            float Scale = (float)ram.Width / (float)Session.FrontMap.tilesets[CurrentTile.Tileset].Texture.Width;
            int tile = 0;
            for (int y = 0; y < CurrentTileSet.TilesHigh; y++)
                for (int x = 0; x < CurrentTileSet.TilesWide; x++)
                {
                    sourceRectangles[tile] = new Rectangle(
                    (int)(x * CurrentTileSet.TileWidth * Scale),
                    (int)(y * CurrentTileSet.TileWidth * Scale),
                    (int)(CurrentTileSet.TileWidth * Scale),
                    (int)(CurrentTileSet.TileHeight * Scale));
                    tile++;
                }

            base.LoadContent();
        }

        /// <summary>
        /// Metoda pro stlaceni tlacitka Save.
        /// Ulozi mapu a na chvili oznami ze je mapa Savnuta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void linkLabel1_Selected(object sender, EventArgs e)
        {
            Session.SaveMap();

            lvisible = true;
            label1.Text = "Saved";
            label1.Visible = true;
        }

        /// <summary>
        /// Metoda pro stlaceni tlacitka Load.
        /// Nacte mapu a na chvili oznami ze je mapa nactena
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void linkLabel2_Selected(object sender, EventArgs e)
        {
            Session.LoadMapAndTextures("map.xml");

            switch (LayerMenu.clickedIndex)
            {
                case 0:
                    CurrentMap = Session.BackMap;
                    CurrentLayer = Session.BackMap.mapLayers[0];
                    break;
                case 1:
                    CurrentMap = Session.BackMap;
                    CurrentLayer = Session.BackMap.mapLayers[1];
                    break;
                case 2:
                    CurrentMap = Session.FrontMap;
                    CurrentLayer = Session.FrontMap.mapLayers[0];
                    break;
                case 3:
                    CurrentMap = Session.FrontMap;
                    CurrentLayer = Session.FrontMap.mapLayers[1];
                    break;
            }

            lvisible = true;
            label1.Text = "Loaded";
            label1.Visible = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (KeyboardInput.KeyReleased(Keys.Escape))
                manager.PopScreen();

            //Oznamovaci label
            #region Time label

            if (lvisible)
            {
                lvisible = false;
                end = gameTime.TotalGameTime + TimeSpan.FromSeconds(1);
            }
            if (end.TotalMilliseconds < gameTime.TotalGameTime.TotalMilliseconds)
            {
                label1.Visible = false;
            }

            #endregion

            //Osetreni pohybu po mape klavesnici
            #region Posun + camera

            Vector2 motion = new Vector2();

            if (KeyboardInput.KeyDown(Keys.Up))
                motion.Y = -1;
            if (KeyboardInput.KeyDown(Keys.Down))
                motion.Y = 1;
            if (KeyboardInput.KeyDown(Keys.Left))
                motion.X = -1;
            if (KeyboardInput.KeyDown(Keys.Right))
                motion.X = 1;
            
            Session.Camera.Position += motion * Session.Camera.Speed;
            Session.Camera.LockCamera();

            #endregion

            //Osetreni vsech menicek jejich aktualni hodnoty a nasledne vykresleni tilesetu
            #region Tlacitka + menicka

            if (LayerMenu.isMouseOver() && MouseInput.LeftButtonReleased())
            {
                switch (LayerMenu.SelectedIndex)
                {
                    case 0:
                        LayerMenu.clickedIndex = 0;
                        CurrentMap = Session.BackMap;
                        CurrentLayer = Session.BackMap.mapLayers[0];
                        break;
                    case 1:
                        LayerMenu.clickedIndex = 1;
                        CurrentMap = Session.BackMap;
                        CurrentLayer = Session.BackMap.mapLayers[1];
                        break;
                    case 2:
                        LayerMenu.clickedIndex = 2;
                        CurrentMap = Session.FrontMap;
                        CurrentLayer = Session.FrontMap.mapLayers[0];
                        break;
                    case 3:
                        LayerMenu.clickedIndex = 3;
                        CurrentMap = Session.FrontMap;
                        CurrentLayer = Session.FrontMap.mapLayers[1];
                        break;
                }
            }
            LayerMenu.Update();

            if (ActionMenu.isMouseOver() && MouseInput.LeftButtonReleased() && toMove == null)
            {
                switch (ActionMenu.SelectedIndex)
                {
                    case 0:
                        ActionMenu.clickedIndex = 0;
                        action = Action.Add;
                        break;
                    case 1:
                        ActionMenu.clickedIndex = 1;
                        action = Action.Move;
                        break;
                    case 2:
                        ActionMenu.clickedIndex = 2;
                        action = Action.Pick;
                        break;
                    case 3:
                        ActionMenu.clickedIndex = 3;
                        action = Action.Delete;
                        break;
                }
            }
            ActionMenu.Update();

            if(TileSetMenu.clickedIndex == 3)
            {
                if (CharacterMenu.isMouseOver() && MouseInput.LeftButtonReleased())
                {
                    CharacterMenu.clickedIndex = CharacterMenu.SelectedIndex;
                }
                CharacterMenu.Update();
            }
            if (TileSetMenu.clickedIndex == 4)
            {
                if (KeyMenu.isMouseOver() && MouseInput.LeftButtonReleased())
                {
                    KeyMenu.clickedIndex = KeyMenu.SelectedIndex;
                }
                KeyMenu.Update();
            }

            if (TileSetMenu.isMouseOver() && MouseInput.LeftButtonReleased())
            {
                TileSetMenu.clickedIndex = TileSetMenu.SelectedIndex;
                if (TileSetMenu.clickedIndex < 3)
                {
                    CurrentTile.TileIndex = 0;
                    CurrentTile.Tileset = TileSetMenu.clickedIndex;
                    CurrentTileSet = Session.FrontMap.tilesets[CurrentTile.Tileset];

                    int tiles = CurrentTileSet.TilesWide * CurrentTileSet.TilesHigh;
                    sourceRectangles = new Rectangle[tiles];
                    float Scale = (float)ram.Width / (float)Session.FrontMap.tilesets[CurrentTile.Tileset].Texture.Width;
                    int tile = 0;
                    for (int y = 0; y < CurrentTileSet.TilesHigh; y++)
                        for (int x = 0; x < CurrentTileSet.TilesWide; x++)
                        {
                            sourceRectangles[tile] = new Rectangle(
                            (int)(x * CurrentTileSet.TileWidth * Scale),
                            (int)(y * CurrentTileSet.TileWidth * Scale),
                            (int)(CurrentTileSet.TileWidth * Scale),
                            (int)(CurrentTileSet.TileHeight * Scale));
                            tile++;
                        }
                }
            }
            TileSetMenu.Update();
            Controls.Update(gameTime);

            #endregion

            //Osetreni akci provedenych do mapy
            #region Actions

            switch (action)
            {
                case Action.Delete:
                    {
                        if (MouseInput.MouseOver(Session.ViewportRect.X, Session.ViewportRect.Y, Session.ViewportRect.Width, Session.ViewportRect.Height) && MouseInput.MouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (TileSetMenu.clickedIndex < 3)
                            {
                                Tile tmp = CurrentLayer.GetTile(CurrentMap.MouseToPoint());
                                CurrentLayer.SetTile(tmp.X, tmp.Y, -1, -1, true);
                            }
                            else if (TileSetMenu.clickedIndex == 3 && MouseInput.LeftButtonPressed())
                            {
                                Tile tmp = Session.FrontMap.GetTile(Session.FrontMap.MouseToPoint());
                                Unit u = (Unit)tmp.obj;
                                Session.Units.Remove(u);
                                tmp.obj = null;
                            }
                            else if (TileSetMenu.clickedIndex == 4 && MouseInput.LeftButtonPressed())
                            {
                                Tile tmp = Session.FrontMap.GetTile(Session.FrontMap.MouseToPoint());
                                GameKey u = (GameKey)tmp.obj;
                                Session.Keys.Remove(u);
                                tmp.obj = null;
                            }
                        }
                        
                        break;
                    }
                case Action.Add:
                    {
                        if (MouseInput.MouseOver(Session.ViewportRect.X, Session.ViewportRect.Y, Session.ViewportRect.Width, Session.ViewportRect.Height) && MouseInput.MouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (TileSetMenu.clickedIndex < 3)
                            {
                                Tile tmp = CurrentLayer.GetTile(CurrentMap.MouseToPoint());
                                if (CurrentTile.Tileset == 1)
                                    CurrentLayer.SetTile(tmp.X, tmp.Y, CurrentTile.TileIndex, CurrentTile.Tileset, false);
                                else
                                    CurrentLayer.SetTile(tmp.X, tmp.Y, CurrentTile.TileIndex, CurrentTile.Tileset, true);
                            }
                            else if (TileSetMenu.clickedIndex == 3 && MouseInput.LeftButtonPressed())
                            { 
                                Tile tmp = Session.FrontMap.GetTile(Session.FrontMap.MouseToPoint());
                                Session.Units.Add(Session.baseUnits[CharacterMenu.menuItems[CharacterMenu.clickedIndex]].Clone(Notoriety.Enemy, tmp.X, tmp.Y));
                            }
                            else if (TileSetMenu.clickedIndex == 4 && MouseInput.LeftButtonPressed())
                            {
                                Tile tmp = Session.FrontMap.GetTile(Session.FrontMap.MouseToPoint());
                                Session.Keys.Add(Session.baseKeys[KeyMenu.clickedIndex + 1].Clone(tmp.X, tmp.Y));
                            }
                        }
                        break;
                    }
                case Action.Pick:
                    {
                        if (MouseInput.MouseOver(Session.ViewportRect.X, Session.ViewportRect.Y, Session.ViewportRect.Width, Session.ViewportRect.Height) && MouseInput.MouseState.LeftButton == ButtonState.Pressed)
                        {
                            Tile tmp = CurrentLayer.GetTile(CurrentMap.MouseToPoint());
                            CurrentTile = new Tile(tmp.TileIndex, tmp.Tileset, -1, -1);
                            TileSetMenu.clickedIndex = tmp.Tileset;
                        }
                        break;
                    }
                case Action.Move:
                    {
                        if (MouseInput.MouseOver(Session.ViewportRect.X, Session.ViewportRect.Y, Session.ViewportRect.Width, Session.ViewportRect.Height) && MouseInput.LeftButtonPressed())
                        {
                            if (toMove == null)
                            {
                                toMove = CurrentLayer.GetTile(CurrentMap.MouseToPoint());
                            }
                            else
                            {
                                Tile tmp = CurrentLayer.GetTile(CurrentMap.MouseToPoint());
                                if (CurrentTile.Tileset == 1)
                                    CurrentLayer.SetTile(tmp.X, tmp.Y, toMove.TileIndex, toMove.Tileset, false);
                                else
                                    CurrentLayer.SetTile(tmp.X, tmp.Y, CurrentTile.TileIndex, CurrentTile.Tileset, true);
                                CurrentLayer.SetTile(toMove.X, toMove.Y, -1, -1, true);
                                toMove = null;
                            }
                        }
                        break;
                    }
            }

            #endregion

            //Osetreni kliku do tilesetu a nastaveni bazoveho policka pro upravu
            #region Get CurrentTile
            if (MouseInput.MouseOver(ram.X, ram.Y, ram.Width, ram.Height))
            {
                if (MouseInput.LeftButtonReleased())
                {
                    Point p = new Point(MouseInput.MouseState.X, MouseInput.MouseState.Y);
                    p.X -= ram.X;
                    p.Y -= ram.Y;
                    int tiles = CurrentTileSet.TilesWide * CurrentTileSet.TilesHigh;
                    for (int i = 0; i < tiles; i++)
                    {
                        if (sourceRectangles[i].Contains(p))
                        {
                            CurrentTile.TileIndex = i;
                            break;
                        }
                    }
                }
            }

            #endregion

            //Update vsech jednotek a klicu
            Session.UpdateMap(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) 
        {
            //Cast posunuta o pozici kamery
            Game1.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, Session.Camera.TransformMatrix);

            //Vykresleni Back a Front mapy
            Session.DrawMap(gameTime, Game1.SpriteBatch);

            //Vykresleni jednotek
            foreach (Unit item in Session.Units)
            {
                Rectangle rec = Session.ViewportRect;
                rec.X += (int)Session.Camera.Position.X;
                rec.Y += (int)Session.Camera.Position.Y;
                if (rec.Contains(new Point((int)item.Position.X, (int)item.Position.Y)))
                    item.Draw(gameTime, Game1.SpriteBatch);
            }

            //Vykresleni klicu
            foreach (GameKey item in Session.Keys)
            {
                Rectangle rec = Session.ViewportRect;
                rec.X += (int)Session.Camera.Position.X;
                rec.Y += (int)Session.Camera.Position.Y;
                if (rec.Contains(new Point((int)item.Position.X, (int)item.Position.Y)))
                    item.Draw(gameTime, Game1.SpriteBatch);
            }

            Game1.SpriteBatch.End();


            //Normalni vykreslovaci cast neposunuta
            Game1.SpriteBatch.Begin();

            //Vykresleni vsech ovladacich prvku
            background.Draw(Game1.SpriteBatch);
            LayerMenu.Draw(Game1.SpriteBatch);
            ActionMenu.Draw(Game1.SpriteBatch);
            TileSetMenu.Draw(Game1.SpriteBatch);
            Controls.Draw(Game1.SpriteBatch);
            
            //Vykresleni oznamovacich prvku jako je tileset a bazove policko nebo listu charakteru nebo listu klicu
            Game1.SpriteBatch.DrawString(font, 
                CurrentMap.MouseToPoint().ToString() +
                MouseInput.ToStringg(),
                new Vector2(770, 700), Color.Red);
            if (CurrentTile != null && TileSetMenu.clickedIndex < 3)
            {
                Game1.SpriteBatch.DrawString(font, "CurrTile " + CurrentTile.ToString(), new Vector2(870, 95), Color.Red);
                if (CurrentTile.TileIndex != -1)
                    Game1.SpriteBatch.Draw(CurrentMap.tilesets[TileSetMenu.clickedIndex].Texture, new Rectangle(870, 120, 32, 32), CurrentMap.tilesets[TileSetMenu.clickedIndex].SourceRectangles[CurrentTile.TileIndex], Color.White);
                
                Game1.SpriteBatch.Draw(CurrentMap.tilesets[TileSetMenu.clickedIndex].Texture, ram, Color.White);
                Game1.SpriteBatch.Draw(ramTexture, ram, Color.White);
            }
            else if (TileSetMenu.clickedIndex == 3)
            {
                CharacterMenu.Draw(Game1.SpriteBatch);
            }
            else if (TileSetMenu.clickedIndex == 4)
            {
                KeyMenu.Draw(Game1.SpriteBatch);
            }
            if (toMove != null)
            {
                Game1.SpriteBatch.DrawString(font, "toMove " + toMove.ToString(), new Vector2(870, 155), Color.Red);
                if (toMove.TileIndex != -1 && toMove.Tileset != -1)
                    Game1.SpriteBatch.Draw(CurrentMap.tilesets[toMove.Tileset].Texture, new Rectangle(870, 180, 32, 32), CurrentMap.tilesets[toMove.Tileset].SourceRectangles[toMove.TileIndex], Color.White);
            }

            base.Draw(gameTime);



            Game1.SpriteBatch.End();
        }
    
        #endregion
    }
}