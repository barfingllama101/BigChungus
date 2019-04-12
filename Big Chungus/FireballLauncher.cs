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
    class SpikeballLauncher : GameObject
    {
        private Texture2D texture;
        private Rectangle box;
        private Spike spikeball;

        //0=left, 1=right, 2=up, 3=down
        private int direction;

        public Rectangle Box { get => box; set => box = value; }
        public int XPos { get => box.X; set => box.X = value; }
        public int YPos { get => box.Y; set => box.Y = value; }
        public int Direction { get => direction; set => direction = value; }
        internal Spike Spikeball { get => spikeball; set => spikeball = value; }
        public Texture2D Texture { get => texture; set => texture = value; }

        public SpikeballLauncher(Texture2D newTexture, int newX, int newY, int width, int height, int facing, List<Spike> levelSpikes, Texture2D spikeballTexture)
        {
            texture = newTexture;
            XPos = newX;
            YPos = newY;
            box = new Rectangle(XPos, YPos, width, height);
            direction = facing;
            spikeball = new Spike(spikeballTexture, XPos, YPos, spikeballTexture.Width/2, spikeballTexture.Height/2);
            levelSpikes.Add(spikeball);
        }
        /*public SpikeballLauncher(Texture2D newTexture, int width, int height, Texture2D spikeballTexture)
        {
            texture = newTexture;
            box = new Rectangle();
            box.Width = width;
            box.Height = height;
            direction = 0;
            spikeball = new Spike(spikeballTexture, spikeballTexture.Width / 2, spikeballTexture.Height / 2);
        }*/
        public void Launch(int speed)
        {
            switch (direction)
            {
                case 0:
                    spikeball.XPos -= speed;
                    break;
                case 1:
                    spikeball.XPos += speed;
                    break;
                case 2:
                    spikeball.YPos -= speed;
                    break;
                case 3:
                    spikeball.YPos += speed;
                    break;
                default:
                    break;
            }
        }
    }
}
