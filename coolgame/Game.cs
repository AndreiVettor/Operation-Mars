using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using coolgame.Systems;

namespace coolgame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public const int GAME_WIDTH = 1200;
        public const int GAME_HEIGHT = 600;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch UIspriteBatch;

        Texture2D bgImage;
        Ground ground;

        Button resumeButton;
        Button exitButton;

        float deltaTime, totalGameTime;

        EnemySpawner enemySpawner1, enemySpawner2;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GAME_WIDTH;
            graphics.PreferredBackBufferHeight = GAME_HEIGHT;

            IsFixedTimeStep = true;
            GameManager.FrameLimiting = true;
            graphics.SynchronizeWithVerticalRetrace = false;

            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            SoundManager.SoundVolume = 50;
            SoundManager.MusicVolume = 50;

            Debug.Log("Game Initialized");
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            UIspriteBatch = new SpriteBatch(GraphicsDevice);
        
            bgImage = Content.Load<Texture2D>("background");
            ground = new Ground(Content);
            Base baseBuilding = new Base(Content, GameManager.Ground.Top);
            Forcefield test = new Forcefield(Content, GameManager.Ground.Top);

            EnemyFactory.LoadContent(Content);
            Debug.LoadContent(Content);

            resumeButton = new Button(Content, new Vector2(GAME_WIDTH/2 - 140/2, GAME_HEIGHT/2 - 80), 140, 40, "RESUME");
            resumeButton.BackgroundColor = Color.CadetBlue;
            UIManager.AddElement(resumeButton);

            exitButton = new Button(Content, new Vector2(GAME_WIDTH / 2 - 140 / 2, GAME_HEIGHT / 2 - 60 / 2), 140, 40, "EXIT GAME");
            exitButton.BackgroundColor = Color.CadetBlue;
            UIManager.AddElement(exitButton);

            SoundManager.AddSong(Content.Load<Song>("music"), "music");
            SoundManager.AddClip(Content.Load<SoundEffect>("towerlaser"), "enemylaser");
            SoundManager.AddClip(Content.Load<SoundEffect>("towerlaser2"), "laser");
            SoundManager.AddClip(Content.Load<SoundEffect>("crawlerhit"), "crawlerhit");
            SoundManager.AddClip(Content.Load<SoundEffect>("steelroachhit"), "steelroachhit");
            SoundManager.AddClip(Content.Load<SoundEffect>("steelroachattack"), "steelroachattack");

            SoundManager.PlaySong("music");

            enemySpawner1 = new EnemySpawner(new Vector2(Game.GAME_WIDTH + 50, GameManager.Ground.Top), Enemy.EnemyDirection.ToLeft);
            enemySpawner2 = new EnemySpawner(new Vector2(-50, GameManager.Ground.Top), Enemy.EnemyDirection.ToRight);
            
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (!GameManager.GamePaused)
                totalGameTime += deltaTime;

            Debug.Update(deltaTime);
            InputManager.Update();
            GameManager.UpdateEntities(deltaTime);
            UIManager.Update(this);

            ReadKeyPresses();

            enemySpawner1.Update(totalGameTime, deltaTime);
            enemySpawner2.Update(totalGameTime, deltaTime);
           
            base.Update(gameTime);
        }

        public void ReadKeyPresses()
        {
            if (InputManager.KeyPress(Keys.Escape))
            {
                UIManager.ToggleMenu();
            }

                if (InputManager.KeyPress(Keys.C))
            {
                GameManager.ToggleFrameLimiting(this);
                Debug.Log("Toggled Frame Limiting");
            }

            if (InputManager.KeyPress(Keys.P))
            {
                GameManager.GamePaused = !GameManager.GamePaused;
                Debug.Log("Toggled Game Pause");
            }

            if (InputManager.KeyPress(Keys.F))
            {
                Debug.ToggleFPS();
                Debug.Log("Toggled FPS");
            }
            if (InputManager.KeyPress(Keys.M))
            {
                SoundManager.ToggleMute();
                Debug.Log("Toggled Mute");
            }

            if (InputManager.KeyPress(Keys.R))
            {
                Debug.ToggleRectangles();
                Debug.Log("Toggled Collision Boxes");
            }
            if (InputManager.KeyPress(Keys.L))
            {
                Debug.ToggleDebugLog();
                Debug.Log("Toggled Debug Log");
            }
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null);

            spriteBatch.Draw(bgImage,
                new Rectangle(0, 0, GAME_WIDTH, GAME_HEIGHT),
                null,
                Color.White,
                0,
                Vector2.Zero,
                SpriteEffects.None,
                LayerManager.GetLayerDepth(Layer.Background));

            GameManager.DrawEntities(spriteBatch);
            Debug.Draw(spriteBatch);

            spriteBatch.End();

            //UI
            UIspriteBatch.Begin();

            UIManager.Draw(UIspriteBatch);

            UIspriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
