using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Prime
{
    class Enemy
    {
        Texture2D texture;
        Rectangle screenBounds;

        Vector2 size;
        Vector2 coords;
        Vector2 boardPos;

        int frames = 0;
        int dir = 0;

        public Enemy(Texture2D _texture, Rectangle _screenBounds)
        {
            texture = _texture;
            screenBounds = _screenBounds;
            SetPosition();
        }

        private void SetPosition()
        {
            size.Y = screenBounds.Height * 9 / 50;
            size.X = size.Y;

            coords.X = 1;
            coords.Y = 0;

            boardPos.X = (screenBounds.Width - screenBounds.Height * 9 / 10) / 2;
            boardPos.Y = (screenBounds.Height - screenBounds.Height * 9 / 10) / 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)(boardPos.X + size.X * coords.X), (int)(boardPos.Y + size.Y * coords.Y), (int)size.X, (int)size.Y), Color.White);
        }

        public void Update()
        {
            frames += 1;
            if (Prime.level < 7)
            {
                if (frames % 50 == 0)
                {
                    switch (dir)
                    {
                        case 0:
                            if (coords.Y > 0)
                            {
                                coords.Y -= 1;
                            }
                            else
                            {
                                coords.X += 1;
                                dir = 1;
                            }
                            break;
                        case 1:
                            if (coords.X < 4)
                            {
                                coords.X += 1;
                            }
                            else
                            {
                                coords.Y += 1;
                                dir = 2;
                            }
                            break;
                        case 2:
                            if (coords.Y < 4)
                            {
                                coords.Y += 1;
                            }
                            else
                            {
                                coords.X -= 1;
                                dir = 3;
                            }
                            break;
                        case 3:
                            if (coords.X > 0)
                            {
                                coords.X -= 1;
                            }
                            else
                            {
                                coords.Y -= 1;
                                dir = 0;
                            }
                            break;
                        default:
                            // wht the actual fuck
                            break;
                    }
                }
            }
        }
    }
}
