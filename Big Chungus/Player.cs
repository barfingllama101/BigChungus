using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Big_Chungus
{
    class Player
    {
        Rectangle playerBox=new Rectangle();

        int width = 200;
        int height = 200;
        int xPos;
        int yPos;

        public void player(int x, int y)
        {
            xPos = x;
            yPos = y;
        }
    }
}
