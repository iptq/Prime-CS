﻿using System;
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

        Texture2D texture;
        Rectangle screenBounds;

        Random rand = new Random();

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
                Numbers[i] = rand.Next(1, 21);
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

                String d = Numbers[i].ToString();
                int digits = d.Length;
                // Console.WriteLine(d + " is " + digits + " digits long.");

                Vector2 numSize = new Vector2(size.X / 20, size.X * 64 / 58 / 20);
                if (digits < 4)
                {
                    switch (digits)
                    {
                        case 1:
                            spriteBatch.Draw(Game1.numberTextures[int.Parse(d.Substring(0, 1))], new Rectangle((int)(position.X + col * size.X / 5 + size.X / 10 - numSize.X / 2), (int)(position.Y + row * size.Y / 5 + size.Y / 10 - numSize.Y / 2), (int)numSize.X, (int)numSize.Y), Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(Game1.numberTextures[int.Parse(d.Substring(0, 1))], new Rectangle((int)(position.X + col * size.X / 5 + size.X / 10 - numSize.X), (int)(position.Y + row * size.Y / 5 + size.Y / 10 - numSize.Y / 2), (int)numSize.X, (int)numSize.Y), Color.White);
                            spriteBatch.Draw(Game1.numberTextures[int.Parse(d.Substring(1, 1))], new Rectangle((int)(position.X + col * size.X / 5 + size.X / 10), (int)(position.Y + row * size.Y / 5 + size.Y / 10 - numSize.Y / 2), (int)numSize.X, (int)numSize.Y), Color.White);
                            break;
                        case 3:
                            spriteBatch.Draw(Game1.numberTextures[int.Parse(d.Substring(0, 1))], new Rectangle((int)(position.X + col * size.X / 5 + size.X / 10 - numSize.X / 2 - numSize.X), (int)(position.Y + row * size.Y / 5 + size.Y / 10 - numSize.Y / 2), (int)numSize.X, (int)numSize.Y), Color.White);
                            spriteBatch.Draw(Game1.numberTextures[int.Parse(d.Substring(1, 1))], new Rectangle((int)(position.X + col * size.X / 5 + size.X / 10 - numSize.X / 2), (int)(position.Y + row * size.Y / 5 + size.Y / 10 - numSize.Y / 2), (int)numSize.X, (int)numSize.Y), Color.White);
                            spriteBatch.Draw(Game1.numberTextures[int.Parse(d.Substring(2, 1))], new Rectangle((int)(position.X + col * size.X / 5 + size.X / 10 - numSize.X / 2 + numSize.X), (int)(position.Y + row * size.Y / 5 + size.Y / 10 - numSize.Y / 2), (int)numSize.X, (int)numSize.Y), Color.White);
                            break;
                        case 4:
                            spriteBatch.Draw(Game1.numberTextures[int.Parse(d.Substring(0, 1))], new Rectangle((int)(position.X + col * size.X / 5 + size.X / 10 - 2 * numSize.X), (int)(position.Y + row * size.Y / 5 + size.Y / 10 - numSize.Y / 2), (int)numSize.X, (int)numSize.Y), Color.White);
                            spriteBatch.Draw(Game1.numberTextures[int.Parse(d.Substring(1, 1))], new Rectangle((int)(position.X + col * size.X / 5 + size.X / 10 - numSize.X), (int)(position.Y + row * size.Y / 5 + size.Y / 10 - numSize.Y / 2), (int)numSize.X, (int)numSize.Y), Color.White);
                            spriteBatch.Draw(Game1.numberTextures[int.Parse(d.Substring(2, 1))], new Rectangle((int)(position.X + col * size.X / 5 + size.X / 10), (int)(position.Y + row * size.Y / 5 + size.Y / 10 - numSize.Y / 2), (int)numSize.X, (int)numSize.Y), Color.White);
                            spriteBatch.Draw(Game1.numberTextures[int.Parse(d.Substring(3, 1))], new Rectangle((int)(position.X + col * size.X / 5 + size.X / 10 + numSize.X), (int)(position.Y + row * size.Y / 5 + size.Y / 10 - numSize.Y / 2), (int)numSize.X, (int)numSize.Y), Color.White);
                            break;
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
