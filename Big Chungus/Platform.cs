using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Big_Chungus
{
    class Platform
    {
        //Fields
        private Rectangle platformBox;
        private int width = 300;
        private int height = 4;
        private int xPos;
        private int yPos;

        //Properties
        public int Width { get => width; }
        public int Height { get => height; }
        public int YPos { get => yPos; set => yPos = value; }
        public int XPos { get => xPos; set => xPos = value; }
        public Rectangle PlatformBox { get { return platformBox; } }

        public Platform(int x, int y)
        {
            xPos = x;
            yPos = y;
            platformBox.X = x;
            platformBox.Y = y;
            platformBox.Width = width;
            platformBox.Height = height;
        }

        public void Drag(EventArgs e)
        {

        }

    }
}
