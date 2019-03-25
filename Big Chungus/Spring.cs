using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Big_Chungus
{
    class Spring : Platform
    {

        private Texture2D springTexture;
        private Rectangle springBox;

        public Texture2D SpringTexture { get => springTexture; set => springTexture = value; }
        public int XPos { get => springBox.X; set => springBox.X = value; }
        public int YPos { get => springBox.Y; set => springBox.Y = value; }
        public Rectangle Box { get => springBox; set => springBox = value; }
        public int Width { get => Box.Width; set => springBox.Width = value; }
        public int Height { get => Box.Height; set => springBox.Height = value; }

        public Spring(Texture2D texture, int x, int y, int width, int height)
            : base(texture, x, y, width, height)
        {
            springTexture = texture;
            springBox = new Rectangle(x, y, width, height);
        }

        //set spring center to cursor position when dragging
        public void Drag()
        {
            MouseState mouseState = Mouse.GetState();
            XPos = mouseState.X - Width / 2;
            YPos = mouseState.Y - Height / 2;
        }

    }
}
