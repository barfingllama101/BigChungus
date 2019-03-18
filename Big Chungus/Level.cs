using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Big_Chungus
{
    class Level
    {
        //Fields
        private Platform[] levelPlatforms;
        private Carrot[] levelCarrots;
        private int[,] platformPositions = new int[0, 2];
        private int[,] carrotPositions = new int[0, 2];
        private int playerSpawnX;
        private int playerSpawnY;

        public int[,] CarrotPositions { get => carrotPositions; set => carrotPositions = value; }
        public int[,] PlatformPositions { get => platformPositions; set => platformPositions = value; }
        internal Carrot[] LevelCarrots { get => levelCarrots; set => levelCarrots = value; }
        internal Platform[] LevelPlatforms { get => levelPlatforms; set => levelPlatforms = value; }
        public int PlayerSpawnX { get => playerSpawnX; set => playerSpawnX = value; }
        public int PlayerSpawnY { get => playerSpawnY; set => playerSpawnY = value; }

        public Level()
        {

        }

        public void AddObject(GameObject newObject)
        {
            if (newObject is Platform)
            {
                levelPlatforms[levelPlatforms.Length] = (Platform)newObject;
                platformPositions[platformPositions.Length, 0] = newObject.XPos;
                platformPositions[platformPositions.Length, 1] = newObject.YPos;
            }
            else if (newObject is Carrot)
            {
                levelCarrots[levelCarrots.Length] = (Carrot)newObject;
                carrotPositions[carrotPositions.Length, 0] = newObject.XPos;
                carrotPositions[carrotPositions.Length, 1] = newObject.YPos;
            }
        }
    }
}
