using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Big_Chungus
{
    class MainMenu
    {
        // I can't load fonts on this computer which is obviously a problem. 


            // this is all goign to be changed 
            // so plz ignore it 

            // Ther will be a UIELEMENT class instead and those will be put into a list 
            // 

        //background
        private Texture2D UITexture;
        //button1
        private Texture2D Button1;
        private Rectangle button1Rect;
        //button2
        private Texture2D Button2;
        private Rectangle button2Rect;

        private Rectangle UIRect;

        private Rectangle mouseRect;
        public void LoadContent(ContentManager content)
        {
            // bg texture;
            UITexture = content.Load<Texture2D>("");
            Button1 = content.Load<Texture2D>("");
            button1Rect = new Rectangle(0, 0, UITexture.Width, UITexture.Height);
            
            Button2 = content.Load<Texture2D>("");
            button2Rect = new Rectangle(50, 50, UITexture.Width, UITexture.Height);
            UIRect = new Rectangle(0, 0, UITexture.Width, UITexture.Height);

            mouseRect = new Rectangle(0, 0,30, 30);
        }

        void Update() {
            mouseRect.X = Mouse.GetState().X;
            mouseRect.Y = Mouse.GetState().Y;

           if(Intersect(button1Rect, mouseRect))
            {

            }
           if(Intersect(button2Rect, mouseRect))
            {

            }
           
        }

        bool Intersect(Rectangle hello, Rectangle mouse)
        {
          
            if (mouse.Intersects(hello))
            {
                return true;
            }
            return true ;
        }

    }
}
