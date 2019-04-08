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
    class FireballLauncher : GameObject
    {
        private Texture2D texture;
        private Rectangle box;

        public Rectangle Box { get => box; set => box = value; }
        public int XPos { get => box.X; set => box.X = value; }
        public int YPos { get => box.Y; set => box.Y = value; }

        public FireballLauncher(Texture2D newTexture, int newX, int newY, int width, int height)
        {
            texture = newTexture;
            XPos = newX;
            YPos = newY;
            box = new Rectangle(XPos, YPos, width, height);
        }
    }
}
