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

        #region declaring
        //Once you add a level file to the Debug Folder, change this string to its filename
        private List<string> levels = new List<string>();
        private string LevelFile = ("Levels/" + "Level1.txt");
        private int levelCount = 0;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        //Texture dictionary for streamlining code
        Dictionary<String, Texture2D> textures = new Dictionary<String, Texture2D>();

        //game bg
        Texture2D gameBG;
        Rectangle gameBGRect;
        //Add backgrounds to this list
        List<Texture2D> gameBGS = new List<Texture2D>();

        //Player things
        Player player;
        Texture2D playerSprite;
        //Add player spritesheets to this list once they are standardized
        List<Texture2D> playerSprites = new List<Texture2D>();
        bool alive;
        int[] spawn = new int[2];

        //player animation things
        enum animation { WalkLeft, WalkRight, Left, Right, IdleLeft, IdleRight, Idle, JumpLeft, JumpRight};
        animation current;

        int frame;      
        double timePerFrame = 500;
        int numFrames = 6;
        int framesElapsed;
        int frameWidth = 160;
        int frameHeight = 205;

        int currentTime = 0;
        int period = 50;
        Point currentFrame = new Point(0, 0);
        Point spriteSize = new Point(8, 3);

        //Spike things
        Texture2D spikeTexture;
        List<Spike> spikes = new List<Spike>();

        //Spikeball Launcher things
        Texture2D launcherTexture;
        List<SpikeballLauncher> launchers;

        Level level;

        //Platform things
        Texture2D platform;
        List<Platform> platforms;
        
        //Carrot things
        Texture2D carrotTexture;
        List<Carrot> carrots = new List<Carrot>();
        //int carrotCount = 0;
        bool hasWon = false;

        //Spring Things
        Texture2D springTexture;
        List<Spring> springs;

        //mouse state
        MouseState mouseState = new MouseState();
        MouseState prevMouseState;
        //kayboard state
        KeyboardState kStateCurrent;
        KeyboardState kStatePrevious;

        //enum
        enum GameState { Menu, LevelSelect, Building, Game, Pause, LevelFinal, GameOver };
        GameState curr;

        //player movement variables
        int hspd; //horizontal speed
        int vspd; //vertical speed
        int hacc; //horizontal acceleration
        int grav; //vertical acceleration from gravity
        int hmax; //maximum horizontal movespeed

        //collision rectangles
        Rectangle tempHRec1;
        Rectangle tempHRec2;
        Rectangle tempVRec1;
        Rectangle tempVRec2;

        //main menu
        private Texture2D UITexture;
        private Rectangle UIRect;

        //levelselect
        private List<UIElement> UIButtons = new List<UIElement>();
        private List<LevelButton> levelButtons;

        //levelselect2electric boogaloo
        private UIElement[,] UIButts;
        private LevelButton[,] levelButts;

        //invisible for padding 
        private UIElement[,] invisible;

        //pause
        private Texture2D pauseTexture;
        private Rectangle pauseTextureRect;
        private Rectangle button1Rect;
        private Rectangle button2Rect;
        private Rectangle mouseRect;
        private Texture2D pauseBG;
        private Rectangle pauseBGRect;

        //Game Over

        private Texture2D gameOverTexture;
        private Rectangle gameOverRectangle;

        private Texture2D continueButton;
        private Texture2D mainMenuButton;

        private Rectangle contdRect;
        private Rectangle mainMenRect;

        Texture2D gameWinScreen;

        //inventory
        List<Slot> slot;
        int slots = 6;
        //int columns = 1;
        Texture2D sTexture;
        List<int> inventoryItems = new List<int>();
        List<Platform> pForms = new List<Platform>();
        GameObject heldObject;
        //Class list for slots
        GameObject[] classes = new GameObject[6];

        // INVENTORY AESTHETICS 
        private Texture2D inventorySplash;
        private Rectangle inventorySplashRect;

        private Texture2D platformSplash;

        private Texture2D springSplash;

        private Texture2D emptySplash;

        private Rectangle instructionRect;
        #endregion
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
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
        public void NextLevel(int levelnum)
        {
            levelCount = levelnum;
            if (levelCount < levels.Count)
            {
                LevelFile = levels[levelCount];
                alive = true;

                #region reading LevelFile
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
                            platforms.Add(new Platform(textures[platformValues[(5 * i) + 1]], int.Parse(platformValues[(5 * i) + 2]), int.Parse(platformValues[(5 * i) + 3]), int.Parse(platformValues[(5 * i) + 4]), int.Parse(platformValues[(5 * i) + 5])));
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
                            carrots.Add(new Carrot(textures[carrotValues[1]], int.Parse(carrotValues[(2 * i) + 2]), int.Parse(carrotValues[(2 * i) + 3]), carrotTexture.Width / 2, carrotTexture.Height / 2));
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
                            spikes.Add(new Spike(textures[spikeValues[1]], int.Parse(spikeValues[(2 * i) + 2]), int.Parse(spikeValues[(2 * i) + 3]), 80, 80));
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
                           //springs.Add(new Spring(springTexture, int.Parse(springValues[(2 * i) + 2]), int.Parse(springValues[(2 * i) + 3]), 150, 40));
                            platforms.Add(new Spring(textures[springValues[1]], int.Parse(springValues[(2 * i) + 2]), int.Parse(springValues[(2 * i) + 3]), 150, 40));
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
                            launchers.Add(new SpikeballLauncher(textures[launcherValues[1]], int.Parse(launcherValues[(3 * i) + 2]), int.Parse(launcherValues[(3 * i) + 3]), 80, 57, int.Parse(launcherValues[(3 * i) + 4]), spikes, spikeTexture));
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
                    
                    //Sets spawn
                    if (input.ReadLine() != null)
                    {
                        line = input.ReadLine();
                        String[] playerSpawnCoor = line.Split(',');
                        spawn[0] = int.Parse(playerSpawnCoor[0]);
                        spawn[1] = int.Parse(playerSpawnCoor[1]);
                    }
                    //Sets background
                    if (input.ReadLine() != null)
                    {
                        line = input.ReadLine();
                        gameBG = gameBGS[int.Parse(line)];
                    }
                    //Sets player sprite
                    if (input.ReadLine() != null)
                    {
                        line = input.ReadLine();
                        playerSprite = playerSprites[int.Parse(line)];
                    }
                    level = new Level(spawn[0], spawn[1], platforms, carrots, spikes, springs, launchers, inventoryItems);

                    slot = new List<Slot>();
                    classes[0] = new Platform(platform, 200, 40);
                    classes[1] = new Spring(springTexture, 150, 40);
                    classes[2] = new Carrot(carrotTexture, carrotTexture.Width / 2, carrotTexture.Height / 2);
                    classes[3] = new Spike(spikeTexture, spikeTexture.Width / 2, spikeTexture.Height / 2);
                    classes[4] = new SpikeballLauncher(launcherTexture, 80, 80, level.Spikes, spikeTexture);
                    classes[5] = new Player(playerSprite);

                    //Makes Slots
                    for (int i = 0; i < slots; i++)
                    {
                        slot.Add(new Slot(sTexture, 30+ 3 + i * 200, 668, Color.Wheat, inventoryItems[i], classes[i]));
                    }
                    slot[0].SlotName = "Platform";
                    slot[0].SlotDescription = "Place this object anywhere on the screen for Chungus to travel.";
                    slot[1].SlotName = "Spring";
                    slot[1].SlotDescription = "place this on the screen for Chungus to jump high!";

                    slot[0].SlotTypeTexture = platformSplash;

                    slot[1].SlotTypeTexture = springSplash;

                    for (int i = 2; i < slot.Count; i++)
                    {
                        slot[i].SlotTypeTexture = emptySplash;
                        slot[i].SlotDescription = "There's nothing here! Maybe in another level?...";
                    }


                    player = new Player(playerSprite, level.PlayerSpawnX, level.PlayerSpawnY);
                    player.LevelScore = 0;
                    level.AddObject(player);
                    input.Close();
                }

                catch (System.Exception e)
                {
                    Debug.WriteLine(e.Message);

                    throw;
                }
                #endregion
            }
        }

        internal void CheckCollision(List<Platform> platformList)
        {
            //New collision uses 4 rectangles to detect platforms before the main player box intersects them, meaning that collision works in every direction and at all times.
            kStateCurrent = Keyboard.GetState();
            

            for (int i = 0; i < platformList.Count; i++)
            {
                tempHRec1 = new Rectangle(player.XPos + hspd, player.YPos, player.Width, player.Height);
                tempHRec2 = new Rectangle(player.XPos + Math.Sign(hspd), player.YPos, player.Width, player.Height);

                //horizontal collision
                if (tempHRec1.Intersects(platformList[i].Box))
                {
                    while (tempHRec2.Intersects(platformList[i].Box) == false)
                    {
                           
                        player.XPos += Math.Sign(hspd);
                        tempHRec2 = new Rectangle(player.XPos + Math.Sign(hspd), player.YPos, player.Width, player.Height);
                    }
                    if (platformList[i] is Spring && vspd > 0 && player.YPos + player.Height < platformList[i].YPos + 1)
                    {

                    }
                    else
                    {
                        hspd = 0;
                    }
                }
            }
            //update player position based on hspeed
            player.XPos += hspd;
            
            for (int i = 0; i < platformList.Count; i++)
            {
                tempVRec1 = new Rectangle(player.XPos, player.YPos + vspd, player.Width, player.Height);
                tempVRec2 = new Rectangle(player.XPos, player.YPos + Math.Sign(vspd), player.Width, player.Height);


                // fixed collisions -- Kimmy
                //vertical collision and prevents falling through the floor
                if (tempVRec1.Intersects(platformList[i].Box))
                {
                    while (tempVRec2.Intersects(platformList[i].Box) == false)
                    {        
                        player.YPos += Math.Sign(vspd);
                        tempVRec2 = new Rectangle(player.XPos, player.YPos + Math.Sign(vspd), player.Width, player.Height);
                    }
                    if(platformList[i] is Spring && vspd > 0 && player.YPos + player.Height < platformList[i].YPos + 1)
                    {
                        vspd = -24;
                    }
                    else
                    {
                        vspd = 0;
                    }
                    
                }
            }
            //update player position based on vspeed
            

            player.YPos += vspd;
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

            heldObject = null;
            mouseState = Mouse.GetState();
            instructionRect = new Rectangle(220, 475, 500, 25);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            levels.Add("Level1.txt");
            levels.Add("Level2.txt");
            levels.Add("Level3.txt");
            levels.Add("Level4.txt");
            levels.Add("Level5.txt");
            levels.Add("Level6.txt");
            levels.Add("Level7.txt");
            levels.Add("Level8.txt");
            levels.Add("Level9.txt");
            levels.Add("Level10.txt");
            //levels.Add("TestLevel.txt");
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region gameplay images
            // TODO: use this.Content to load your game content here
            launcherTexture = Content.Load<Texture2D>("cannon");
            springTexture = Content.Load<Texture2D>("spring");
            spikeTexture = Content.Load<Texture2D>("spikeanim");
            carrotTexture = Content.Load<Texture2D>("CarrotCropped");
            //playerSprite = Content.Load<Texture2D>("BigChungusCropped");
            playerSprite = Content.Load<Texture2D>("POLY");
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            platform = Content.Load<Texture2D>("platform");
            gameBG = Content.Load<Texture2D>("level1");
            gameBGRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            sTexture = Content.Load<Texture2D>("slot");

            textures.Add("launcherTexture", Content.Load<Texture2D>("cannon"));
            textures.Add("springTexture", Content.Load<Texture2D>("spring"));
            textures.Add("spikeTexture", Content.Load<Texture2D>("spikeanim"));
            textures.Add("carrotTexture", Content.Load<Texture2D>("CarrotCropped"));
            //textures.Add("playerSprite", Content.Load<Texture2D>("BigChungusCropped"));
            textures.Add("platform", Content.Load<Texture2D>("platform"));
            //textures.Add("gameBG", Content.Load<Texture2D>("GAMESCREEN"));
            textures.Add("sTexture", Content.Load<Texture2D>("slot"));
            textures.Add("playerSprite", Content.Load<Texture2D>("Big Chung"));

            gameBGS.Add(Content.Load<Texture2D>("GAMESCREEN"));
            gameBGS.Add(Content.Load<Texture2D>("level1"));
            gameBGS.Add(Content.Load<Texture2D>("background"));
            playerSprites.Add(Content.Load<Texture2D>("POLY"));
            playerSprites.Add(Content.Load<Texture2D>("3"));
            //playerSprites.Add(Content.Load<Texture2D>("BigChungusCropped"));
            #endregion
            //main menu

            #region main Menu 
            UITexture = Content.Load<Texture2D>("chung");
            UIRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            #endregion
            #region Level Select
  
       // this is initializing the array of buttons 
            UIButts = new UIElement[4, 3];
          // this is so the level displayed will be one of twelve levels 
            int counter = 1;
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    UIButts[i, j] = new UIElement(counter, 20 + i *400 , 200 *j + 200,String.Format("Level {0}", counter++));
                }
            }
            #endregion
            #region pause menu
            pauseTexture = Content.Load<Texture2D>("pausescreen");

            pauseBG = Content.Load<Texture2D>("level1");
            pauseBGRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            pauseTextureRect = new Rectangle(GraphicsDevice.Viewport.Width/2-300,GraphicsDevice.Viewport.Height/2-200, pauseTexture.Width, pauseTexture.Height);
            mouseRect = new Rectangle(mouseState.X, mouseState.Y, 10, 10);

            button1Rect = new Rectangle(300, 300, pauseTexture.Width, 125);
            button2Rect = new Rectangle(300, 450, pauseTexture.Width, 100);

            #endregion

            #region game Over
            gameOverTexture = Content.Load<Texture2D>("GOScreen");
            gameOverRectangle = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            continueButton = Content.Load < Texture2D>("overContd");
            contdRect = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 70, GraphicsDevice.Viewport.Height / 2-10, 200, 70);

            mainMenuButton = Content.Load<Texture2D>("overGammain");
            mainMenRect = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 70, GraphicsDevice.Viewport.Height / 2 - 90, 200, 70);

            #endregion
            gameWinScreen = Content.Load<Texture2D>("GAMEOVER");
            #region inventory

            inventorySplash = Content.Load<Texture2D>("InventoryExtra");
            inventorySplashRect = new Rectangle(0, 400, 250, 250);
            platformSplash = Content.Load<Texture2D>("platformInv");

            springSplash = Content.Load<Texture2D>("SpringInv");
            emptySplash = Content.Load<Texture2D>("emptyINV");
      

            #endregion
            NextLevel(0);
            
            //player = new Player(playerSprite, level.PlayerSpawnX, level.PlayerSpawnY);
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
                #region Main Menu
                case GameState.Menu:
                    kStatePrevious = kStateCurrent;
                    bool res = KeyPress(Keys.Enter);
                    if (res == true)
                    {
                        
                        curr = GameState.LevelSelect;
                    }
                    break;
                #endregion
                #region Level Select
                case GameState.LevelSelect:
                    MouseState pMouseState = mouseState;
                    mouseState = Mouse.GetState();
                    kStatePrevious = kStateCurrent;
             
                    //checking to see if the mouse has intersected with one of the level buttons 
                    for(int i = 0; i < 4; i++)
                    {
                        for(int j = 0; j <3; j++)
                        {
                            if (mouseRect.Intersects(UIButts[i, j].Box))
                            {
                                // "magnifying" the button on intersection 
                                UIButts[i, j].Width = 120;
                                UIButts[i, j].Height = 40;
                               
                                // if the button is clicked. 
                                if (mouseState.LeftButton == ButtonState.Pressed && pMouseState.LeftButton == ButtonState.Released)
                                {
                                    if(UIButts[i,j].LevelNum >= levels.Count)
                                    {
                                        Console.WriteLine("This level is not available yet!");
                                    }
                                    else
                                    {
                                        curr = GameState.Building;
                                        NextLevel(UIButts[i, j].LevelNum);
                                    }
                                }
                            }
                            else // so the button will go back to its regular height and width after it's intersected with. 
                            {
                                UIButts[i, j].Width =100;
                                UIButts[i, j].Height = 20;
                            }
                        }
                    }

                  
                    break;
                #endregion
                #region building phase 

                case GameState.Building:
                    
                    prevMouseState = mouseState;
                    mouseState = Mouse.GetState();
                    //let the player move platforms with isMovable set to true

                    foreach (Slot button in slot)
                    {
                        button.getItem(level, mouseState, prevMouseState, spikeTexture, level.Spikes);
                    }

                    if (heldObject == null)
                    {
                        heldObject = level.HoldObject(mouseState, prevMouseState, heldObject);
                    }
                    
                    if (heldObject != null)
                    {
                        //moves the held platform and releases it if the mouse button is released
                        heldObject.Drag();
                        if (mouseState.LeftButton != ButtonState.Pressed)
                        {
                            level.AddObject(heldObject);
                            heldObject = null;
                        }
                    }

                    kStatePrevious = kStateCurrent;
                    bool res1 = KeyPress(Keys.Enter);
                    if (res1 == true)
                    {
                        curr = GameState.Game;

                    }
                    for(int i = 0; i < slot.Count; i++)
                    {
                        if (slot[i].SlotIntersecting(mouseRect))
                        {
                            Debug.WriteLine("yes");
                        }
                    }
                    break;
                #endregion
                #region Play State 
                case GameState.Game:

                    alive = true;
                    
                    base.Update(gameTime);

                    kStateCurrent = Keyboard.GetState();
                    //Pause
                    bool res3 = KeyPress(Keys.P);
                    if (res3 == true)
                    {
                        curr = GameState.Pause;
                    }
                    //Restart
                    bool res4 = KeyPress(Keys.R);
                    if (res4 == true)
                    {
                        level.Player.XPos = level.PlayerSpawnX;
                        level.Player.YPos = level.PlayerSpawnY;
                        level.Player.LevelScore = 0;
                        for (int i = 0; i < spikes.Count; i++)
                        {
                            spikes[i].XPos = level.SpikePositions[0][i];
                            spikes[i].YPos = level.SpikePositions[1][i];
                        }
                        curr = GameState.Building;
                    }
                    kStatePrevious = kStateCurrent;

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


                    //gravity
                    if (vspd < 36)
                    {
                        vspd += grav;
                    }

                    //animation
                    currentTime += gameTime.ElapsedGameTime.Milliseconds;

                    if(currentTime > period)
                    {
                        currentTime -= period;
                        ++currentFrame.X;
                        
                        if(currentFrame.X >= spriteSize.X)
                        {
                            currentFrame.X = 0;
                        }
                    }

                    if(kStateCurrent.IsKeyDown(Keys.Right) == true && player.standingCheck(level.Platforms) == true)
                    {
                        current = animation.Right;
                    }

                    if (player.standingCheck(level.Platforms) == false && (current == animation.Left || current == animation.IdleLeft))
                    {
                        current = animation.JumpLeft;
                    }

                    if (player.standingCheck(level.Platforms) == false && (current == animation.Right || current == animation.IdleRight || current == animation.Idle))
                    {
                        current = animation.JumpRight;
                    }

                    if (kStateCurrent.IsKeyDown(Keys.Left) == true && player.standingCheck(level.Platforms) == true && player.XPos > 0)
                    {
                        current = animation.Left;
                    }

                    if (kStateCurrent.IsKeyDown(Keys.Left) == false && player.standingCheck(level.Platforms) == true && kStateCurrent.IsKeyDown(Keys.Right) == false && (current == animation.Left || current == animation.JumpLeft))
                    {
                        current = animation.IdleLeft;
                    }

                    if (kStateCurrent.IsKeyDown(Keys.Left) == false && player.standingCheck(level.Platforms) == true && kStateCurrent.IsKeyDown(Keys.Right) == false && (current == animation.Right|| current == animation.JumpRight))
                    {
                        current = animation.IdleRight;
                    }
                    if (kStateCurrent.IsKeyDown(Keys.Left) == false && kStateCurrent.IsKeyDown(Keys.Right) == false && player.standingCheck(level.Platforms) == true && current != animation.IdleLeft)
                    {
                        current = animation.Idle;
                    }
                    if(current == animation.JumpLeft && kStateCurrent.IsKeyDown(Keys.Right) == true)
                    {
                        current = animation.JumpRight;
                    }
                    if (current == animation.JumpRight && kStateCurrent.IsKeyDown(Keys.Left) == true)
                    {
                        current = animation.JumpLeft;
                    }

                    //jump
                    if (kStateCurrent.IsKeyDown(Keys.Up) && player.standingCheck(level.Platforms))
                    {
                        vspd = -16;
                    }



                    //gravity and collision
                    kStatePrevious = kStateCurrent;
                    CheckCollision(level.Platforms);


                    //bounds
                    if (player.XPos < 0)
                    {
                        player.XPos = 0;
                    }
                    if (player.XPos > GraphicsDevice.Viewport.Width - 130)
                    {
                        player.XPos = GraphicsDevice.Viewport.Width - 130;
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
                    foreach (SpikeballLauncher item in level.Launchers)
                    {
                        item.Launch(2);
                    }

                    //collision with spikes
                    foreach (Spike spikeObject in level.Spikes)
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

                    
                    break;
                #endregion
                #region gameOver
                case GameState.GameOver:
                    vspd = 0;
                    hspd = 0;
      

                    pMouseState = mouseState;
                    mouseState = Mouse.GetState();
                    kStatePrevious = kStateCurrent;
                   
                    //buttons are intersected with 
                    if (mouseRect.Intersects(mainMenRect))
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed && pMouseState.LeftButton == ButtonState.Released)
                        {
                            curr = GameState.Menu;

                        }
                    }
                    if (mouseRect.Intersects(contdRect))
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed && pMouseState.LeftButton == ButtonState.Released)
                        {
                            level.Player.XPos = level.PlayerSpawnX;
                            level.Player.YPos = level.PlayerSpawnY;
                            curr = GameState.Building;
                            NextLevel(levelCount);

                        }
                    }

                    break;
                #endregion
                #region Pause Menu
                case GameState.Pause:
                    pMouseState = mouseState;
                    mouseState = Mouse.GetState();
                    kStatePrevious = kStateCurrent;
                  
                    if (mouseRect.Intersects(button1Rect))
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed && pMouseState.LeftButton == ButtonState.Released)
                        {
                            curr = GameState.Game;

                     
                        }
                    }
                    if (mouseRect.Intersects(button2Rect))
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed && pMouseState.LeftButton == ButtonState.Released)
                        {
                            curr = GameState.Menu;

                       
                        }
                    }
                    break;
                #endregion
                #region final level 
                case GameState.LevelFinal:
                    vspd = 0;
                    hspd = 0;
                    kStatePrevious = kStateCurrent;
                    bool res5 = KeyPress(Keys.Enter);
                    if (LevelFile == levels[levels.Count - 1])
                    {
                        hasWon = true;
                        //level.Player.XPos = level.PlayerSpawnX;
                        //level.Player.YPos = level.PlayerSpawnY;
                        //add You Beat the Game! screen here
                    }
                    else
                    {
                        if (res5 == true)
                        {
                            levelCount += 1;
                            NextLevel(levelCount);
                            level.Player.LevelScore = 0;
                            curr = GameState.Building;
                        }
                    }
                    bool res6 = KeyPress(Keys.M);
                    if (res6 == true)
                    {
                        curr = GameState.Menu;
                    }
                    break;
                #endregion
                default:
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
                #region Main Menu
                case GameState.Menu:

                    spriteBatch.Draw(UITexture, UIRect, Color.White);
                    spriteBatch.DrawString(spriteFont, "Press enter to begin", new Vector2(603, 600), Color.Blue);
                  
                    break;
                #endregion
                #region Level Select
                case GameState.LevelSelect:
                    /*for (int i = 0; i < levelButtons.Count; i++)
                    {
                        spriteBatch.Draw(platform, levelButtons[i].Box, Color.White);
                        spriteBatch.DrawString(spriteFont, System.IO.Path.GetFileNameWithoutExtension(levelButtons[i].LevelName), new Vector2(levelButtons[i].XPos, levelButtons[i].YPos), Color.White);
                    }*/
                    spriteBatch.Draw(gameBG, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    spriteBatch.DrawString(spriteFont, "Select a level to play!", new Vector2(GraphicsDevice.Viewport.Width / 2 - 7, 50), Color.Blue);
                    for (int i = 0; i < 4; i++)
                    {
                        for(int j = 0; j < 3; j++)
                        {
                            Color textCo = Color.Blue;

                           
                            spriteBatch.Draw(sTexture, UIButts[i, j].Box, Color.White);
                            spriteBatch.DrawString(spriteFont, UIButts[i,j].Label, new Vector2(UIButts[i,j].XPos + 15, UIButts[i,j].YPos), textCo);

                        }
                    }
                    foreach(UIElement button in UIButts)
                    {

                    }
                    /*   foreach (UIElement button in UIButtons)
                       {
                           Color textColor = Color.Blue;
                           spriteBatch.Draw(platform, button.Box, Color.White);
                           if (button.LevelNum >= levels.Count || mouseRect.Intersects(button.Box))
                           {
                               textColor = Color.Red;
                           }
                           else
                           {
                               textColor = Color.Orange;
                           }
                       }*/
                    //spriteBatch.DrawString(spriteFont, "Level 1", new Vector2(100, 100), Color.Blue);
                    break;
                #endregion
                #region building Phase
                case GameState.Building:
                    // drawing inventory 
                    for (int i = 0; i < slots; i++)
                    {
                        slot[i].Draw(spriteBatch, spriteFont, sTexture);
                    }
                    
                    for (int i = 0; i < level.Platforms.Count; i++)
                    {
                        spriteBatch.Draw(level.Platforms[i].Texture, level.Platforms[i].Box, Color.Orange);
                    }


                    #region inventory aesthetics 
                    spriteBatch.Draw(inventorySplash, inventorySplashRect, Color.White);
                    spriteBatch.Draw(sTexture, instructionRect, Color.LawnGreen);
                    spriteBatch.DrawString(spriteFont, "Click on a slot below to get an item for Big Chungus to use!", new Vector2(250, 475), Color.Blue);
                   #endregion
                    spriteBatch.Draw(player.Texture, player.Box, new Rectangle(1019, 50, frameWidth, frameHeight), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    for (int i = 0; i < level.Carrots.Count; i++)
                    {
                        level.Carrots[i].Visible = true;
                        if (level.Carrots[i].Visible == true)
                        {
                            spriteBatch.Draw(level.Carrots[i].Texture, level.Carrots[i].Box, Color.White);
                        }
                    }
                    for (int i = 0; i < level.Spikes.Count; i++)
                    {
                        spriteBatch.Draw(level.Spikes[i].Texture, level.Spikes[i].Box, Color.White);
                    }
                    for (int i = 0; i < level.Springs.Count; i++)
                    {
                        spriteBatch.Draw(level.Springs[i].Texture, level.Springs[i].Box, Color.White);
                    }
                    for (int i = 0; i < level.Launchers.Count; i++)
                    {
                        spriteBatch.Draw(level.Launchers[i].Texture, level.Launchers[i].Box, Color.White);
                    }

                    spriteBatch.DrawString(spriteFont, "In Building Mode, use platforms and springs from your inventory to build a path, then press enter to begin the level", new Vector2(200, 50), Color.Blue);
                    spriteBatch.DrawString(spriteFont, "Mode: Building", new Vector2(GraphicsDevice.Viewport.Width - 200,100), Color.DarkBlue);

                    break;
                #endregion
                #region Play Game 
                case GameState.Game:
                    spriteBatch.Draw(gameBG, gameBGRect, Color.White);

                    #region animation 
                    if (current == animation.Idle)
                    {
                        spriteBatch.Draw(player.Texture, player.Box, new Rectangle(1019 + currentFrame.X * frameWidth, 50, frameWidth, frameHeight), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                        //spriteBatch.Draw(playerSprite, loc, new Rectangle(0, 0, frameWidth, frameHeight), Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                    }
                    if (current == animation.IdleLeft)
                    {
                        spriteBatch.Draw(player.Texture, player.Box, new Rectangle(1019 + currentFrame.X * frameWidth, 50, frameWidth, frameHeight), Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                        //spriteBatch.Draw(playerSprite, loc, new Rectangle(0, 0, frameWidth, frameHeight), Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                    }
                    if (current == animation.IdleRight)
                    {
                        spriteBatch.Draw(player.Texture, player.Box, new Rectangle(1019 + currentFrame.X * frameWidth, 50, frameWidth, frameHeight), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                        //spriteBatch.Draw(playerSprite, loc, new Rectangle(0, 0, frameWidth, frameHeight), Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                    }
                    {
                        spriteBatch.Draw(player.Texture, player.Box, new Rectangle(1019 + currentFrame.X * frameWidth, 370, frameWidth, frameHeight), Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                    }
                    if (current == animation.Right)
                    {
                        spriteBatch.Draw(player.Texture, player.Box, new Rectangle(1019 + currentFrame.X * frameWidth, 370, frameWidth, frameHeight), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    }
                    if (current == animation.JumpLeft)
                    {
                        spriteBatch.Draw(player.Texture, player.Box, new Rectangle(1019, 690, frameWidth, frameHeight), Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                    }
                    if (current == animation.JumpRight)
                    {
                        spriteBatch.Draw(player.Texture, player.Box, new Rectangle(1019, 690, frameWidth, frameHeight), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    }
                    #endregion
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
                    for(int i = 0; i < level.Springs.Count; i++)
                    {
                        spriteBatch.Draw(level.Springs[i].Texture, level.Springs[i].Box, Color.White);
                    }
                    for (int i = 0; i < level.Spikes.Count; i++)
                    {
                        spriteBatch.Draw(level.Spikes[i].Texture, level.Spikes[i].Box, Color.White);
                    }
                    for (int i = 0; i < level.Launchers.Count; i++)
                    {
                        spriteBatch.Draw(level.Launchers[i].Texture, level.Launchers[i].Box, Color.White);
                    }
           
                    #region UI 
                    spriteBatch.DrawString(spriteFont, "Mode: Game Mode", new Vector2(GraphicsDevice.Viewport.Width - 200,100), Color.DarkBlue);
                    spriteBatch.DrawString(spriteFont, "Walk:  Left and Right Arrows", new Vector2(50, 100), Color.Blue);
                    spriteBatch.DrawString(spriteFont, "Jump:  Up Arrow          hspd: " + hspd + "   vspd: " + vspd, new Vector2(50, 150), Color.Blue);
                    spriteBatch.DrawString(spriteFont, "Press P to pause and R to restart", new Vector2(50, 200), Color.Blue);
                    spriteBatch.DrawString(spriteFont, string.Format("Level {0}", levelCount+1), new Vector2(GraphicsDevice.Viewport.Width - 200, 50), Color.DarkBlue);
                    spriteBatch.DrawString(spriteFont, string.Format("carrots collected: {0}/{1}", player.LevelScore, level.Carrots.Count), new Vector2(GraphicsDevice.Viewport.Width - 200, 150), Color.DarkBlue);
                    #endregion
                    break;
                #endregion
                #region game over 
                case GameState.GameOver:
                    spriteBatch.Draw(gameOverTexture, gameOverRectangle, Color.White);
                    spriteBatch.Draw(continueButton, contdRect, Color.White);
                    spriteBatch.Draw(mainMenuButton, mainMenRect, Color.White);

                    break;
                #endregion

                #region Pause Screen 
                case GameState.Pause:
                    spriteBatch.Draw(pauseBG, pauseBGRect, Color.White);

                    spriteBatch.DrawString(spriteFont, "Click on an option to continue", new Vector2(480, 50), Color.DarkBlue);
                    spriteBatch.Draw(pauseTexture, pauseTextureRect, Color.White);
          
                    break;
                #endregion

                #region level final  
                case GameState.LevelFinal:
                    spriteBatch.Draw(gameWinScreen, gameOverRectangle, Color.White);
                    spriteBatch.DrawString(spriteFont, String.Format("TOTAL SCORE: {0}", player.LevelScore), new Vector2(300, 400), Color.DarkBlue);
                    spriteBatch.DrawString(spriteFont, "Congrats!", new Vector2(300, 200), Color.DarkBlue);
                    if (hasWon==true)
                    {
                        spriteBatch.DrawString(spriteFont, "Big Chungus has found all the carrots!", new Vector2(300, 300), Color.DarkBlue);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont, "Press enter for the next level", new Vector2(300, 300), Color.DarkBlue);
                    }
                    spriteBatch.DrawString(spriteFont, "Press M to exit to the main menu", new Vector2(300, 350), Color.DarkBlue);
                    break;
                #endregion
            }
            
            // End the sprite batch
            spriteBatch.End();
            base.Draw(gameTime);
        }

     
    }
}
