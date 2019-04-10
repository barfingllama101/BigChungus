using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Big_Chungus
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {/*Author:  Maxwell Hazel and Max Bennett
         Class Purpose:  Runs a Monogame program with an image that can be moved with the arrow keys.  Also displays the location of the image.
         Caveats:  The location coordinates displayed in the program only track the top left corner of the image.*/

        //Once you add a level file to the Debug Folder, change this string to its filename
        private List<string> levels = new List<string>();
        private string LevelFile = "Level1.txt";
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        //Texture dictionary for streamlining code
        Dictionary<String, Texture2D> textures = new Dictionary<String, Texture2D>();
        //game bg
        Texture2D gameBG;
        Rectangle gameBGRect;

        //Player things
        Player player;
        Texture2D playerSprite;
        bool alive;

        //Spike things
        Texture2D spikeTexture;
        List<Spike> spikes = new List<Spike>();

        //Spikeball Launcher things
        Texture2D launcherTexture;
        List<SpikeballLauncher> launchers;
        SpikeballLauncher launcher;

        Level level;

        //Platform things
        Texture2D platform;
        List<Platform> platforms;
        Platform heldPlatform;

        //Carrot things
        Texture2D CarrotTexture;
        List<Carrot> carrots = new List<Carrot>();
        //int carrotCount = 0;
        //bool hasWon = false;

        //Spring Things
        Texture2D springTexture;
        List<Spring> springs;

        //mouse state
        MouseState mouseState;

        //kayboard state
        KeyboardState kStateCurrent;
        KeyboardState kStatePrevious;

        //Use this for inventory
        List<int> inventoryItems;

        //enum
        enum GameState { Menu, Building, Game, Pause, LevelFinal, GameOver };
        GameState curr;

        //player movement variables
        int hspd; //horizontal speed
        int vspd; //vertical speed
        int hacc; //horizontal acceleration
        int grav; //vertical acceleration from gravity
        int hmax; //maximum horizontal movespeed

        //main menu
        private Texture2D UITexture;
        private Rectangle UIRect;

        //pause
        private Texture2D pauseTexture;
        private Rectangle pauseTextureRect;
        private Rectangle button1Rect;
        private Rectangle button2Rect;
        private Rectangle mouseRect;

        //Game Over

        private Texture2D gameOverTexture;
        private Rectangle gameOverRectangle;

        //inventory 
        Slot[,] slot;
        int rows = 3;
        int columns = 3;
        Texture2D sTexture;
        List<Platform> pForms = new List<Platform>();


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 1024;
            graphics.ApplyChanges();
        }

        //Checks if a specific key was pressed
        public bool KeyPress(Keys key)
        {
            bool r = false;
            kStateCurrent = Keyboard.GetState();
            if (kStateCurrent.IsKeyDown(key) == true && kStatePrevious.IsKeyDown(key) == false)
            {
                r = true;
            }
            else
            {
                r = false;
            }
            return r;
        }

        //sets next level by reading a text file containing level data
        public void NextLevel()
        {
            alive = true;
            try
            {
                String line;
                StreamReader input = new StreamReader(LevelFile);
                //Adds platforms
                if (input.ReadLine() != null)
                {
                    line = input.ReadLine();
                    String[] platformValues = line.Split(',');
                    platforms = new List<Platform>();
                    for (int i = 0; i < int.Parse(platformValues[0]); i++)
                    {
                        platforms.Add(new Platform(platform, int.Parse(platformValues[(5 * i) + 2]), int.Parse(platformValues[(5 * i) + 3]), int.Parse(platformValues[(5 * i) + 4]), int.Parse(platformValues[(5 * i) + 5])));
                    }
                }
                //Adds carrots
                if (input.ReadLine() != null)
                {
                    line = input.ReadLine();
                    String[] carrotValues = line.Split(',');
                    carrots = new List<Carrot>();
                    for (int i = 0; i < int.Parse(carrotValues[0]); i++)
                    {
                        carrots.Add(new Carrot(CarrotTexture, int.Parse(carrotValues[(2 * i) + 2]), int.Parse(carrotValues[(2 * i) + 3]), CarrotTexture.Width / 2, CarrotTexture.Height / 2));
                    }
                }
                //Adds spikes
                if (input.ReadLine() != null)
                {
                    line = input.ReadLine();
                    String[] spikeValues = line.Split(',');
                    spikes = new List<Spike>();
                    for (int i = 0; i < int.Parse(spikeValues[0]); i++)
                    {
                        spikes.Add(new Spike(spikeTexture, int.Parse(spikeValues[(2 * i) + 2]), int.Parse(spikeValues[(2 * i) + 3]), spikeTexture.Width / 2, spikeTexture.Height / 2));
                    }
                }
                //Adds springs
                if (input.ReadLine() != null)
                {
                    line = input.ReadLine();
                    String[] springValues = line.Split(',');
                    springs = new List<Spring>();
                    for (int i = 0; i < int.Parse(springValues[0]); i++)
                    {
                        platforms.Add(new Spring(springTexture, int.Parse(springValues[(2 * i) + 2]), int.Parse(springValues[(2 * i) + 3]), 150, 40));
                    }
                }
                //Adds launchers
                if (input.ReadLine() != null)
                {
                    line = input.ReadLine();
                    String[] launcherValues = line.Split(',');
                    launchers = new List<SpikeballLauncher>();
                    for (int i = 0; i < int.Parse(launcherValues[0]); i++)
                    {
                        launchers.Add(new SpikeballLauncher(launcherTexture, int.Parse(launcherValues[(3 * i) + 2]), int.Parse(launcherValues[(3 * i) + 3]), 80, 80, int.Parse(launcherValues[(3 * i) + 4]), spikes, spikeTexture));
                    }
                }
                //Reads inventory
                if (input.ReadLine() != null)
                {
                    line = input.ReadLine();
                    String[] inventoryValues = line.Split(',');
                    inventoryItems = new List<int>();
                    for (int i = 0; i < 6; i++)
                    {
                        inventoryItems.Add(int.Parse(inventoryValues[i]));
                    }
                }
                if (input.ReadLine() != null)
                {
                    line = input.ReadLine();
                    String[] playerSpawnCoor = line.Split(',');
                    player.XPos = int.Parse(playerSpawnCoor[0]);
                    player.YPos = int.Parse(playerSpawnCoor[1]);
                }
                input.Close();
            }
        
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
          
            player.LevelScore = 0;
        }

        //SPRING COLLISION REQUIRED
        public void CheckCollision(Rectangle platformBox)
        {
            kStateCurrent = Keyboard.GetState();
            //vertical collision and prevents falling through the floor
            if (player.Box.Intersects(platformBox) && !kStatePrevious.IsKeyDown(Keys.Up))
            {
                vspd += 0;
                if (player.YPos <= platformBox.Y)
                {
                    player.YPos = platformBox.Y - player.Height;
                }
            }

            //horizontal collision
            if (player.Box.Intersects(platformBox) && !kStateCurrent.IsKeyDown(Keys.Left) && !kStatePrevious.IsKeyDown(Keys.Up))
            {
                player.XPos = platformBox.X - player.Width;
                hspd = 0;
            }
            else if (player.Box.Intersects(platformBox) && !kStateCurrent.IsKeyDown(Keys.Right) && !kStatePrevious.IsKeyDown(Keys.Up))
            {
                player.XPos = platformBox.X + player.Width;
                hspd = 0;
            }
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //show the mouse
            this.IsMouseVisible = true;

            hspd = 0;
            vspd = 0;
            hacc = 1;
            grav = 1;
            hmax = 7;

            heldPlatform = null;
            mouseState = Mouse.GetState();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            launcherTexture = Content.Load<Texture2D>("SmilingPetDog");
            springTexture = Content.Load<Texture2D>("spring");
            spikeTexture = Content.Load<Texture2D>("spike");
            CarrotTexture = Content.Load<Texture2D>("CarrotCropped");
            playerSprite = Content.Load<Texture2D>("BigChungusCropped");
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            platform = Content.Load<Texture2D>("platform");
            gameBG = Content.Load<Texture2D>("GAMESCREEN");
            gameBGRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            sTexture = Content.Load<Texture2D>("SmilingPetDog");
            //main menu
            UITexture = Content.Load<Texture2D>("chung");
            UIRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);


            //pause menu
            pauseTexture = Content.Load<Texture2D>("pausescreen");
            pauseTextureRect = new Rectangle(GraphicsDevice.Viewport.Width/2-300,GraphicsDevice.Viewport.Height/2-200, pauseTexture.Width, pauseTexture.Height);
            mouseRect = new Rectangle(mouseState.X, mouseState.Y, 10, 10);

            button1Rect = new Rectangle(300, 400, pauseTexture.Width, 125);
            button2Rect = new Rectangle(300, 600, pauseTexture.Width, 100);


            //game Over
            gameOverTexture = Content.Load<Texture2D>("GAMEOVER");
            gameOverRectangle = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);


            //inventory 
           // rows = 3;
            //columns = 3;
            slot = new Slot[rows, columns];


            for (int i = 0; i < rows; i++)
            {

                for (int j = 0; j < columns; j++)
                {
                    slot[i, j] = new Slot(sTexture, i * 100 + 600, j * 100 +600, Color.Wheat);
                   // platforms.Add(new Platform(slot[i, j].XPos, slot[i, j].YPos, itemTexture, Color.AliceBlue));
                   // Debug.WriteLine("stexture" + sTexture);
                }
            }
            player = new Player(playerSprite, 0, 0);
            NextLevel();
            level = new Level(0, 0, platforms, carrots, spikes, springs, launchers, inventoryItems);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here

            //button click 
            mouseRect = new Rectangle(mouseState.X, mouseState.Y, 10, 10);
            switch (curr)
            {
                case GameState.Menu:
                    kStatePrevious = kStateCurrent;
                    bool res = KeyPress(Keys.Enter);
                    if (res == true)
                    {
                        curr = GameState.Building;
                        NextLevel();
                    }
                    break;

                case GameState.Building:

                    //let the player move platforms with isMovable set to true
                    MouseState prevMouseState = mouseState;
                    mouseState = Mouse.GetState();
                    foreach (Platform p in level.Platforms)
                    {
                        if (heldPlatform == null)
                        {
                            //checks if the mouse button is clicked on the platform, and if the platform's isMovable is true, then sets the heldplatform
                            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && p.Box.Intersects(new Rectangle(mouseState.Position, new Point(1))) && p.IsMoveable == true)
                            {
                                heldPlatform = p;
                            }
                        }
                    }
                    if (heldPlatform != null)
                    {
                        //moves the held platform and releases it if the mouse button is released
                        heldPlatform.Drag();
                        if (mouseState.LeftButton != ButtonState.Pressed)
                        {
                            heldPlatform = null;
                        }
                    }

                    kStatePrevious = kStateCurrent;
                    bool res1 = KeyPress(Keys.Enter);
                    if (res1 == true)
                    {
                        curr = GameState.Game;
                       
                    }
                    break;

                case GameState.Game:

                    alive = true;
                    //update player position based on hspeed and vspeed
                    player.XPos += hspd;
                    player.YPos += vspd;
                    base.Update(gameTime);

                    kStateCurrent = Keyboard.GetState();
                    //Pause
                    bool res3 = KeyPress(Keys.P);
                    if (res3 == true)
                    {
                        curr = GameState.Pause;
                    }
                    kStatePrevious = kStateCurrent;

                    //gravity
                    if (player.standingCheck(level.Platforms) == false)
                    {
                        vspd += grav;
                    }
                    else
                    {
                        vspd = 0;
                    }

                    //jump
                    if (kStateCurrent.IsKeyDown(Keys.Up) && player.standingCheck(level.Platforms))
                    {
                        vspd = -16;
                    }
                    //gravity and collision
                    for (int i = 0; i < level.Platforms.Count; i++)
                    {
                        if (player.Box.Intersects(level.Platforms[i].Box))
                        {
                            if (!kStateCurrent.IsKeyDown(Keys.Up) && player.standingCheck(level.Platforms))
                            {
                                vspd += 0;
                            }
                            kStatePrevious = kStateCurrent;
                            CheckCollision(level.Platforms[i].Box);
                        }
                    }

                    //bounds
                    if (player.XPos < 0)
                    {
                        player.XPos = 0;
                    }
                    if (player.XPos > GraphicsDevice.Viewport.Width)
                    {
                        player.XPos = GraphicsDevice.Viewport.Width;
                    }

                    //health system
                    if (player.YPos > GraphicsDevice.Viewport.Height)
                    {
                        alive = false;
                        if (alive == false)
                        {
                            curr = GameState.GameOver;
                        }
                    }

                    //spike launching
                    foreach (SpikeballLauncher item in launchers)
                    {
                        item.Launch(2);
                    }

                    //collision with spikes
                    foreach (Spike spikeObject in spikes)
                    {
                        alive = player.CheckCollision(spikeObject.Box);
                        if (alive == false)
                        {
                            curr = GameState.GameOver;
                        }
                    }
                    
                    //Carrot dectection and win tracking
                    for (int i = 0; i < level.Carrots.Count; i++)
                    {
                        bool r = level.Carrots[i].CheckCollision(player.Box);
                        if (r == true)
                        {
                            level.Carrots[i].Visible = false;
                            player.LevelScore++;
                        }
                    }
                    if (player.LevelScore == level.Carrots.Count)
                    {
                        curr = GameState.LevelFinal;
                    }

                    //left and right movement/deceleration
                    if (kStateCurrent.IsKeyDown(Keys.Left) && hspd > -hmax)
                    {
                        hspd -= hacc;
                    }
                    else if (hspd < 0)
                    {
                        hspd += hacc;
                    }
                    if (kStateCurrent.IsKeyDown(Keys.Right) && hspd < hmax)
                    {
                        hspd += hacc;
                    }
                    else if (hspd > 0)
                    {
                        hspd -= hacc;
                    }
                    break;

                case GameState.GameOver:

                    kStatePrevious = kStateCurrent;
                    bool res7 = KeyPress(Keys.Enter);
                    if (res7 == true)
                    {
                        curr = GameState.Building;
                        NextLevel();
                    }
                    bool res8 = KeyPress(Keys.M);
                    if (res8 == true)
                    {
                        curr = GameState.Menu;
                    }
                    break;

                case GameState.Pause:
                    MouseState pMouseState = mouseState;
                    mouseState = Mouse.GetState();
                    kStatePrevious = kStateCurrent;
                    bool res4 = KeyPress(Keys.Enter);
                  /*    if (res4 == true)
                      {
                          curr = GameState.Game;
                      }
                      bool res2 = KeyPress(Keys.M);
                      if (res2 == true)
                      {
                          curr = GameState.Menu;
                      }*/
                   // IsMouseVisible = true;
                    if (mouseRect.Intersects(button1Rect))
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed && pMouseState.LeftButton == ButtonState.Released)
                        {
                            curr = GameState.Game;

                            // do something here
                        }
                    }
                    if (mouseRect.Intersects(button2Rect))
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed && pMouseState.LeftButton == ButtonState.Released)
                        {
                            curr = GameState.Menu;

                            // do something here
                        }
                    }
                    break;

                case GameState.LevelFinal:

                    kStatePrevious = kStateCurrent;
                    bool res5 = KeyPress(Keys.Enter);
                    if (res5 == true)
                    {
                        curr = GameState.Building;
                        NextLevel();
                    }
                    bool res6 = KeyPress(Keys.Escape);
                    if (res6 == true)
                    {
                        curr = GameState.Menu;
                    }
                    break;
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightCyan);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            // Draw
            switch (curr)
            {
                case GameState.Menu:

                    spriteBatch.Draw(UITexture, UIRect, Color.White);
                    spriteBatch.DrawString(spriteFont, "Press enter to begin", new Vector2(432, 800), Color.Blue);
                    spriteBatch.DrawString(spriteFont, "In Building Mode, click and drag platforms to move them, then press enter to begin the level", new Vector2(200, 100), Color.Blue);
                    spriteBatch.DrawString(spriteFont, "Walk:  Left and Right Arrows", new Vector2(50, 200), Color.Blue);
                    spriteBatch.DrawString(spriteFont, "Jump:  Up Arrows", new Vector2(50, 250), Color.Blue);
                    break;

                case GameState.Building:

                    for (int i = 0; i < level.Platforms.Count; i++)
                    {
                        spriteBatch.Draw(level.Platforms[i].Texture, level.Platforms[i].Box, Color.White);
                    }

                    spriteBatch.Draw(player.Texture, player.Box, Color.White);
                    for (int i = 0; i < level.Carrots.Count; i++)
                    {
                        level.Carrots[i].Visible = true;
                        if (level.Carrots[i].Visible == true)
                        {
                            spriteBatch.Draw(level.Carrots[i].Texture, level.Carrots[i].Box, Color.White);
                        }
                    }
                    for (int i = 0; i < spikes.Count; i++)
                    {
                        spriteBatch.Draw(spikes[i].Texture, spikes[i].Box, Color.White);
                    }
                    for (int i = 0; i < launchers.Count; i++)
                    {
                        spriteBatch.Draw(launchers[i].Texture, launchers[i].Box, Color.White);
                    }
                    spriteBatch.DrawString(spriteFont, "In Building Mode, click and drag platforms to move them, then press enter to begin the level", new Vector2(200, 50), Color.Blue);
                    spriteBatch.DrawString(spriteFont, "Mode: Building", new Vector2(GraphicsDevice.Viewport.Width - 200,100), Color.DarkBlue);


                    int jk = 0;
                    for (int i = 0; i < rows; i++)
                    {
                    
                        for (int j = 0; j < columns; j++)
                        {
                            slot[i, j].Draw(spriteBatch);

                            //can't d this but worked in testing
                        //    level.Platforms[i].Box = slot[i, j].bRect;

                           // level.Platforms[i].Box
                            jk++;
                        }

                    }
                    for(int i = 0; i < level.Platforms.Count; i++)
                    {
                        spriteBatch.Draw(level.Platforms[i].Texture, level.Platforms[i].Box, Color.AliceBlue);
                    }
                  


                    break;

                case GameState.Game:
                    spriteBatch.Draw(gameBG, gameBGRect, Color.White);
                    spriteBatch.Draw(player.Texture, player.Box, Color.White);
                    
                    for (int i = 0; i < level.Carrots.Count; i++)
                    {
                        if (level.Carrots[i].Visible == true)
                        {
                            spriteBatch.Draw(level.Carrots[i].Texture, level.Carrots[i].Box, Color.White);
                        }
                    }
                    for (int i = 0; i < level.Platforms.Count; i++)
                    {
                        spriteBatch.Draw(level.Platforms[i].Texture, level.Platforms[i].Box, Color.White);
                    }
                    for (int i = 0; i < spikes.Count; i++)
                    {
                        spriteBatch.Draw(spikes[i].Texture, spikes[i].Box, Color.White);
                    }
                    for (int i = 0; i < launchers.Count; i++)
                    {
                        spriteBatch.Draw(launchers[i].Texture, launchers[i].Box, Color.White);
                    }
                    spriteBatch.DrawString(spriteFont, "Mode: Game Mode", new Vector2(GraphicsDevice.Viewport.Width - 200,100), Color.DarkBlue);
                    spriteBatch.DrawString(spriteFont, "Walk:  Left and Right Arrows", new Vector2(50, 200), Color.Blue);
                    spriteBatch.DrawString(spriteFont, "Jump:  Up Arrow", new Vector2(50, 250), Color.Blue);
                    spriteBatch.DrawString(spriteFont, string.Format("carrots collected: {0}", player.LevelScore), new Vector2(GraphicsDevice.Viewport.Width - 200, 150), Color.DarkBlue);
                    break;

                case GameState.GameOver:
                    spriteBatch.Draw(gameOverTexture, gameOverRectangle, Color.White);
                    spriteBatch.DrawString(spriteFont, "GAME OVER", new Vector2(GraphicsDevice.Viewport.Width / 2-40, 200), Color.DarkBlue);
                    spriteBatch.DrawString(spriteFont, "Press enter to restart", new Vector2(GraphicsDevice.Viewport.Width / 2-40, 300), Color.DarkBlue);
                    spriteBatch.DrawString(spriteFont, "Press M to menu", new Vector2(GraphicsDevice.Viewport.Width / 2 - 40, 400), Color.DarkBlue);
                    break;

                case GameState.Pause:

                    spriteBatch.DrawString(spriteFont, "Click on an option to continue", new Vector2(320, 250), Color.DarkBlue);
                    spriteBatch.Draw(pauseTexture, pauseTextureRect, Color.White);
          
                    break;

                case GameState.LevelFinal:
                    spriteBatch.Draw(gameOverTexture, gameOverRectangle, Color.White);
                    spriteBatch.DrawString(spriteFont, "Press enter to next level", new Vector2(300, 300), Color.DarkBlue);
                    spriteBatch.DrawString(spriteFont, "Congrats!", new Vector2(300, 200), Color.DarkBlue);
                    spriteBatch.DrawString(spriteFont, String.Format("TOTAL SCORE: {0}", player.LevelScore), new Vector2(300, 400), Color.DarkBlue);

                    break;
            }
            
            // End the sprite batch
            spriteBatch.End();
            base.Draw(gameTime);
        }

     
    }
}
