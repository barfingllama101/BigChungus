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

        //texture that will hold the image of what type of object is in the inventory slot 
        Texture2D slotTypeTexture;

        public Texture2D SlotTypeTexture
        {
            get { return slotTypeTexture; }
            set { slotTypeTexture = value; }
        }

        // rectangle that will hold the slottypetexture 
        Rectangle slottyperect;

        public Rectangle SlotTRect
        {
            get { return slottyperect; }
            set { slottyperect = value; }
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
        private Rectangle textRect;

        public Rectangle TextRect
        {
            get { return textRect; }
            set { textRect = value; }
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
      
            // this is the rectangle for the image above the slot that tells what type of item it is
            slottyperect = new Rectangle(xPos - 30, yPos - 125, 200, 200);
            // this rectangle is for the item description
            textRect = new Rectangle(230, 520, 450, 25);
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
        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, Texture2D textTexture)
        {
            // drawing slot 
            spriteBatch.Draw(slotTexture, baseRect, color);
         
            //drawing remaining tools of that type left 
            spriteBatch.DrawString(spriteFont, numItems+" remaining", new Vector2(xPos, yPos + 50), Color.Blue);
            //  spriteBatch.DrawString(spriteFont, slotName, new Vector2(xPos + 20, yPos +30), Color.Blue);

            //drawing type 
            spriteBatch.Draw(slotTypeTexture, slottyperect, Color.White);
            if (hasObject == true)
            {
                //drawing item description if condition met 
                spriteBatch.Draw(textTexture, textRect, Color.LawnGreen);
                spriteBatch.DrawString(spriteFont, slotDescription, new Vector2(250, 520), Color.Blue);
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
