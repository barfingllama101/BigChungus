using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Big_Chungus
{
    class ItemBar {
        public Texture2D itembarStuff, iSelected;
        public static Rectangle itemBar = new Rectangle(0, 50, 300, 600);
        public Rectangle box1 = new Rectangle(itemBar.X, 218, 40, 40);
        public int Selected = 0;
        //private ItemManager manager;


        //loadfrom textfile
    }
}
