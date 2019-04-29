using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Big_Chungus
{
    class LevelButton
    {
        //give the button a level file
        protected string levelName;
        protected Rectangle box;

        public string LevelName
        {
            get { return levelName; }
        }
        public Rectangle Box { get => box; set => box = value; }
        public int XPos { get => box.X; set => box.X = value; }
        public int YPos { get => box.Y; set => box.Y = value; }

        //constructor
        public LevelButton(string level, int x, int y)
        {
            levelName = level;
            Box = new Rectangle(x, y, 100, 20);
        }
    }
}
