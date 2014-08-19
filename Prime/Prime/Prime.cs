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
using System.Threading.Tasks;

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

        public static List<int> primes = new List<int>();

        public static Random rand = new Random();

        Texture2D meterTex;
        Texture2D meterFillTex;

        public Prime()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            screenRectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            helper = new InputHelper();
            primes = atkin(10000);

            Console.WriteLine("found " + primes.Count + " primes.");
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

            meterTex = Content.Load<Texture2D>("meter");
            meterFillTex = Content.Load<Texture2D>("meter-fill");

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

            CheckBoard();

            helper.Update();
            player.Update();
            enemy.Update();
            board.Update();

            base.Update(gameTime);
        }

        private void CheckBoard()
        {
            while (NumberOfPrimesRemainingOnBoard() <= 3)
            {
                board.GenerateNumbers();
            }
        }

        private int NumberOfPrimesRemainingOnBoard()
        {
            int count = 0;
            for (int i = 0; i < 25; i++)
            {
                if (primes.Contains(board.Numbers[i]))
                {
                    count += 1;
                }
            }
            return count;
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

            int h = screenRectangle.Height * 9 / 10;
            int w = h / 8;
            int x = screenRectangle.Width - w - (screenRectangle.Height - h) / 2;
            int y = (screenRectangle.Height - h) / 2;
            spriteBatch.Draw(meterTex, new Rectangle(x, y, w, h), Color.White);

            int h1 = displayPercent * (h - 10) / 100;
            w -= 10;
            x += 5;
            y += 5 + (100 - displayPercent) * (h - 10) / 100;
            spriteBatch.Draw(meterFillTex, new Rectangle(x, y, w, h1), Color.White);
            
            /*
             * DEBUG
            percent += 1;
            percent %= 100;
             */

            player.Draw(spriteBatch);
            enemy.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
        static List<int> atkin(int max)
        {
            //  var isPrime = new BitArray((int)max+1, false); 
            //  Can't use BitArray because of threading issues.
            var isPrime = new bool[max + 1];
            var sqrt = (int)Math.Sqrt(max);

            Parallel.For(1, sqrt, x =>
            {
                var xx = x * x;
                for (int y = 1; y <= sqrt; y++)
                {
                    var yy = y * y;
                    var n = 4 * xx + yy;
                    if (n <= max && (n % 12 == 1 || n % 12 == 5))
                        isPrime[n] ^= true;

                    n = 3 * xx + yy;
                    if (n <= max && n % 12 == 7)
                        isPrime[n] ^= true;

                    n = 3 * xx - yy;
                    if (x > y && n <= max && n % 12 == 11)
                        isPrime[n] ^= true;
                }
            });

            var primes = new List<int>() { 2, 3 };
            for (int n = 5; n <= sqrt; n++)
            {
                if (isPrime[n])
                {
                    primes.Add(n);
                    int nn = n * n;
                    for (int k = nn; k <= max; k += nn)
                        isPrime[k] = false;
                }
            }

            for (int n = sqrt + 1; n <= max; n++)
                if (isPrime[n])
                    primes.Add(n);

            return primes;
        }
    }
}
