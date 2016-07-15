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
        Texture2D bgImage;
        Ground ground;
        Base baseBuilding;
        Tower towerBuilding;

        Enemy1 steve;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GAME_WIDTH;
            graphics.PreferredBackBufferHeight = GAME_HEIGHT;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bgImage = Content.Load<Texture2D>("background");
            ground = new Ground(Content);
            baseBuilding = new Base(Content, ground.Top);
            towerBuilding = new Tower(Content, ground.Top);
            steve = new Enemy1(Content);

            steve.X = 800;
            steve.Y = 460;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            baseBuilding.Update(gameTime);
            towerBuilding.Update(gameTime);
            steve.Update(gameTime);
            steve.X -= 1;
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            spriteBatch.Draw(bgImage, Vector2.Zero, Color.White);
            ground.Draw(spriteBatch);

            towerBuilding.Draw(spriteBatch);
            baseBuilding.Draw(spriteBatch);
            steve.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
