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
        Texture2D Texture { get; set; }
        Rectangle Box { get; set; }

        int XPos { get; set; }
        int YPos { get; set; }
        bool IsMoveable { get; set; }
        void Drag();

    }
}
