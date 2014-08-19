using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Prime
{
    class Player
    {
        Texture2D texture;
        Rectangle screenBounds;

        Vector2 size;
        public Vector2 coords;
        Vector2 boardPos;

        public Player(Texture2D _texture, Rectangle _screenBounds)
        {
            texture = _texture;
            screenBounds = _screenBounds;
            SetPosition();
        }

        public void SetPosition()
        {
            size.Y = screenBounds.Height * 9 / 50;
            size.X = size.Y;

            coords.X = 2;
            coords.Y = 2;

            boardPos.X = (screenBounds.Width - screenBounds.Height * 9 / 10) / 2;
            boardPos.Y = (screenBounds.Height - screenBounds.Height * 9 / 10) / 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)(boardPos.X + size.X * coords.X), (int)(boardPos.Y + size.Y * coords.Y), (int)size.X, (int)size.Y), Color.White);
        }

        public void Update()
        {
            if (Prime.helper.IsNewPress(Keys.Space) || Prime.helper.IsNewPress(Buttons.A))
            {
                Eat();
            }

            Calc();

            if (coords.X > 0 && (Prime.helper.IsNewPress(Keys.Left) || Prime.helper.IsNewPress(Buttons.LeftThumbstickLeft) || Prime.helper.IsNewPress(Buttons.DPadLeft)))
            {
                coords.X -= 1;
            }
            if (coords.X < 4 && (Prime.helper.IsNewPress(Keys.Right) || Prime.helper.IsNewPress(Buttons.LeftThumbstickRight) || Prime.helper.IsNewPress(Buttons.DPadRight)))
            {
                coords.X += 1;
            }
            if (coords.Y > 0 && (Prime.helper.IsNewPress(Keys.Up) || Prime.helper.IsNewPress(Buttons.LeftThumbstickUp) || Prime.helper.IsNewPress(Buttons.DPadUp)))
            {
                coords.Y -= 1;
            }
            if (coords.Y < 4 && (Prime.helper.IsNewPress(Keys.Down) || Prime.helper.IsNewPress(Buttons.LeftThumbstickDown) || Prime.helper.IsNewPress(Buttons.DPadDown)))
            {
                coords.Y += 1;
            }
        }

        public void Eat()
        {
            int index = (int)coords.Y * 5 + (int)coords.X;
            int N = Prime.board.Numbers[index];

            if (Prime.primes.Contains(N))
            {
                // yay
                Prime.score += Prime.level;
                Prime.board.Numbers[index] += Prime.rand.Next(Prime.level, 2 * Prime.level);
            }
            else
            {
                // gg
                Prime.score -= Prime.level / 2;
            }
        }

        public void Calc()
        {
            if (Prime.percent > 100)
            {
                Prime.percent = 0;
                Prime.level += 1;
                Prime.upLevel = 5;
            }
            if (Prime.percent < 0)
            {
                Prime.percent = 0;
            }

            Prime.nextLevel = 3 * Prime.level * (Prime.level + 1);
            Prime.percent = Prime.score * 100 / Prime.nextLevel;

            if (Prime.displayPercent != Prime.percent)
            {
                if (Math.Abs(Prime.displayPercent - Prime.percent) < 2)
                {
                    Prime.displayPercent = Prime.percent;
                }
                else
                {
                    Prime.displayPercent = (Prime.displayPercent + Prime.percent) / 2;
                }
            }
        }
    }
}
