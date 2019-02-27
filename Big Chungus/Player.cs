using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Big_Chungus
{
    class Player
    {
        private Texture2D playerTexture;
        private Rectangle playerBox;

        private int width;
        private int height;
        private int xPos;
        private int yPos;

        public Rectangle PlayerBox { get => playerBox; set => playerBox = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public int XPos { get => xPos; set => xPos = value; }
        public int YPos { get => yPos; set => yPos = value; }
        public Texture2D PlayerTexture { get => playerTexture; set => playerTexture = value; }

        public Player(Texture2D texture, int x, int y)
        {
            playerTexture = texture;
            xPos = x;
            yPos = y;
            Width = texture.Width;
            Height = texture.Height;
            playerBox = new Rectangle(x, y, texture.Width, texture.Height);
        }
    }
}
