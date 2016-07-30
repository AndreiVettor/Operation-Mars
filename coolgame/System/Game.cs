using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using coolgame.GUI;
using coolgame.GUI.Menus;

namespace coolgame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public const int GAME_WIDTH = 1200;
        public const int GAME_HEIGHT = 600;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch UIspriteBatch;

        GUIManager guiManager;

        float deltaTime, totalGameTime;

        EnemySpawner enemySpawner;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GAME_WIDTH;
            graphics.PreferredBackBufferHeight = GAME_HEIGHT;

            GameManager.SetFrameLimiting(this, true);
            GameManager.SetVSync(graphics, false);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            SoundManager.SoundVolume = 50;
            SoundManager.MusicVolume = 100;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            UIspriteBatch = new SpriteBatch(GraphicsDevice);

            //Utility
            Debug.LoadContent(Content);

            //Base
            GameManager.Background = Content.Load<Texture2D>("background");
            GameManager.StartBackground = Content.Load<Texture2D>("startBackground");
            GameManager.Ground = new Ground(Content);
            GameManager.AddEntity(new Base(Content, GameManager.Ground.Top));
            GameManager.AddEntity(new Turret(Content, GameManager.Ground.Top, Enemy.EnemyDirection.ToLeft), false);
            GameManager.AddEntity(new Turret(Content, GameManager.Ground.Top, Enemy.EnemyDirection.ToRight), true);
            GameManager.AddEntity(new Forcefield(Content, GameManager.Ground.Top));

            //GUI
            guiManager = new GUIManager(Content);
            guiManager.AddWindow(new MainMenu(Content, guiManager));

            //Sound
            SoundManager.LoadContent(Content);
            SoundManager.PlayMusic();

            //Enemies
            EnemyFactory.LoadContent(Content);
            enemySpawner = new EnemySpawner(guiManager);

            Debug.Log("Content Loaded");

            GameManager.State = GameState.StartMenu;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if(IsActive) //Game Window in Focus
            {
                deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                GameManager.Update(deltaTime, Content, guiManager);
                guiManager.Update(this, deltaTime, Content, guiManager, enemySpawner);
                Debug.Update(deltaTime);
                InputManager.Update();

                ReadKeyPresses();

                if (GameManager.State == GameState.Game)
                {
                    totalGameTime += deltaTime;
                    enemySpawner.Update(totalGameTime, deltaTime, guiManager, Content);

                    base.Update(gameTime);
                }
            }
        }

        public void ReadKeyPresses()
        {
            if (GameManager.State == GameState.Game || GameManager.State == GameState.Paused)
            {
                if (InputManager.KeyPress(Keys.Escape) && !GameManager.GameOver)
                {
                    if (guiManager.WindowOpen(typeof(GameMenu)))
                    {
                        guiManager.CloseWindow(typeof(GameMenu));
                        if (!guiManager.WindowOpen(typeof(UpgradeMenu)))
                        {
                            GameManager.State = GameState.Game;
                        }
                    }
                    else
                    {
                        guiManager.AddWindow(new GameMenu(Content, guiManager));
                        GameManager.State = GameState.Paused;
                    }
                }

                if (InputManager.KeyPress(Keys.U))
                {
                    if(GameManager.State == GameState.Game && !GameManager.GameOver)
                    {
                        guiManager.AddWindow(new UpgradeMenu(Content, guiManager, enemySpawner));
                        GameManager.State = GameState.Paused;
                    }
                }

                if (InputManager.KeyPress(Keys.P))
                {
                    GameManager.TogglePause();
                    Debug.Log("Toggled Game Pause");
                }

                if (InputManager.KeyPress(Keys.D))
                {
                    if (GameManager.GodMode)
                    {
                        GameManager.GodMode = false;
                    }
                    else
                    {
                        GameManager.GodMode = true;
                    }
                    Debug.Log("God Mode", GameManager.GodMode);
                }

                if (InputManager.KeyPress(Keys.R))
                {
                    GameManager.State = GameState.StartMenu;
                }

                if (InputManager.KeyPress(Keys.H))
                {
                    guiManager.DisplayMessage("Wave 1: Crawlers are warm and fuzzy");
                }
            }


            if (InputManager.KeyPress(Keys.T))
            {
                GameManager.State = GameState.Game;
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

            if (InputManager.KeyPress(Keys.B))
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



            spriteBatch.End();

            //UI
            UIspriteBatch.Begin();
            guiManager.Draw(UIspriteBatch);
            Debug.Draw(UIspriteBatch);
            UIspriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
