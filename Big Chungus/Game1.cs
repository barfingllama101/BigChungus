using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        Carrot carrot;
        Texture2D player;
        Texture2D dog;
        SpriteFont spriteFont;
        Rectangle playerRect;
        Platform platform=new Platform(300, 430);

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
            
            hspd = 0;
            vspd = 0;
            hacc = 1;
            grav = 1;
            hmax = 7;
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
            carrot = new Carrot(200, 100);
            playerRect = new Rectangle();
            playerRect.Width = 201;
            playerRect.Height = 201;
            
            carrot.CarrotTexture= Content.Load<Texture2D>("Carrot");
            player = Content.Load<Texture2D>("BigChungus");
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            dog= Content.Load<Texture2D>("SmilingPetDog");
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
            ProcessInput();
            // TODO: Add your update logic here

            //update player position based on hspeed and vspeed
            playerRect.X += hspd;
            playerRect.Y += vspd;

            base.Update(gameTime);
        }

        protected void ProcessInput()
        {
            KeyboardState input = Keyboard.GetState();

            //gravity
            if (!playerRect.Intersects(platform.PlatformBox))
            {
                vspd += grav;
            }
            else
            {
                vspd = 0;
            }
            if (playerRect.Y > 231)
            {
                playerRect.Y = 231;
            }
            if (playerRect.Intersects(platform.PlatformBox))
            {
                playerRect.Y = platform.YPos-playerRect.Height;
            }

            //jump
            if (input.IsKeyDown(Keys.Up) && (playerRect.Y == 231))
            {

                vspd = -16;
                
            }

            //left and right movement/deceleration
            if (input.IsKeyDown(Keys.Left) && hspd > -hmax)
            {
                hspd -= hacc;
            }
            else if (hspd < 0)
            {
                hspd += hacc;
            }
            if (input.IsKeyDown(Keys.Right) && hspd < hmax)
            {
                hspd += hacc;
            }
            else if (hspd > 0)
            {
                hspd -= hacc;
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
            spriteBatch.Draw(player, playerRect, Color.White);

            spriteBatch.DrawString(spriteFont, "such text, very picture, much input, wow", new Vector2(player.Width / 2, player.Height / 2), Color.White);

            spriteBatch.DrawString(spriteFont, playerRect.X + ", " + playerRect.Y, new Vector2(0, 100), Color.White);
            spriteBatch.Draw(dog, platform.PlatformBox, Color.White);


            // End the sprite batch
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
