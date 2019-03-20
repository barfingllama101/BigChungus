using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        //Player things
        Player player;
        Texture2D playerSprite;

        Level level;

        //Platform things
        Texture2D dog;
        List<Platform> platforms;
        Platform heldPlatform;

        //Carrot things
        Texture2D CarrotTexture;
        List<Carrot> carrots = new List<Carrot>();
        //int carrotCount = 0;
        //bool hasWon = false;

        //mouse state
        MouseState mouseState;

        //kayboard state
        KeyboardState kStateCurrent;
        KeyboardState kStatePrevious;

        //enum
        enum GameState { Menu, Building, Game, Pause, LevelFinal};
        GameState curr;

        //player movement variables
        int hspd; //horizontal speed
        int vspd; //vertical speed
        int hacc; //horizontal acceleration
        int grav; //vertical acceleration from gravity
        int hmax; //maximum horizontal movespeed


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 1024;
            graphics.ApplyChanges();
        }

        // Checks if enter key was pressed
        public bool EnterKeyPress()
        {
            bool r = false;
            kStateCurrent = Keyboard.GetState();
            if (kStateCurrent.IsKeyDown(Keys.Enter) == true && kStatePrevious.IsKeyDown(Keys.Enter) == false)
            {
                r = true;
            }
            else
            {
                r = false;
            }
            return r;
        }

        // Checks if escape key was pressed
        public bool EscKeyPress()
        {
            bool r = false;
            kStateCurrent = Keyboard.GetState();
            if (kStateCurrent.IsKeyDown(Keys.Escape) == true && kStatePrevious.IsKeyDown(Keys.Escape) == false)
            {
                r = true;
            }
            else
            {
                r = false;
            }
            return r;
        }

        //sets next level
        public void NextLevel()
        {
            try
            {
                String line;
                StreamReader input = new StreamReader("Level1.txt");
                if (input.ReadLine() != null)
                {
                    line = input.ReadLine();
                    String[] platformValues = line.Split(',');
                    platforms = new List<Platform>();
                    for (int i = 0; i < int.Parse(platformValues[0]); i++)
                    {
                        platforms.Add(new Platform(dog, int.Parse(platformValues[(5 * i) + 2]), int.Parse(platformValues[(5 * i) + 3]), int.Parse(platformValues[(5 * i) + 4]), int.Parse(platformValues[(5 * i) + 5])));
                    }
                }
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
            
            CarrotTexture = Content.Load<Texture2D>("CarrotCropped");
            playerSprite = Content.Load<Texture2D>("BigChungusCropped");
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            dog = Content.Load<Texture2D>("SmilingPetDog");
            
            player = new Player(playerSprite, 0, 0);
            NextLevel();
            level = new Level(0, 0, platforms, carrots);
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

            switch (curr)
            {
                case GameState.Menu:
                    kStatePrevious = kStateCurrent;
                    bool res = EnterKeyPress();
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
                    bool res1 = EnterKeyPress();
                    if (res1 == true)
                    {
                        curr = GameState.Game;
                       
                    }
                    break;

                case GameState.Game:

                    //update player position based on hspeed and vspeed
                    player.XPos += hspd;
                    player.YPos += vspd;
                    base.Update(gameTime);

                    kStateCurrent = Keyboard.GetState();

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
                    if (kStateCurrent.IsKeyDown(Keys.Up))
                    {
                        vspd = -16;
                    }
                    //gravity and collision (Needs to be fixed)
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

                    if (player.LevelScore == level.Carrots.Count)
                    {
                        curr = GameState.LevelFinal;
                    }
                    kStatePrevious = kStateCurrent;
                    bool res3 = EscKeyPress();
                    if (res3 == true)
                    {
                        curr = GameState.Pause;
                    }
                    break;

                case GameState.Pause:

                    kStatePrevious = kStateCurrent;
                    bool res4 = EnterKeyPress();
                    if (res4 == true)
                    {
                        curr = GameState.Game;
                    }
                    bool res2 = EscKeyPress();
                    if (res2 == true)
                    {
                        curr = GameState.Menu;
                    }
                    break;

                case GameState.LevelFinal:

                    kStatePrevious = kStateCurrent;
                    bool res5 = EnterKeyPress();
                    if (res5 == true)
                    {
                        curr = GameState.Building;
                        NextLevel();
                    }
                    bool res6 = EscKeyPress();
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
            GraphicsDevice.Clear(Color.Green);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            // Draw
            switch (curr)
            {
                case GameState.Menu:

                    spriteBatch.DrawString(spriteFont, "Press enter to begin", new Vector2(300, 300), Color.White);
                    break;

                case GameState.Building:

                    for (int i = 0; i < level.Platforms.Count; i++)
                    {
                        spriteBatch.Draw(level.Platforms[i].PlatformTexture, level.Platforms[i].Box, Color.White);
                    }

                    spriteBatch.Draw(player.PlayerTexture, player.Box, Color.White);
                    for (int i = 0; i < level.Carrots.Count; i++)
                    {
                        if (level.Carrots[i].Visible == true)
                        {
                            spriteBatch.Draw(level.Carrots[i].CarrotTexture, level.Carrots[i].Box, Color.White);
                        }
                    }

                    break;

                case GameState.Game:

                    spriteBatch.Draw(player.PlayerTexture, player.Box, Color.White);
                    for (int i = 0; i < level.Carrots.Count; i++)
                    {
                        if (level.Carrots[i].Visible == true)
                        {
                            spriteBatch.Draw(level.Carrots[i].CarrotTexture, level.Carrots[i].Box, Color.White);
                        }
                    }
                    for (int i = 0; i < level.Platforms.Count; i++)
                    {
                        spriteBatch.Draw(level.Platforms[i].PlatformTexture, level.Platforms[i].Box, Color.White);
                    }
                    break;

                case GameState.Pause:

                    spriteBatch.DrawString(spriteFont, "Press enter to resume", new Vector2(300, 300), Color.White);
                    spriteBatch.DrawString(spriteFont, "Press esc to menu", new Vector2(300, 400), Color.White);
                    break;

                case GameState.LevelFinal:

                    spriteBatch.DrawString(spriteFont, "Press enter to next level", new Vector2(300, 300), Color.White);
                    spriteBatch.DrawString(spriteFont, "Congrats!", new Vector2(300, 200), Color.White);
                    break;
            }
            
            // End the sprite batch
            spriteBatch.End();
            base.Draw(gameTime);
        }

     
    }
}
