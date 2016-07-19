using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace coolgame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public const int GAME_WIDTH = 1200;
        public const int GAME_HEIGHT = 600;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputManager input;
        Texture2D bgImage;
        Ground ground;
        Base baseBuilding;

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
            input = new InputManager();
        }

        protected override void Initialize()
        {
            base.Initialize();

            SoundManager.SoundVolume = 50;
            SoundManager.MusicVolume = 15;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Debug.LoadContent(Content);
            bgImage = Content.Load<Texture2D>("background");
            ground = new Ground(Content);
            baseBuilding = new Base(Content, ground.Top);

            EnemyFactory.LoadContent(Content);
            SoundManager.AddClip(Content.Load<SoundEffect>("machine_gun"), "laser");
            SoundManager.AddSong(Content.Load<Song>("music"), "music");

            SoundManager.PlaySong("music");

            int seed = System.DateTime.Now.Year +
                System.DateTime.Now.Month +
                System.DateTime.Now.Day +
                System.DateTime.Now.Hour +
                System.DateTime.Now.Minute +
                System.DateTime.Now.Second +
                System.DateTime.Now.Millisecond;

            enemySpawner1 = new EnemySpawner(seed, new Vector2(Game.GAME_WIDTH + 50, ground.Top), Enemy.EnemyDirection.ToLeft);
            enemySpawner2 = new EnemySpawner(seed + 1337, new Vector2(-50, ground.Top), Enemy.EnemyDirection.ToRight);
            
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            totalGameTime = (float)gameTime.TotalGameTime.TotalMilliseconds;

            Debug.Update(deltaTime);
            input.Update();
            GameManager.UpdateEntities(deltaTime, input);
            CollisionManager.Update();

            if (input.KeyDown(Keys.Escape))
                Exit();

            if (input.KeyPress(Keys.C))
            {
                GameManager.ToggleFrameLimiting(this);
                Debug.Log("Toggled Frame Limiting");
            }

            if (input.KeyPress(Keys.F))
            {
                Debug.ToggleFPS();
                Debug.Log("Toggled FPS");
            }
            if (input.KeyPress(Keys.M))
            {
                SoundManager.ToggleMute();
                Debug.Log("Toggled Mute");
            }

            if (input.KeyPress(Keys.R))
            {
                Debug.ToggleRectangles();
                Debug.Log("Toggled Collision Boxes");
            }
            if (input.KeyPress(Keys.L))
            {
                Debug.ToggleDebugLog();
                Debug.Log("Toggled Debug Log");
            }

            baseBuilding.Update(deltaTime, input);
            enemySpawner1.Update(totalGameTime, deltaTime);
            enemySpawner2.Update(totalGameTime, deltaTime);
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, null);

            spriteBatch.Draw(bgImage,
                new Rectangle(0, 0, GAME_WIDTH, GAME_HEIGHT),
                null,
                Color.White,
                0,
                Vector2.Zero,
                SpriteEffects.None,
                LayerManager.GetLayerDepth(Layer.Background));
            
            baseBuilding.Draw(spriteBatch);

            ground.Draw(spriteBatch);

            GameManager.DrawEntities(spriteBatch);

            Debug.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
