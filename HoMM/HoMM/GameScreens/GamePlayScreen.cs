using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HoMM.GameComponents;
using HoMM.CharacterClasses;
using HoMM.TileEngine;

namespace HoMM.GameScreens
{
    public class GamePlayScreen : GameScreen
    {
        #region Fields and Properties

        //Fieldy
        SpriteFont spriteFont;
        ScreenManager manager;
        BackgroundComponent background;
        PathEngine pathengine;

        #endregion

        #region Constructors

        public GamePlayScreen(Game game, ScreenManager screenManager)
            : base(game)
        {
            Content = Game.Content;
            manager = screenManager;
        }

        #endregion

        #region Methods

        protected override void LoadContent()
        {
            //Nacteni fontu, pozadi a inicializace pathenginu
            spriteFont = Content.Load<SpriteFont>(@"Fonts\editorFont");
            background = new BackgroundComponent(GameRef, Content.Load<Texture2D>(@"Backgrounds\gameplayscreen"), DrawMode.Center);
            pathengine = new PathEngine();

            //Nacteni minimapy
            Texture2D minimapSetTexture = Content.Load<Texture2D>(@"Maps\minimapSet");
            Tileset minimapSet = new Tileset(minimapSetTexture, 5, 5, 32, 32);
            Texture2D minimapTexture = Content.Load<Texture2D>(@"Maps\minimap");
            Session.minimap = new Minimap(minimapSet, minimapTexture);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            switch (Session.state)
            {
                //Obsluha kamery a pathenginu v normalnim stavu
                #region Stable state

                case GameState.Stable:
                    {
                        Vector2 motion = new Vector2();

                        if (KeyboardInput.KeyReleased(Keys.Escape))
                        {
                            manager.PushMessage(GameRef.endMessage);
                        } 
                        if (KeyboardInput.KeyReleased(Keys.P))
                        {
                            if (Session.CurrentHero.units.Count != 0)
                                Session.CurrentHero.units[0].HitPoints.Enhance(1000, 1000);
                        }

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

                        pathengine.Update();
                        
                        break;
                    }

                #endregion

                //Obsluha chodiciho stavu
                #region Moving state

                case GameState.Moving:
                    {
                        Session.UpdateParty(gameTime);

                        break;
                    }

                #endregion
            }

            //Update minimapy a mapy
            Session.minimap.Update();
            Session.UpdateMap(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) 
        {
            //Cast posunuta o pozici kamery
            Game1.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, Session.Camera.TransformMatrix);

            //Vykresleni Back a Front mapy
            Session.DrawMap(gameTime, Game1.SpriteBatch); 

            //Vykresleni Hera
            Session.DrawHero(gameTime, Game1.SpriteBatch);

            //Vykresleni jednotek
            foreach (Unit item in Session.Units)
            {
                Rectangle rec = Session.ViewportRect;
                rec.X += (int)Session.Camera.Position.X;
                rec.Y += (int)Session.Camera.Position.Y;
                if (rec.Contains(new Point((int)item.Position.X, (int)item.Position.Y)))
                    item.Draw(gameTime, Game1.SpriteBatch);
            }

            //Vykresleni klicuu
            foreach (GameKey item in Session.Keys)
            {
                Rectangle rec = Session.ViewportRect;
                rec.X += (int)Session.Camera.Position.X;
                rec.Y += (int)Session.Camera.Position.Y;
                if (rec.Contains(new Point((int)item.Position.X, (int)item.Position.Y)))
                item.Draw(gameTime, Game1.SpriteBatch);
            }

            Game1.SpriteBatch.End();


            //Normalni cast neposunuta
            Game1.SpriteBatch.Begin();

            //Vykresleni pozadi a minimapy
            background.Draw(Game1.SpriteBatch);

            Session.minimap.Draw(Game1.SpriteBatch);
            
            base.Draw(gameTime);

            //Vykresleni klickuu a jednotek v hrdinovi
            Session.DrawHeroStuff(gameTime, Game1.SpriteBatch);

            Game1.SpriteBatch.End();
        }
    
        #endregion
    }
}