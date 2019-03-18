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
        Rectangle Box { get; set; }
        int XPos { get; set; }
        int YPos { get; set; }

    }
}
