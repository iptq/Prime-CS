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
            else
            {
                // complicated formula that does nothing
                int delay = (int)Math.Floor((Math.Floor(-1 * (Math.Log(Prime.level) / (Math.Log(15) - Math.Log(14))) + 65.5)));
                if (frames % delay == 0)
                {
                    int px = (int)Prime.player.coords.X;
                    int py = (int)Prime.player.coords.Y;
                    int ex = (int)coords.X;
                    int ey = (int)coords.Y;
                    if (px == ex && py != ey)
                    {
                        if (py < ey)
                            ey -= 1;
                        else if (py > ey)
                            ey += 1;
                    }
                    else if (py == ey && px != ex)
                    {
                        if (px < ex)
                            ex -= 1;
                        else if (px > ex)
                            ex += 1;
                    }
                    else
                    {
                        int area1 = 0, area2 = 0;
                        if (px < ex)
                        {
                            if (py > ey)
                            {
                                area1 = (ex) * (5 - ey); // UP;
                                area2 = (ex + 1) * (4 - ey); // RIGHT;
                            }
                            else if (py < ey)
                            {
                                area1 = (ex) * (ey + 1); // UP;
                                area2 = (ex + 1) * (ey); // LEFT;
                            }
                        }
                        else if (px > ex)
                        {
                            if (py > ey)
                            {
                                area1 = (4 - ex) * (5 - ey); // DOWN;
                                area2 = (5 - ex) * (4 - ey); // RIGHT;
                            }
                            else if (py < ey)
                            {
                                area1 = (4 - ex) * (ey + 1); // DOWN;
                                area2 = (5 - ey) * (ey); // LEFT;
                            }
                        }

                        if (area1 > area2)
                        { // VERTICAL
                            if (px < ex)
                                ex -= 1;
                            else if (px > ex)
                                ex += 1;
                        }
                        else if (area2 > area1)
                        { // HORIZONTAL
                            if (py < ey)
                                ey -= 1;
                            else if (py > ey)
                                ey += 1;
                        }
                        else if (area1 == area2)
                        { // RANDOM
                            if (Prime.rand.Next(1, 3) == 1)
                            {
                                if (px < ex)
                                    ex -= 1;
                                else if (px > ex)
                                    ex += 1;
                            }
                            else
                            {
                                if (py < ey)
                                    ey -= 1;
                                else if (py > ey)
                                    ey += 1;
                            }
                        }
                    }
                }
            }
        }
    }
}
