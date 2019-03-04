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
        private Rectangle carrotBox;
        private bool isCollected;

        public Texture2D CarrotTexture { get => carrotTexture; set => carrotTexture = value; }
        public int XPos { get => carrotBox.X; set => carrotBox.X = value; }
        public int YPos { get => carrotBox.Y; set => carrotBox.Y = value; }
        public Rectangle CarrotBox { get => carrotBox; set => carrotBox = value; }
        public bool IsCollected { get => isCollected; set => isCollected = value; }

        public Carrot(Texture2D texture, int x, int y, int width, int height)
        {
            carrotTexture = texture;
            carrotBox = new Rectangle(x, y, width, height);
        }

        public void checkCollision(Rectangle thing)
        {
            
        }
    }
}
