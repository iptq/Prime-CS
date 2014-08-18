﻿using System;
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
        }
    }
}
