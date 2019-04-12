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

        string slotName;
        public string SlotName
        {
            get { return slotName; }
            set { SlotName = value; }
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
        public Texture2D SlotTexture
        {
            get { return slotTexture; }
            set { slotTexture = value; }
        }
        public Rectangle bRect
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
        public Slot(Texture2D texture, int xpos, int ypos, Color c, int items, string name)
        {
            slotName = name;
            slotTexture = texture;
            this.xPos = xpos;
            this.yPos = ypos;
            baseRect = new Rectangle(xPos, yPos, 100, 100);
            cOlor = c;
            numItems = items;
        }


        #endregion
        #region interactions
        private int numItems;
        private bool hasObject;
        public bool HasObject
        {
            get { return hasObject; }
            set { hasObject = value; }
        }

        public int NumItems { get => numItems; set => numItems = value; }
        #endregion
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(slotTexture, baseRect, color);
        }

        public void activating(int counting){
            if (counting > 0)
            {
                isActivated = true;

            }
            else
            {
                isActivated = false;
            }
}
        public void getItem() {
            MouseState mouseState = new MouseState();
            MouseState prevMouseState = mouseState;
            mouseState = Mouse.GetState();
       
                    //checks if the mouse button is clicked on the platform, and if the platform's isMovable is true, then sets the heldplatform
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && hasObject == true)
                    {
                        
                    }
                
            
        }

        public void SlotIntersecting(Rectangle rect) {
            if (this.baseRect.Intersects(rect))
            {
                hasObject = true;
            }
            else
            {
                hasObject = false;
            }
        }
    }
}
