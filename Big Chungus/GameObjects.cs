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
    class GameObjects
    {
        // Attributes
        protected Texture2D obj;
        protected Rectangle rec;

        // properties
        public Texture2D Obj
        {
            get { return obj; }
            set { obj = value; }
        }
        public Rectangle Rec
        {
            get { return rec; }
            set { rec = value; }
        }
        public int xPos
        {
            get { return rec.X; }
            set { rec.X = value; }
        }
        public int yPos
        {
            get { return rec.Y; }
            set { rec.Y = value; }
        }

        //Constructor
        public GameObjects(Texture2D texture, int x, int y, int width, int height)
        {
            obj = texture;
            rec = new Rectangle(x, y, width, height);
        }
    }
}
