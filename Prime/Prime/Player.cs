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
        Vector2 coords;
        Vector2 boardPos;

        KeyboardState keyboardState;
        GamePadState gamePadState;

        bool[] KeyDown = new bool[4] { false, false, false, false }; // up, right, down, left, just like in CSS

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
            UpdateKeys();

            if (KeyDown[1] && coords.X < 4)
            {
                coords.X += 1;
                KeyDown[1] = false;
            }
            if (KeyDown[3] && coords.X > 0)
            {
                coords.X -= 1;
                KeyDown[3] = false;
            }
        }

        public void UpdateKeys()
        {
            keyboardState = Keyboard.GetState();
            gamePadState = GamePad.GetState(PlayerIndex.One);

            if (!KeyDown[0] && (keyboardState.IsKeyDown(Keys.Up) || gamePadState.IsButtonDown(Buttons.DPadUp) || gamePadState.IsButtonDown(Buttons.LeftThumbstickUp)))
            {
                KeyDown[0] = true;
            }
            else
            {
                KeyDown[0] = false;
            }
            if (!KeyDown[1] && (keyboardState.IsKeyDown(Keys.Right) || gamePadState.IsButtonDown(Buttons.DPadRight) || gamePadState.IsButtonDown(Buttons.LeftThumbstickRight)))
            {
                KeyDown[1] = true;
            }
            else
            {
                KeyDown[1] = false;
            }
            if (!KeyDown[2] && (keyboardState.IsKeyDown(Keys.Down) || gamePadState.IsButtonDown(Buttons.DPadDown) || gamePadState.IsButtonDown(Buttons.LeftThumbstickDown)))
            {
                KeyDown[2] = true;
            }
            else
            {
                KeyDown[2] = false;
            }
            if (!KeyDown[3] && (keyboardState.IsKeyDown(Keys.Left) || gamePadState.IsButtonDown(Buttons.DPadLeft) || gamePadState.IsButtonDown(Buttons.LeftThumbstickLeft)))
            {
                KeyDown[3] = true;
            }
            else
            {
                KeyDown[3] = false;
            }
        }
    }
}
