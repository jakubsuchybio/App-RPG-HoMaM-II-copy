using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HoMM.GameComponents;
using HoMM.GameScreens;

namespace HoMM
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Fields and Properties
        
        //Fieldy
        public StartScreen StartScreen;
        public CharacterGeneratorScreen CharacterGenerator;
        public LoadScreen LoadScreen;
        public GamePlayScreen GamePlayScreen;
        public EditorScreen EditorScreen;
        public endMessage endMessage;
        public TheEndMessage TheEndMessage;
        public MessageScreen MessageScreen;
        public GameOverMessage GameOverMessage;

        public ScreenManager screenManager;
        public Session Session = Session.Instance;
        GraphicsDeviceManager graphics;
        public static SpriteBatch SpriteBatch;

        #endregion

        #region Constructors

        public Game1()
        {
            //Nacteni grafiky
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Nastaveni velikosti okna
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
            
            //Inicializace Session
            Session.Initialize(this);

            //Nacteni komponent pro obsluhu vstupu
            Components.Add(new KeyboardInput(this));
            Components.Add(new MouseInput(this));

            //Inicializace managera
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);

            //Vytvoreni vsech screenuu
            StartScreen = new StartScreen(this, screenManager);
            CharacterGenerator = new CharacterGeneratorScreen(this, screenManager);
            GamePlayScreen = new GamePlayScreen(this, screenManager);
            LoadScreen = new LoadScreen(this, screenManager);
            EditorScreen = new EditorScreen(this, screenManager);
            endMessage = new endMessage(this, screenManager);
            TheEndMessage = new TheEndMessage(this, screenManager);
            MessageScreen = new MessageScreen(this, screenManager);
            GameOverMessage = new GameOverMessage(this, screenManager);

            //Vyvolani StartScreenu
            screenManager.ChangeScreens(StartScreen);
        }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// Nacteni veskereho obsahu hry. Napr: Obrazky, Fonty, Zvuky, atd.
        /// </summary>
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Zahozeni veskereho obsahu.
        /// Vola se pri konci hry.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Vola se kazdy frame.
        /// Slouzi pro logiku hry
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Vola se kazdy frame
        /// Slouzi pro vykresleni objektu na obrazovku.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);
            base.Draw(gameTime);
        }

        #endregion
    }
}
