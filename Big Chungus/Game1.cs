using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Big_Chungus
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {/*Author:  Maxwell Hazel
         Class Purpose:  Runs a Monogame program with an image that can be moved with the arrow keys.  Also displays the location of the image.
         Caveats:  The location coordinates displayed in the program only track the top left corner of the image.*/
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background;
        Vector2 vector;
        SpriteFont spriteFont;
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
            vector = new Vector2(0, 0);
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
            background = Content.Load<Texture2D>("SmilingPetDog");
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
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
            //vector.X += 2;
            base.Update(gameTime);
        }

        protected void ProcessInput()
        {
            KeyboardState input = Keyboard.GetState();
            if (vector.Y<0)
            {
                vector.Y += 1;
            }
            if (input.IsKeyDown(Keys.Up)&&(vector.Y==0))
            {

                vector.Y -= 10;
                
            }
            
            if (input.IsKeyDown(Keys.Left))
            {
                vector.X -= 2;
            }
            if (input.IsKeyDown(Keys.Right))
            {
                vector.X += 2;
            }


        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            // Draw
            spriteBatch.Draw(background, vector, Color.White);

            spriteBatch.DrawString(spriteFont, "such text, very picture, much input, wow", new Vector2(background.Width / 2, background.Height / 2), Color.Green);

            spriteBatch.DrawString(spriteFont, vector.X + ", " + vector.Y, new Vector2(0, 100), Color.Green);

            // End the sprite batch
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
