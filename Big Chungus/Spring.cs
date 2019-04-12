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

    public enum platformType
    {
        platform, 
        spike
    }
    class Spring : Platform, GameObject
    {
        private Texture2D springTexture;
        private Rectangle springBox;

        public Texture2D SpringTexture { get => springTexture; set => springTexture = value; }
        public new int XPos { get => springBox.X; set => springBox.X = value; }
        public new int YPos { get => springBox.Y; set => springBox.Y = value; }
        public new Rectangle Box { get => springBox; set => springBox = value; }
        public new int Width { get => Box.Width; set => springBox.Width = value; }
        public new int Height { get => Box.Height; set => springBox.Height = value; }

        public Spring(Texture2D texture, int x, int y, int width, int height)
            : base(texture, x, y, width, height)
        {
            springTexture = texture;
            springBox = new Rectangle(x, y, width, height);
        }

        /*public Spring(Texture2D texture, int width, int height)
            : base(texture, width, height)
        {
            springTexture = texture;
            springBox = new Rectangle();
            springBox.Width = width;
            springBox.Height = height;
        }*/
        //set spring center to cursor position when dragging
        /*public void Drag()
        {
            MouseState mouseState = Mouse.GetState();
            XPos = mouseState.X - Width / 2;
            YPos = mouseState.Y - Height / 2;
        }*/

    }
}
