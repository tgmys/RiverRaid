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
    public class Bullet: MovingSprite
    {
        public bool inter = false;
        public Bullet(Texture2D inSpriteTexture,
            float widthFactor, float ticksToCrossScreen,
            float inMinDisplayX, float inMaxDisplayX,
            float inMinDisplayY, float inMaxDisplayY,
            float inInitialX, float inInitialY)
    : base(inSpriteTexture, widthFactor, ticksToCrossScreen,
    inMinDisplayX, inMaxDisplayX,
    inMinDisplayY, inMaxDisplayY,
    inInitialX, inInitialY)
        {

        }

        public Boolean BulletCol(Game1 game,Rectangle rec,int upt)
        {
            if (game.bullet.CheckCollision(rec) && game.shoot)
            {
                game.soundEffects[2].Play();
                game.inter1 = true;
                if (game.bullet.YPos < minDisplayY || game.inter1)
                {
                    game.shoot = false;
                    if (game.inter1)
                        game.UpdateScore(upt);

                    game.inter1 = false;
                    game.spee = 0;
                    game.f = false;
                    game.bullet.YPos = game.Raid.YPos + game.Raid.getRectangle().Height / 2;

                }
                return true;
            }
            else
                return false;
        }
        public override void Update(Game1 game, TouchCollection touches)
        {
            y -= game.spee;
            foreach (TouchLocation touch in touches)
            {
                if (touch.State == TouchLocationState.Pressed)
                {

                    if (game.Shoot.getRectangle().Contains(touch.Position.X, touch.Position.Y))
                    {
                        game.soundEffects[3].Play();
                        game.spee = ySpeed;
                        game.f = true;
                        game.shoot = true;
                    }
                   
                        
                }
            }
            if(game.fuel.CheckCollision(spriteRectangle)&& game.shoot)
            {
                inter = true;
            }
            if(game.bullet.YPos<minDisplayY || inter)
            {
                game.shoot = false;
                if(inter)
                    game.UpdateScore(80);
                inter = false;
                game.spee = 0;
                game.f = false;
                game.bullet.YPos= game.Raid.YPos+ game.Raid.getRectangle().Height/2;
                
            }
            x = game.Raid.XPos + game.Raid.getRectangle().Width / 2;
         
            spriteRectangle.Y = (int)(y + 0.5f);
            if(!game.f)
            spriteRectangle.X = (int)(x + 0.5f);
        }
    }
}