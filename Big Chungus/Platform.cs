using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private Texture2D platformTexture;
        private Rectangle platformBox;
        
        //Properties
        public int Width { get => platformBox.Width; set => platformBox.Width = value; }
        public int Height { get => platformBox.Height; set => platformBox.Height = value; }
        public int YPos { get => platformBox.Y; set => platformBox.Y = value; }
        public int XPos { get => platformBox.X; set => platformBox.X = value; }
        public Rectangle PlatformBox { get => platformBox; set => platformBox = value; }

        public Platform(Texture2D texture, int x, int y, int width, int height)
        {
            platformTexture = texture;
            platformBox = new Rectangle(x, y, width, height);
        }

        public void Drag(EventArgs e)
        {

        }

    }
}
