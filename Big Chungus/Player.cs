using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Big_Chungus
{
    class Player : GameObject
    {
        private Texture2D playerTexture;
        private Rectangle playerBox;
        protected int levelScore;
        private bool isStanding = false;
        private bool isMoveable = true;

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
        public Texture2D Texture { get => playerTexture; set => playerTexture = value; }
        public bool IsStanding { get => isStanding; set => isStanding = value; }
        public bool IsMoveable { get => isMoveable; set => isMoveable = value; }
        public bool IsVisible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Player(Texture2D texture)
        {
            playerTexture = texture;
            playerBox = new Rectangle();
            playerBox.Width = texture.Width;
            playerBox.Height = texture.Height;
            levelScore = 0;
        }
        public Player(Texture2D texture, int x, int y)
        {
            playerTexture = texture;
            playerBox = new Rectangle(x, y, 200, 200);
            levelScore = 0;
        }

        public bool standingCheck(List<Platform> platformList)
        {
            //checks if the pixel directly below the player is intersected by a platform
            for (int i = 0; i < platformList.Count; i++)
            {
                if (new Rectangle(XPos, YPos + 1, Width, Height).Intersects(platformList[i].Box))
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
        //set player center to cursor position when dragging
        public void Drag()
        {
            MouseState mouseState = Mouse.GetState();
            XPos = mouseState.X - playerBox.Width / 2;
            YPos = mouseState.Y - playerBox.Height / 2;
        }
    }
}
