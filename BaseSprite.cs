
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using System.Collections.Generic;
namespace RiverRaid
{ 
    public class BaseSprite
    {
        protected Texture2D spriteTexture;
        protected Rectangle spriteRectangle;


        public void LoadTexture(Texture2D inSpriteTexture)
        {
            spriteTexture = inSpriteTexture;
        }

        public void SetRectangle(Rectangle inSpriteRectangle)
        {
            spriteRectangle = inSpriteRectangle;
        }

        public Rectangle getRectangle()
        {
            return spriteRectangle;
        }
        public virtual void Update(Game1 game, TouchCollection touches)
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spriteRectangle, Color.White);
        }
        public virtual void StartGame()
        {
        }
 
      
        public BaseSprite(Texture2D inSpriteTexture, Rectangle inRectangle)
        {
            LoadTexture(inSpriteTexture);
            SetRectangle(inRectangle);
        }

    }
}