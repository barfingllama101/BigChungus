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
    class Spike : GameObject
    {
        private Texture2D spikeTexture;
        private Rectangle spikeBox;
        private bool isMoveable = false;

        public Texture2D Texture { get => spikeTexture; set => spikeTexture = value; }
        public int XPos { get => spikeBox.X; set => spikeBox.X = value; }
        public int YPos { get => spikeBox.Y; set => spikeBox.Y = value; }
        public Rectangle Box { get => spikeBox; set => spikeBox = value; }
        public bool IsMoveable { get => isMoveable; set => isMoveable = value; }

        public Spike(Texture2D texture, int x, int y, int width, int height)
        {
            spikeTexture = texture;
            spikeBox = new Rectangle(x, y, width, height);
        }

        public Spike(Texture2D texture, int width, int height)
        {
            spikeTexture = texture;
            spikeBox = new Rectangle();
            spikeBox.Width = width;
            spikeBox.Height = height;
        }
        //set spike center to cursor position when dragging
        public void Drag()
        {
            MouseState mouseState = Mouse.GetState();
            XPos = mouseState.X - spikeBox.Width / 2;
            YPos = mouseState.Y - spikeBox.Height / 2;
        }
    }
}