using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace RiverRaid
{
    
    public class RaiderCont:MovingSprite
    {
        protected Texture2D spriteleft;
        protected Texture2D spriteright;
        protected Texture2D sprite;
        //float countDuration1s = 3f;
        // float currentTime = 0f;
        

     
        public RaiderCont(Texture2D inSpriteTexture,
        Texture2D inspriteleft,
        Texture2D inspriteright,
            float widthFactor, float ticksToCrossScreen,
            float inMinDisplayX, float inMaxDisplayX,
            float inMinDisplayY, float inMaxDisplayY,
            float inInitialX, float inInitialY)
    : base(inSpriteTexture, widthFactor, ticksToCrossScreen,
    inMinDisplayX, inMaxDisplayX,
    inMinDisplayY, inMaxDisplayY,
    inInitialX, inInitialY)
        {
            spriteleft = inspriteleft;
            spriteright = inspriteright;
            sprite = inSpriteTexture;
        }

        public void Deathraider(Game1 game, Rectangle rec,float curentTime)
        {
            if (game.Raid.CheckCollision(rec))
            {
                game.death1 = true;
                game.bullet.YPos = 5000;
                game.Raid.LoadTexture(game.explosion);
                game.downY = 0;
                game.start = true;
                game.death = true;
                if ((game.currentTime >= curentTime)|| curentTime==0)
                {
                    game.counter++;
                    game.currentTime -= game.countDuration1s;
                    if (game.counter >= 1)
                    {
                        game.death1 = false;
                        game.start = false;
                        game.death = false;
                        game.counter = 0;
                        game.currentTime -= game.countDuration1s;

                        game.UpdateLives(-1);
                    }
                }
            }
        }
        public override void Update(Game1 game, TouchCollection touches)
        {

            if (!game.death1)
            {
                game.Raid.LoadTexture(sprite);

                foreach (TouchLocation touch in touches)
                {
                    if (touch.State == TouchLocationState.Moved)
                    {
                        if (game.Right.getRectangle().Contains(touch.Position.X, touch.Position.Y))
                        {
                            game.Raid.LoadTexture(spriteright);
                            x = x + xSpeed;

                        }
                        if (game.Left.getRectangle().Contains(touch.Position.X, touch.Position.Y))
                        {
                            game.Raid.LoadTexture(spriteleft);
                            x -= xSpeed;

                        }
                    }

                }
            }
            for (int i = 0; i < game.edges.Length; i++)
            {
                if (game.edges[i].CheckCollision(spriteRectangle))
                {
                    game.bullet.YPos = 5000;
                    game.death1 = true;
                    game.Raid.LoadTexture(game.explosion);
                    game.downY = 0;
                    game.start = true;
                    game.death = true;
                    if ((game.currentTime >= game.countDuration1s))
                    {
                        game.counter++;
                        game.currentTime -= game.countDuration1s;
                        if (game.counter>=1)
                        {
                            game.start = false;
                            game.death = false;
                            game.counter = 0;
                            game.currentTime -= game.countDuration1s;
                            game.death1 = false;
                            game.UpdateLives(-1);
                        }
                    }
                }
            }
            if (!game.death)
            {
                spriteRectangle.X = (int)(x + 0.5f);
                spriteRectangle.Y = (int)(y + 0.5f);
            }
            

        }
 

   
    }
}