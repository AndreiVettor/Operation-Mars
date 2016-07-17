using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace coolgame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public const int GAME_WIDTH = 800;
        public const int GAME_HEIGHT = 600;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputManager input;
        CollisionDetector collisionDetector;
        Texture2D bgImage;
        Ground ground;
        Base baseBuilding;
        Tower towerBuilding;

        float deltaTime;

        Enemy1 steve;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GAME_WIDTH;
            graphics.PreferredBackBufferHeight = GAME_HEIGHT;

            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;

            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            input = new InputManager();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Debug.LoadContent(Content);
            bgImage = Content.Load<Texture2D>("background");
            ground = new Ground(Content);
            collisionDetector = new CollisionDetector(ground);
            baseBuilding = new Base(Content, ground.Top);
            towerBuilding = new Tower(Content, ground.Top);
            steve = new Enemy1(Content);
            collisionDetector.AddEnemy(steve);

            steve.X = 800;
            steve.Y = 460;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            Debug.Update(deltaTime);
            input.Update();
            collisionDetector.Update();

            if (input.KeyDown(Keys.Escape))
                Exit();

            if (input.KeyPress(Keys.D))
                Debug.Log("TestD");
            if (input.KeyPress(Keys.F))
                Debug.Log("TestF");

            baseBuilding.Update(gameTime, input, collisionDetector);
            towerBuilding.Update(gameTime, input, collisionDetector);
            steve.Update(gameTime, input, collisionDetector);
            steve.X -= 0.1f * deltaTime;
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            spriteBatch.Draw(bgImage, Vector2.Zero, Color.White);

            towerBuilding.Draw(spriteBatch);
            baseBuilding.Draw(spriteBatch);
            steve.Draw(spriteBatch);
            ground.Draw(spriteBatch);

            Debug.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
