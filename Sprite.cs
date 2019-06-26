using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;
namespace RiverRaid
{
    public class Sprite : BaseSprite
    {
        protected float x;
        protected float y;
        protected float initialX;
        protected float initialY;

        protected float minDisplayX;
        protected float maxDisplayX;

        protected float minDisplayY;
        protected float maxDisplayY;
       

        public float XPos
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }

        }

        public float YPos
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }

        }

        public Sprite(Texture2D inSpriteTexture,
                float widthFactor,
                float inMinDisplayX,
                float inMaxDisplayX,
                float inMinDisplayY,
                float inMaxDisplayY,
                float inInitialX,
                float inInitialY)
            : base(inSpriteTexture, Rectangle.Empty)
        {
            minDisplayX = inMinDisplayX;
            minDisplayY = inMinDisplayY;
            maxDisplayX = inMaxDisplayX;
            maxDisplayY = inMaxDisplayY;
            initialX = inInitialX;
            initialY = inInitialY;

            float displayWidth = maxDisplayX - minDisplayX;

            spriteRectangle.Width = (int)((displayWidth * widthFactor) + 0.5f);
            float aspectRatio =
                    (float)spriteTexture.Width / spriteTexture.Height;
            spriteRectangle.Height =
                    (int)((spriteRectangle.Width / aspectRatio) + 0.5f);
            x = initialX;
            y = initialY;
       
        }
     

        public override void StartGame()
        {
            x = initialX;
            y = initialY;
            spriteRectangle.X = (int)x;
            spriteRectangle.Y = (int)y;
            base.StartGame();
        }

    }
}
