using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Big_Chungus
{
    //This class houses the data for a single level so it can be saved and edited
    class Level
    {
        //Fields
        private List<Platform> platforms = new List<Platform>(100);
        private List<Carrot> carrots = new List<Carrot>(100);
        private List<Spike> spikes = new List<Spike>(100);
        private List<Spring> springs = new List<Spring>(100);
        private List<SpikeballLauncher> launchers = new List<SpikeballLauncher>(100);
        private List<List<int>> platformPositions = new List<List<int>>();
        private List<List<int>> carrotPositions = new List<List<int>>();
        private List<List<int>> spikePositions = new List<List<int>>();
        private List<List<int>> springPositions = new List<List<int>>();
        private List<List<int>> launcherPositions = new List<List<int>>();
        private List<int> inventoryItems = new List<int>();
        private int playerSpawnX;
        private int playerSpawnY;

        public int PlayerSpawnX { get => playerSpawnX; set => playerSpawnX = value; }
        public int PlayerSpawnY { get => playerSpawnY; set => playerSpawnY = value; }
        internal List<Platform> Platforms { get => platforms; set => platforms = value; }
        internal List<Carrot> Carrots { get => carrots; set => carrots = value; }
        internal List<Spike> Spikes { get => spikes; set => spikes = value; }
        internal List<Spring> Springs { get => springs; set => springs = value; }
        internal List<SpikeballLauncher> Launchers { get => launchers; set => launchers = value; }
        public List<List<int>> PlatformPositions { get => platformPositions; set => platformPositions = value; }
        public List<List<int>> CarrotPositions { get => carrotPositions; set => carrotPositions = value; }
        public List<List<int>> SpikePositions { get => spikePositions; set => spikePositions = value; }
        public List<List<int>> SpringPositions { get => springPositions; set => springPositions = value; }
        public List<List<int>> LauncherPositions { get => launcherPositions; set => launcherPositions = value; }
        public List<int> InventoryItems { get => inventoryItems; set => inventoryItems = value; }

        public Level()
        {

        }

        public Level(int playerX, int playerY, List<Platform> newPlatforms, List<Carrot> newCarrots, List<Spike> newSpikes, List<Spring> newSprings,List<SpikeballLauncher> newLaunchers,List<int> newInv)
        {
            playerSpawnX = playerX;
            playerSpawnY = playerY;
            platformPositions.Add(new List<int>(100));
            platformPositions.Add(new List<int>(100));
            carrotPositions.Add(new List<int>(100));
            carrotPositions.Add(new List<int>(100));
            spikePositions.Add(new List<int>(100));
            spikePositions.Add(new List<int>(100));
            springPositions.Add(new List<int>(100));
            springPositions.Add(new List<int>(100));
            launcherPositions.Add(new List<int>(100));
            launcherPositions.Add(new List<int>(100));

            for (int i = 0; i < newPlatforms.Count; i++)
            {
                AddObject(newPlatforms[i]);
            }
            for (int i = 0; i < newCarrots.Count; i++)
            {
                AddObject(newCarrots[i]);
            }
            for (int i = 0; i < newSpikes.Count; i++)
            {
                AddObject(newSpikes[i]);
            }
            for (int i = 0; i < newSprings.Count; i++)
            {
                AddObject(newSprings[i]);
            }
            for (int i = 0; i < newLaunchers.Count; i++)
            {
                AddObject(newLaunchers[i]);
            }
            for (int i = 0; i < 6; i++)
            {
                InventoryItems.Add(newInv[i]);
            }
        }

        public void AddObject(GameObject newObject)
        {
            if (newObject is Platform)
            {
                platforms.Add((Platform)newObject);
                platformPositions[0].Add(newObject.XPos);
                platformPositions[1].Add(newObject.YPos);
            }
            else if (newObject is Carrot)
            {
                carrots.Add((Carrot)newObject);
                carrotPositions[0].Add(newObject.XPos);
                carrotPositions[1].Add(newObject.YPos);
            }
            else if (newObject is Spike)
            {
                spikes.Add((Spike)newObject);
                spikePositions[0].Add(newObject.XPos);
                spikePositions[1].Add(newObject.YPos);
            }
            else if (newObject is Spring)
            {
                springs.Add((Spring)newObject);
                springPositions[0].Add(newObject.XPos);
                springPositions[1].Add(newObject.YPos);
            }
            else if (newObject is SpikeballLauncher)
            {
                launchers.Add((SpikeballLauncher)newObject);
                launcherPositions[0].Add(newObject.XPos);
                launcherPositions[1].Add(newObject.YPos);
            }
        }

        /*public void Reset(List<Platform> newPlatforms, List<Carrot> newCarrots, List<Spike> newSpikes, List<Spring> newSprings, List<SpikeballLauncher> newLaunchers, List<int> newInv)
        {
            for (int i = 0; i < newPlatforms.Count; i++)
            {
                newPlatforms[i].XPos = platformPositions[i][0];
                newPlatforms[i].YPos = platformPositions[i][1];
            }
            for (int i = 0; i < newCarrots.Count; i++)
            {
                newCarrots[i].XPos = carrotPositions[i][0];
                newCarrots[i].YPos = carrotPositions[i][1];
            }
            for (int i = 0; i < newSpikes.Count; i++)
            {
                newSpikes[i].XPos = spikePositions[i][0];
                newSpikes[i].YPos = spikePositions[i][1];
            }
            for (int i = 0; i < newSprings.Count; i++)
            {
                newSprings[i].XPos = springPositions[i][0];
                newSprings[i].YPos = springPositions[i][1];
            }
            for (int i = 0; i < newLaunchers.Count; i++)
            {
                newLaunchers[i].XPos = launcherPositions[i][0];
                newLaunchers[i].YPos = launcherPositions[i][1];
            }
            for (int i = 0; i < 6; i++)
            {
                inventoryItems[i] = newInv[i];
            }
        }*/
    }
}
