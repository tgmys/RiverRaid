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
    public class Pointer : MovingSprite
    {
        public Pointer(Texture2D inSpriteTexture,
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

        public override void Update(Game1 game, TouchCollection touches)
        {

            if (game.Raid.CheckCollision(game.fuel.getRectangle()))
            {
                x += xSpeed * 3;
                game.soundEffects[1].Play();
            }
            else
                x -= xSpeed;

            if (game.Gauge.XPos >= game.Pointer.XPos)
            {

                game.UpdateLives(-1);
            }

            spriteRectangle.X = (int)(x + 0.5f);
        }
    }
}