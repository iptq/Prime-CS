using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Prime
{
    class Board
    {
        Vector2 position;
        Vector2 size;
        int[] Numbers = new int[25];

        KeyboardState keyboardState;
        GamePadState gamePadState;

        Texture2D texture;
        Rectangle screenBounds;

        public Board(Texture2D _texture, Rectangle _screenBounds)
        {
            texture = _texture;
            screenBounds = _screenBounds;
            SetPosition();
            GenerateNumbers();
        }

        private void GenerateNumbers()
        {
            for (int i = 0; i < 25; i++)
            {
                Numbers[i] = (new Random()).Next(1, 21);
            }
        }

        private void SetPosition()
        {
            size.Y = screenBounds.Height * 9 / 10;
            size.X = size.Y;

            position.X = (screenBounds.Width - size.X) / 2;
            position.Y = (screenBounds.Height - size.Y) / 2;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle dest = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            spriteBatch.Draw(texture, dest, Color.White);

            for (int i = 0; i < 25; i++)
            {
                int row = (int)(i / 5);
                int col = i % 5;

                int digits = (int)(Math.Ceiling((double)Numbers[i]));
                char[] d = Numbers[i].ToString().ToCharArray();

                Vector2 numSize = new Vector2(size.X/20,size.X*64/58/20);
                if (digits < 4)
                {
                    List<Rectangle> dst = new List<Rectangle>();
                    switch (digits)
                    {
                        case 1:
                            Rectangle r = new Rectangle((int)(position.X + col * size.X / 5 + size.X / 10 - numSize.X / 2), (int)(position.Y + row * size.Y / 5 + size.Y / 10 - numSize.Y / 2), (int)numSize.X, (int)numSize.Y);
                            dst.Add(r);

                            spriteBatch.Draw(Game1.numberTextures[int.Parse(d.ToString())], r, Color.White);
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                    }
                    for (int j = 0; j < digits; j++)
                    {
                        spriteBatch.Draw(Game1.numberTextures[int.Parse(d.ToString())], dst[j], Color.White);
                    }
                }
                else
                {
                    // gg
                }
            }
        }
    }
}
