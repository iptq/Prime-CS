using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Prime
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    class Prime : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public static Board board;
        public static Player player;
        public static Enemy enemy;
        public static Rectangle screenRectangle;

        public static InputHelper helper;

        public static Texture2D[] numberTextures = new Texture2D[10];

        public static int score = 0;
        public static int displayScore = 0;
        public static int level = 1;
        public static int nextLevel = 6;
        public static int percent = 0;
        public static int displayPercent = 0;

        public const string rektStatus = "rekt";

        public static int upLevel = 0;
        public static bool lost = false;

        public Prime()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            screenRectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            helper = new InputHelper();
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

            board = new Board(Content.Load<Texture2D>("board"), screenRectangle);
            player = new Player(Content.Load<Texture2D>("player"), screenRectangle);
            enemy = new Enemy(Content.Load<Texture2D>("enemy"), screenRectangle);

            for (int i = 0; i < 10; i++)
            {
                numberTextures[i] = Content.Load<Texture2D>("numbers/default-" + i);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            helper.Update();
            player.Update();
            enemy.Update();
            board.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            board.Draw(spriteBatch);
            player.Draw(spriteBatch);
            enemy.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
