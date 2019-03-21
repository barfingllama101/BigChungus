using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Big_Chungus
{
    class Player:GameObject
    {
        private Texture2D playerTexture;
        private Rectangle playerBox;
        protected int levelScore;
        private bool isStanding = false;

        public int LevelScore
        {
            get { return levelScore; }
            set { levelScore = value; }
        }
        public Rectangle Box { get => playerBox; set => playerBox = value; }
        public int Width { get => playerBox.Width; set => playerBox.Width = value; }
        public int Height { get => playerBox.Height; set => playerBox.Height = value; }
        public int XPos { get => playerBox.X; set => playerBox.X = value; }
        public int YPos { get => playerBox.Y; set => playerBox.Y = value; }
        public Texture2D PlayerTexture { get => playerTexture; set => playerTexture = value; }
        public bool IsStanding { get => isStanding; set => isStanding = value; }

        public Player(Texture2D texture, int x, int y)
        {
            playerTexture = texture;
            playerBox = new Rectangle(x, y, texture.Width, texture.Height);
            levelScore = 0;
        }

        public bool standingCheck(List<Platform> platformList)
        {
            for (int i = 0; i < platformList.Count; i++)
            {
                if (Box.Intersects(platformList[i].Box))
                {
                    isStanding = true;
                    break;
                }
                isStanding = false;
            }
            return isStanding;
        }

        public bool CheckCollision(Rectangle O)
        {
            bool result = true;
            if (O.Intersects(playerBox))
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}
