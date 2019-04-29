using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Big_Chungus
{
    class UIElement
    {
        private int levelNum = 0;
        private Rectangle box;
        private int xPos;
        private int yPos;
        private string label;
        public int LevelNum { get => levelNum; set => levelNum = value; }
        
        public Rectangle Box { get => box; set => box = value; }
        public int XPos { get => box.X; set => box.X = value; }
        public int YPos { get => box.Y; set => box.Y = value; }
        public string Label { get => label; set => label = value; }

        public UIElement(int num, int x, int y)
        {
            levelNum = num;
            xPos = x;
            yPos = y;
            box = new Rectangle(xPos, yPos, 300, 15);
            label = "Level " + (levelNum + 1);
        }

        public void Drag()
        {
            throw new NotImplementedException();
        }
    }
}
