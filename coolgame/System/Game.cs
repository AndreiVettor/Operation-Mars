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

        UIWindow pauseMenu;
        UIWindow upgradeMenu;

        float deltaTime, totalGameTime;

        EnemySpawner enemySpawner;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GAME_WIDTH;
            graphics.PreferredBackBufferHeight = GAME_HEIGHT;

            GameManager.SetFrameLimiting(this, true);
            GameManager.SetVSync(graphics, false);

            IsMouseVisible = false;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            SoundManager.SoundVolume = 50;
            SoundManager.MusicVolume = 50;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            UIspriteBatch = new SpriteBatch(GraphicsDevice);

            //Utility
            Debug.LoadContent(Content);

            //Base
            GameManager.Background = Content.Load<Texture2D>("background");
            GameManager.Ground = new Ground(Content);
            GameManager.AddEntity(new Base(Content, GameManager.Ground.Top));
            GameManager.AddEntity(new Turret(Content, GameManager.Ground.Top, Enemy.EnemyDirection.ToLeft));
            GameManager.AddEntity(new Turret(Content, GameManager.Ground.Top, Enemy.EnemyDirection.ToRight));
            GameManager.AddEntity(new Forcefield(Content, GameManager.Ground.Top));

            //UI
            UIManager.LoadContent(Content);
            pauseMenu = new PauseMenu(Content);
            upgradeMenu = new UpgradesMenu(Content);
            UIManager.AddElement(pauseMenu);
            UIManager.AddElement(upgradeMenu);


            //Sound
            SoundManager.LoadContent(Content);
            SoundManager.PlaySong("music");


            //Enemies
            EnemyFactory.LoadContent(Content);
            enemySpawner = new EnemySpawner();

            Debug.Log("Content Loaded");
        }

        public void AddButtons()
        {
            //PAUSE MENU
            //menuButton = new Button(
            //   Content, new Vector2(
            //       GAME_WIDTH / 2 - 140 / 2,
            //       GAME_HEIGHT / 2 - 80),
            //   140, 40, "RESUME");
            //menuButton.BackgroundColor = Color.CadetBlue;
            //pauseMenu.AddItem(menuButton);

            //menuButton = new Button(
            //    Content,
            //    new Vector2(
            //        GAME_WIDTH / 2 - 140 / 2,
            //        GAME_HEIGHT / 2 - 60 / 2),
            //    140, 40, "EXIT GAME");
            //menuButton.BackgroundColor = Color.CadetBlue;
            //pauseMenu.AddItem(menuButton);

            //UPGRADE MENU
            //menuButton = new Button(
            //    Content,
            //    new Vector2(
            //        upgradeMenu.Position.X + upgradeMenu.Width - 30 - 10,
            //        upgradeMenu.Position.Y + 10),
            //    30, 30, "X");
            //menuButton.BackgroundColor = Color.CadetBlue;
            //upgradeMenu.AddItem(menuButton);

            //menuButton = new Button(
            //    Content,
            //    new Vector2(
            //        upgradeMenu.Position.X + 20,
            //        upgradeMenu.Position.Y + 20),
            //    250, 30, "Upgrade Lasergun Speed (x2)");
            //menuButton.BackgroundColor = Color.CadetBlue;
            //upgradeMenu.AddItem(menuButton);

            //menuButton = new Button(
            //    Content,
            //    new Vector2(
            //        upgradeMenu.Position.X + 20,
            //        upgradeMenu.Position.Y + 70),
            //    250, 30, "Add Auxiliary Shooting (x2)");
            //menuButton.BackgroundColor = Color.CadetBlue;
            //upgradeMenu.AddItem(menuButton);

            //menuButton = new Button(
            //    Content,
            //    new Vector2(
            //        upgradeMenu.Position.X + 20,
            //        upgradeMenu.Position.Y + 120),
            //    250, 30, "Build Forcefield Generator");
            //menuButton.BackgroundColor = Color.CadetBlue;
            //upgradeMenu.AddItem(menuButton);

            //menuButton = new Button(
            //    Content,
            //    new Vector2(
            //        upgradeMenu.Position.X + 20,
            //        upgradeMenu.Position.Y + 170),
            //    250, 30, "Upgrade Forcefield Regen");
            //menuButton.BackgroundColor = Color.CadetBlue;
            //upgradeMenu.AddItem(menuButton);

            //menuButton = new Button(
            //    Content,
            //    new Vector2(
            //        upgradeMenu.Position.X + 20,
            //        upgradeMenu.Position.Y + 220),
            //    250, 30, "Upgrade Forcefield Strength");
            //menuButton.BackgroundColor = Color.CadetBlue;
            //upgradeMenu.AddItem(menuButton);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (!GameManager.GamePaused)
                totalGameTime += deltaTime;

            UIManager.Update(this, deltaTime);
            Debug.Update(deltaTime);
            InputManager.Update();
            GameManager.UpdateEntities(deltaTime);

            ReadKeyPresses();

            if (!GameManager.GamePaused)
            {
                enemySpawner.Update(totalGameTime, deltaTime);
            }

            base.Update(gameTime);
        }

        public void ReadKeyPresses()
        {
            if (InputManager.KeyPress(Keys.Escape))
            {
                UIManager.TogglePauseMenu();
            }

            if(InputManager.KeyPress(Keys.D))
            {
                if(GameManager.GodMode)
                {
                    GameManager.GodMode = false;
                }
                else
                {
                    GameManager.GodMode = true;
                }
                Debug.Log("God Mode", GameManager.GodMode);
            }

            if(InputManager.KeyPress(Keys.U))
            {
                UIManager.ToggleUpgradeMenu();
            }

            if (InputManager.KeyPress(Keys.C))
            {
                if(GameManager.FrameLimiting)
                {
                    GameManager.SetFrameLimiting(this, false);
                }
                else
                {
                    GameManager.SetFrameLimiting(this, true);
                }
                Debug.Log("Frame Limiting", GameManager.FrameLimiting);
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

            GameManager.DrawEntities(spriteBatch);
            Debug.Draw(spriteBatch);

            spriteBatch.End();

            //UI
            UIspriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, null);

            UIManager.Draw(UIspriteBatch);

            UIspriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
