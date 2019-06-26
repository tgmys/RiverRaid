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
    public class MovingSprite : Sprite
    {

        public float xSpeed;
        public float ySpeed;
        public float ySpeed1;
        float tick;
   
        protected float y2;
        bool hit = false;
       
        int x1, y1;
        public virtual bool CheckCollision(Rectangle target)
        {
            return spriteRectangle.Intersects(target);
        }

        public MovingSprite(
                Texture2D inSpriteTexture,
                float widthFactor,
                float ticksToCrossScreen,
                float inMinDisplayX,
                float inMaxDisplayX,
                float inMinDisplayY,
                float inMaxDisplayY,
                float inInitialX,
                float inInitialY)
        : base(inSpriteTexture,
                 widthFactor,
                 inMinDisplayX,
                 inMaxDisplayX,
                 inMinDisplayY,
                 inMaxDisplayY,
                 inInitialX,
                 inInitialY)
        {
            minDisplayX = inMinDisplayX;
            minDisplayY = inMinDisplayY;
            maxDisplayX = inMaxDisplayX;
            maxDisplayY = inMaxDisplayY;
            initialX = inInitialX;
            initialY = inInitialY;
            tick = ticksToCrossScreen;
            float displayWidth = maxDisplayX - minDisplayX;

            xSpeed = displayWidth / tick;
            ySpeed = xSpeed;
            y2 = xSpeed;
        }

        public override void Update(Game1 game, TouchCollection touches)
        {
            int w=2 ;
            float pom;
            if (!game.death)
            {
                y += ySpeed;
                game.downY = (int)(ySpeed+0.5f);
                spriteRectangle.Y = (int)(y + 0.5f);
            }

           
            if (game.edgeLeft.YPos > maxDisplayY)
            {             
                pom= game.edgeLeft3.YPos - game.edgeLeft.getRectangle().Height + game.edgeLeft.getRectangle().Height / 100;
                game.edgeLeft.YPos = pom;
                game.edgeRight.YPos = pom;
                game.edges[0] = game.edgeLeft;
                game.edges[1] = game.edgeRight;
                w = 0;
            }

            if (game.edgeLeft2.YPos > maxDisplayY)
            {
                pom = game.edgeLeft.YPos - game.edgeLeft2.getRectangle().Height + game.edgeLeft2.getRectangle().Height / 100;
                game.edgeLeft2.YPos = pom;
                game.edgeRight2.YPos = pom;
                game.edges[2] = game.edgeLeft2;
                game.edges[3] = game.edgeRight2;
                w = 1;
            }

            if (game.edgeLeft3.YPos > maxDisplayY)
            {
                pom= game.edgeLeft2.YPos - game.edgeLeft3.getRectangle().Height + game.edgeLeft3.getRectangle().Height / 100;
                game.edgeLeft3.YPos = pom;
                game.edgeRight3.YPos = pom;
                game.edges[4] = game.edgeLeft3;
                game.edges[5] = game.edgeRight3;
                w = 2;
            }

            if(game.fuel.y>maxDisplayY || hit)
            {
                hit = false;
                y1=game.random.Next( -40 - game.fuel.getRectangle().Height, -20 - game.fuel.getRectangle().Height);
                switch (w)
                {
                    case 0:
                        x1 = game.random.Next(game.edgeLeft2.getRectangle().Width, (int)game.edgeRight2.XPos-game.fuel.getRectangle().Width);
                        break;
                    case 1:
                        x1 = game.random.Next(game.edgeLeft3.getRectangle().Width, (int)game.edgeRight3.XPos - game.fuel.getRectangle().Width);
                        break;
                    case 2:
                        x1 = game.random.Next(game.edgeLeft.getRectangle().Width, (int)game.edgeRight.XPos - game.fuel.getRectangle().Width);
                        break;
                }
                game.fuel.XPos = x1;
                game.fuel.YPos = y1;
            }


            
            if(game.bullet.CheckCollision(game.fuel.spriteRectangle) && game.shoot)
            {
                hit = true;
                
            }
            if(!game.death1)
            foreach (TouchLocation touch in touches)
            {
                if (touch.State == TouchLocationState.Moved)
                {
                    if (game.Up.getRectangle().Contains(touch.Position.X, touch.Position.Y))
                    {
                        game.Raid.LoadTexture(game.explosion);
                        y+= ySpeed ;
                        game.downY = (int)(ySpeed*2);
                    }
                    if (game.Down.getRectangle().Contains(touch.Position.X, touch.Position.Y))
                    {

                        y -= (ySpeed/2);
                        game.downY = (int)(ySpeed / 2);
                    }
                }

            }
           

        }
       
      
    }
}