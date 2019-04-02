using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Big_Chungus
{
    class Spike : GameObject
    {

        private Texture2D spikeTexture;
        private Rectangle spikeBox;

        public Texture2D SpikeTexture { get => spikeTexture; set => spikeTexture = value; }
        public int XPos { get => spikeBox.X; set => spikeBox.X = value; }
        public int YPos { get => spikeBox.Y; set => spikeBox.Y = value; }
        public Rectangle Box { get => spikeBox; set => spikeBox = value; }

        public Spike(Texture2D texture, int x, int y, int width, int height)
        {
            spikeTexture = texture;
            spikeBox = new Rectangle(x, y, width, height);
        }

    }
}