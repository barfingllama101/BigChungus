using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Big_Chungus
{
    interface GameObject
    {
        /*protected int xPos=0;
        protected int yPos=0;
        public Rectangle box;*/
        Rectangle Box { get; set; }
        int XPos { get; set; }
        int YPos { get; set; }

        //public int XPos { get => box.X; set => box.X = value; }
        //public int YPos { get => box.Y; set => box.Y = value; }

    }
}
