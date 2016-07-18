using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace coolgame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public const int GAME_WIDTH = 800;
        public const int GAME_HEIGHT = 600;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputManager input;
        Texture2D bgImage;
        Ground ground;
        Base baseBuilding;
        Tower towerBuilding;
        

        float deltaTime;

        EnemySpawner enemySpawner1, enemySpawner2;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GAME_WIDTH;
            graphics.PreferredBackBufferHeight = GAME_HEIGHT;

            IsFixedTimeStep = true;
            graphics.SynchronizeWithVerticalRetrace = false;

            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            input = new InputManager();
        }

        protected override void Initialize()
        {
            base.Initialize();

            SoundManager.SetVolume(25);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Debug.LoadContent(Content);
            bgImage = Content.Load<Texture2D>("background");
            ground = new Ground(Content);
            baseBuilding = new Base(Content, ground.Top);
            towerBuilding = new Tower(Content, ground.Top);

            EnemyFactory.LoadContent(Content);
            SoundManager.AddClip(Content.Load<SoundEffect>("machine_gun"), "laser");

            enemySpawner1 = new EnemySpawner(new Vector2(Game.GAME_WIDTH + 50, 460), 1000, Enemy.EnemyDirection.ToLeft);
            enemySpawner2 = new EnemySpawner(new Vector2(-50, 460), 1000, Enemy.EnemyDirection.ToRight);
            
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            Debug.Update(deltaTime);
            input.Update();
            GameManager.UpdateEntities(deltaTime, input);
            CollisionManager.Update();

            if (input.KeyDown(Keys.Escape))
                Exit();

            if (input.KeyPress(Keys.D))
            {
                Debug.Log("Testing very long strings");
                
            }

            if (input.KeyPress(Keys.F))
                Debug.ToggleFPS();
            if (input.KeyPress(Keys.M))
                Debug.ToggleMessages();
            if (input.KeyPress(Keys.R))
                Debug.ToggleRectangles();

            baseBuilding.Update(deltaTime, input);
            towerBuilding.Update(deltaTime, input);
            enemySpawner1.Update(deltaTime);
            enemySpawner2.Update(deltaTime);
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            spriteBatch.Draw(bgImage, Vector2.Zero, Color.White);
            
            baseBuilding.Draw(spriteBatch);
            towerBuilding.Draw(spriteBatch);

            ground.Draw(spriteBatch);

            GameManager.DrawEntities(spriteBatch);

            Debug.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
