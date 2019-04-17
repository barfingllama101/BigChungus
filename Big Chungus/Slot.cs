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
    class Slot
    {

        #region setup
        Rectangle baseRect;


        // name of object that slot holds
        string slotName = "";
        public string SlotName
        {
            get { return slotName; }
            set { slotName = value; }
        }

        //description of object

        string slotDescription = "";
        public string SlotDescription
        {
            get { return slotDescription; }
            set { slotDescription = value; }
        }
        bool isActivated;
        public bool IsActivated
        {
            get { return isActivated; }
            set { isActivated = value; }
        }
        int itemCount;

        public int ItemCount
        {
            get { return itemCount; }
            set { itemCount = value; }
        }


        Texture2D slotTexture;
        public Texture2D Texture
        {
            get { return slotTexture; }
            set { slotTexture = value; }
        }
        public Rectangle Box
        {
            get { return baseRect; }
            //   set { baseRect = value; }
        }

        private int xPos;
        public int XPos
        {
            get { return baseRect.X; }
            set { baseRect.X = value; }
        }
        private int yPos;
        public int YPos
        {
            get { return baseRect.Y; }
            set { baseRect.Y = value; }
        }
        private Color color;
        public Color cOlor
        {
            get { return color; }
            set { color = value; }
        }
        public Slot(Texture2D texture, int xpos, int ypos, Color c, int newClass, GameObject newObject)
        {
          //  slotName = name;
           
            slotTexture = texture;
            this.xPos = xpos;
            this.yPos = ypos;
            baseRect = new Rectangle(xPos, yPos, 100, 100);
            cOlor = c;
            numItems = newClass;
            itemClass = newObject;
            for (int i = 0; i < numItems; i++)
            {
                items.Add(itemClass);
            }
        }
        #endregion
        #region interactions
        private GameObject itemClass;
        //private String label;
        private int numItems;
        private List<GameObject> items = new List<GameObject>();
        private bool hasObject;
        public bool HasObject
        {
            get { return hasObject; }
            set { hasObject = value; }
        }

        public int NumItems { get => numItems; set => numItems = value; }
        internal GameObject ItemClass { get => itemClass; set => itemClass = value; }
        internal List<GameObject> Items { get => items; set => items = value; }
        #endregion
        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            spriteBatch.Draw(slotTexture, baseRect, color);
            spriteBatch.DrawString(spriteFont, numItems+"", new Vector2(xPos + 50, yPos + 50), Color.Blue);
            spriteBatch.DrawString(spriteFont, slotName, new Vector2(xPos + 20, yPos +30), Color.Blue);
            if (hasObject == true)
            {


                spriteBatch.DrawString(spriteFont, slotDescription, new Vector2(xPos + 2, yPos - 50), Color.Blue);
            }
        }

        public void activating(int counting)
        {
            if (counting > 0)
            {
                isActivated = true;

            }
            else
            {
                isActivated = false;
            }
        }
        //Returns the object created by the slot
        public void getItem(Level level, MouseState mouseState, MouseState prevMouseState, Texture2D spikeballTexture, List<Spike> levelSpikes) {
            
       
            //checks if the mouse button is clicked on the platform, and if the platform's isMovable is true, then sets the heldplatform
            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && numItems > 0 && baseRect.Intersects(new Rectangle(mouseState.Position, new Point(1))))
            {
                numItems -= 1;
                GameObject Object=null;
            //    itemClass.IsVisible = true;
                if (itemClass is Platform)
                {
                    if(itemClass is Spring)
                    {
                        Object = new Spring(itemClass.Texture, itemClass.Box.Width, itemClass.Box.Height);
                    }
                    else
                    {
                        Object = new Platform(itemClass.Texture, itemClass.Box.Width, itemClass.Box.Height);
                    }
                }
                else if(itemClass is Carrot)
                {
                    Object = new Carrot(itemClass.Texture, itemClass.Box.Width, itemClass.Box.Height);
                }
                else if (itemClass is Spike)
                {
                    Object = new Spike(itemClass.Texture, itemClass.Box.Width, itemClass.Box.Height);
                }
                else if (itemClass is SpikeballLauncher)
                {
                    Object = new SpikeballLauncher(itemClass.Texture, itemClass.Box.Width, itemClass.Box.Height, levelSpikes, spikeballTexture);
                }
                Object.XPos = mouseState.X-100;
                Object.YPos = mouseState.Y-100;
                Object.IsMoveable = true;
                if(Object is SpikeballLauncher)
                {
                    SpikeballLauncher newLauncher = (SpikeballLauncher)Object;
                    level.AddObject(newLauncher);
                    level.AddObject(newLauncher.Spikeball);
                }
                else
                {
                    level.AddObject(Object);
                }
                
            }
        }

        public bool SlotIntersecting(Rectangle rect) {
            if (this.baseRect.Intersects(rect))
            {
                hasObject = true;
                return hasObject;

            }
            else
            {
                hasObject = false;
                return hasObject;
            }
        }
    }
}
