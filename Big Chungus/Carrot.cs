using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Big_Chungus
{
    class Carrot : GameObject
    {
        private Texture2D carrotTexture;
        private Rectangle carrotBox;
        private bool isCollected = false;
        protected bool visible = true;


        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }
        public Texture2D Texture { get => carrotTexture; set => carrotTexture = value; }
        public int XPos { get => carrotBox.X; set => carrotBox.X = value; }
        public int YPos { get => carrotBox.Y; set => carrotBox.Y = value; }
        public Rectangle Box { get => carrotBox; set => carrotBox = value; }
        public bool IsCollected { get => isCollected; set => isCollected = value; }

        public Carrot(Texture2D texture, int x, int y, int width, int height)
        {
            //new GameObjects(texture, x, y, width, height);
            carrotTexture = texture;
            carrotBox = new Rectangle(x, y, width, height);
            visible = true;
        }

        /*public Carrot(Texture2D texture, int width, int height)
        {
            carrotTexture = texture;
            carrotBox = new Rectangle();
            carrotBox.Width = width;
            carrotBox.Height = height;
            visible = true;
        }*/

        public bool CheckCollision(Rectangle O)
        {
            bool result = false;
            if (visible == true)
            {
                if (O.Intersects(Box))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
