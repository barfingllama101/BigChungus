using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Big_Chungus
{
    class Carrot
    {
        private Texture2D carrotTexture;
        private int xPos;
        private int yPos;
        private Rectangle carrotBox;

        public Texture2D CarrotTexture { get => carrotTexture; set => carrotTexture = value; }
        public int XPos { get => xPos; set => xPos = value; }
        public int YPos { get => yPos; set => yPos = value; }
        public Rectangle CarrotBox { get => carrotBox; set => carrotBox = value; }

        public Carrot(int x, int y)
        {
            xPos = x;
            yPos = y;
        }

        public void checkCollision(Rectangle thing)
        {
            
        }
    }
}
