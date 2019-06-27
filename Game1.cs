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
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game//,ISpriteBasedGame
    {

        Texture2D jetLeftTexture;
        Rectangle jetLeftPosition;
        int jetLeftX, jetLeftY, jetLeftMove = 5;
        Texture2D jetRightTexture;
        Rectangle jetRightPosition;
        int jetRightX, jetRightY, jetRightMove = 5;

        public bool death1 = false;
        Texture2D explosionTexture;
        Rectangle explosionPosition;
        public bool inter1 = false;
        public bool inter = false;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Random random = new Random();
        public RaiderCont Raid;
        public Sprite Right;
        public Sprite Left;
        public Sprite Up;
        public Sprite Down;
        public Sprite Shoot;
        public Sprite Start;
        public Sprite Gauge;
        public Pointer Pointer;
        public BaseSprite panel;
        public MovingSprite edgeLeft;
        public MovingSprite edgeRight;
        public MovingSprite edgeLeft2;
        public MovingSprite edgeRight2;
        public MovingSprite edgeLeft3;
        public MovingSprite edgeRight3;
        public MovingSprite bridge;

        public MovingSprite shipleft;
        public MovingSprite heli;
        public MovingSprite fuel;
        public Bullet bullet;
        public BaseSprite endgame;
        SpriteFont tekst;
        public int downY;
        int displayWidth;
        int displayHeight;
        float minDisplayX;
        float maxDisplayX;
        float minDisplayY;
        float maxDisplayY;
        Texture2D pom;
        public Texture2D explosion;
        int score = 0;
        int lives = 3;
        int speed = 350;//0.2000
        public List<BaseSprite> GameSprites = new List<BaseSprite>();
        public List<BaseSprite> EndSprites = new List<BaseSprite>();
        bool[] zestrzelony = { false, false };
        public MovingSprite[] edges = new MovingSprite[7];
        public List<SoundEffect> soundEffects;
        public int counter = 1;
        public int counter2 = 1;
        public int counter3 = 1;

        public float countDuration1s = 1.0f; //1s
        public float countDuration3s = 3f; //3s
        public float countDuration13s = 1f;
        public float currentTime = 0.0f;
        public float currentTime2 = 0f;
        public float currentTime3 = 0f;
        public bool death = false;
        public bool start = false;
        public bool f = false;
        public float spee = 0;

        public bool shoot = false;

        Texture2D ship1Texture;
        Texture2D ship1RTexture;
        Rectangle ship1Position;
        Rectangle ship1RPosition;
        int ship1X, ship1Y, ship1Move = 2;
        Boolean ship1LtR = true;
        Texture2D ship2Texture;
        Texture2D ship2RTexture;
        Rectangle ship2Position;
        Rectangle ship2RPosition;
        int ship2X, ship2Y;
        Boolean ship2LtR = false;
        Texture2D heliLTexture;
        Rectangle heliLPosition;
        Texture2D heliL2Texture;
        Rectangle heliL2Position;
        int heliLX, heliLY, heliMove = 2;
        Boolean heliMoveEffect = true;
        int width;
        int height;
        public enum GameState
        {
            GamePaused,
            PlayingGame,
            GameOver
        }
        public GameState state = GameState.GamePaused;
        private void setScreenSizes()
        {
            displayWidth = graphics.GraphicsDevice.Viewport.Width;
            displayHeight = graphics.GraphicsDevice.Viewport.Height;

            minDisplayX = 0;
            minDisplayY = 0;

            maxDisplayX = displayWidth;
            maxDisplayY = displayHeight;
        }
        public void DrawText()
        {
            spriteBatch.DrawString(tekst, "Score: " + score, new Vector2(Gauge.XPos + Gauge.getRectangle().Width / 3, Gauge.YPos - Gauge.getRectangle().Height / 2), Color.Yellow);
            spriteBatch.DrawString(tekst, "Life: " + lives, new Vector2(Gauge.XPos + Gauge.getRectangle().Width / 3, Gauge.YPos + Gauge.getRectangle().Height), Color.Yellow);
        }
        public void StartGame()
        {
            jetLeftX = random.Next(-500, -200);
            jetLeftY = random.Next(0, height / 3);
            jetLeftPosition = new Rectangle(jetLeftX, jetLeftY, (80 * width) / 1080, (45 * height) / 1920);
            jetRightPosition = new Rectangle(jetRightX, jetRightY, (80 * width) / 1080, (45 * height) / 1920);
            ship1X = edgeLeft2.getRectangle().Width;//- ship1Texture.Width;
            ship1Y = random.Next(-500 - ship1Texture.Height, -100 - ship1Texture.Height);
            ship1Position = new Rectangle(ship1X, ship1Y, (150 * width) / 1080, (75 * height) / 1920);
            ship1RPosition = new Rectangle(ship1X, ship1Y, (150 * width) / 1080, (75 * height) / 1920);
            ship2X = (int)maxDisplayX - edgeRight2.getRectangle().Width - ship2Texture.Width;
            ship2Y = random.Next(-500 - ship2Texture.Height, -100 - ship2Texture.Height);
            ship2Position = new Rectangle(ship2X, ship2Y, (150 * width) / 1080, (75 * height) / 1920);
            ship2RPosition = new Rectangle(ship2X, ship2Y, (150 * width) / 1080, (75 * height) / 1920);
            heliLX = random.Next((int)edgeLeft2.XPos + edgeLeft2.getRectangle().Width * width / 1080, width / 3);
            heliLY = random.Next(-500, -100);
            heliLPosition = new Rectangle(heliLX, heliLY, (113 * width) / 1080, (64 * height) / 1920);
            heliL2Position = new Rectangle(heliLX, heliLY, (113 * width) / 1080, (64 * height) / 1920);
            foreach (BaseSprite sprite in GameSprites)
            {
                sprite.StartGame();
            }


        }


        public void Stopped(TouchCollection touches)
        {
           
            foreach (TouchLocation touch in touches)
            {
                if (touch.State == TouchLocationState.Pressed)
                {

                    if (Start.getRectangle().Contains(touch.Position.X, touch.Position.Y))
                    {
                        if (state == GameState.GamePaused)
                        {
                            SoundEffect.MasterVolume = 1.0f;
                            state = GameState.PlayingGame;
                        }
                        else if (state == GameState.GameOver)
                        {
                            SoundEffect.MasterVolume = 0.0f;
                            score = 0;
                            lives = 3;
                            state = GameState.GamePaused;
                            StartGame();
                        }
                        else
                        {
                            SoundEffect.MasterVolume = 0.0f;
                            state = GameState.GamePaused;
                            
                        }
                    }
                }
            }
        }
        public void UpdateScore(int update)
        {
            score += update;
        }
        public void UpdateLives(int life)
        {

            lives += life;
            if (lives <= 0)
                EndGame();
            else
            {
                StartGame();
            }
        }

        public void EndGame()
        {
            state = GameState.GameOver;
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            soundEffects = new List<SoundEffect>();

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }


        protected override void Initialize()
        {
            setScreenSizes();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            jetLeftTexture = Content.Load<Texture2D>("images/jetLeft");
            jetRightTexture = Content.Load<Texture2D>("images/jetRight");
            explosionTexture = Content.Load<Texture2D>("images/explosion");
            width = graphics.GraphicsDevice.Viewport.Width;
            height = graphics.GraphicsDevice.Viewport.Height;
            Texture2D pom2 = Content.Load<Texture2D>("images/edgev2");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tekst = Content.Load<SpriteFont>("spritefonts/tekst");
            explosion = Content.Load<Texture2D>("images/explosion");

           
            fuel = new MovingSprite(Content.Load<Texture2D>("images/fuel"),
                   0.060f,
                   speed,
                  minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                    maxDisplayX / 2,
                      -50);
            GameSprites.Add(fuel);
            edgeLeft = new MovingSprite(Content.Load<Texture2D>("images/edgev2"),
                 0.200f,
                 speed,
                minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   minDisplayX,
                   minDisplayY);

            edges[0] = edgeLeft;
            GameSprites.Add(edgeLeft);

            edgeRight = new MovingSprite(Content.Load<Texture2D>("images/edgev2"),
                 0.200f,
                 speed,
                minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   maxDisplayX - maxDisplayX * 0.200f,
                   minDisplayY);
            edges[1] = edgeRight;
            GameSprites.Add(edgeRight);

            edgeLeft2 = new MovingSprite(Content.Load<Texture2D>("images/edgev2"),
                 0.300f,
                 speed,
                minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   minDisplayX,
                    edgeLeft.YPos - pom2.Height + pom2.Height * 0.300f);
            edges[2] = edgeLeft2;
            GameSprites.Add(edgeLeft2);

            edgeRight2 = new MovingSprite(Content.Load<Texture2D>("images/edgev2"),
                 0.300f,
                 speed,
                minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   maxDisplayX - maxDisplayX * 0.300f,
                   edgeRight.YPos - pom2.Height + pom2.Height * 0.300f);
            edges[3] = edgeRight2;
            GameSprites.Add(edgeRight2);

            edgeLeft3 = new MovingSprite(Content.Load<Texture2D>("images/edgev2"),
                 0.250f,
                 speed,
                minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   minDisplayX,
                    edgeLeft2.YPos - pom2.Height + pom2.Height * 0.250f);
            edges[4] = edgeLeft3;
            GameSprites.Add(edgeLeft3);

            bridge = new MovingSprite(Content.Load<Texture2D>("images/bridge"),
                 0.600f,
                 speed,
                minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   minDisplayX + edgeLeft3.getRectangle().Width,
                   edgeLeft3.YPos);
            edges[6] = bridge;
            GameSprites.Add(bridge);

            edgeRight3 = new MovingSprite(Content.Load<Texture2D>("images/edgev2"),
                 0.250f,
                 speed,
                minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   maxDisplayX - maxDisplayX * 0.250f,
                   edgeRight2.YPos - pom2.Height + pom2.Height * 0.250f);
            edges[5] = edgeRight3;
            GameSprites.Add(edgeRight3);



            pom = Content.Load<Texture2D>("images/panel");



            shipleft = new MovingSprite(Content.Load<Texture2D>("images/jetRight"),
                 0.085f,
                 200,
                minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   random.Next((int)maxDisplayX, (int)maxDisplayX + 200),
                   random.Next(-50, (int)maxDisplayY / 10));


            panel = new BaseSprite(Content.Load<Texture2D>("images/panel"),
                new Rectangle((int)minDisplayX, (int)maxDisplayY - pom.Height * (int)maxDisplayY / 1920, (int)maxDisplayX, (int)maxDisplayY)
                 );
            EndSprites.Add(panel);
            GameSprites.Add(panel);
            endgame = new BaseSprite(Content.Load<Texture2D>("images/gameover1"),
               new Rectangle((int)minDisplayX, (int)minDisplayY, (int)maxDisplayX, (int)maxDisplayY -((int)maxDisplayY- panel.getRectangle().Y))
                );
            EndSprites.Add(endgame);


            Left = new Sprite(Content.Load<Texture2D>("images/left"),
               0.166f,
               minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   minDisplayX,
                 ((int)maxDisplayY - pom.Height * (int)maxDisplayY / 1920) + pom.Height / 4);

            GameSprites.Add(Left);
            EndSprites.Add(Left);
            Up = new Sprite(Content.Load<Texture2D>("images/up"),
               0.166f,
               minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   Left.getRectangle().Width,
                   (int)maxDisplayY - pom.Height * (int)maxDisplayY / 1920);
            EndSprites.Add(Up);
            GameSprites.Add(Up);

            Down = new Sprite(Content.Load<Texture2D>("images/down"),
               0.166f,
               minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   Left.getRectangle().Width,
                   displayHeight - displayHeight / 10);
            EndSprites.Add(Down);
            GameSprites.Add(Down);

            Right = new Sprite(Content.Load<Texture2D>("images/right"),
                 0.166f,
                minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   Up.XPos + Up.getRectangle().Width,
                    Left.YPos);
            EndSprites.Add(Right);
            GameSprites.Add(Right);

            bullet = new Bullet(Content.Load<Texture2D>("images/bullet"),
                0.005f,
                50,
                minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                    maxDisplayX / 2 - (Up.getRectangle().Height) / 4,// na poczıtku znajduje siê na rodku ekranu (w poziomie)
                    panel.getRectangle().Y - (Up.getRectangle().Height));

            GameSprites.Add(bullet);

            Raid = new RaiderCont(Content.Load<Texture2D>("images/raider"),
                Content.Load<Texture2D>("images/raider_mvL"),
                Content.Load<Texture2D>("images/raider_mv"),
                0.085f,
                200,
                minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                    maxDisplayX / 2 - (Up.getRectangle().Height) / 2,// na poczıtku znajduje siê na rodku ekranu (w poziomie)
                    panel.getRectangle().Y - (Up.getRectangle().Height));

            GameSprites.Add(Raid);

            //fe
            Shoot = new Sprite(Content.Load<Texture2D>("images/shoot"),
                 0.166f,
                minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   maxDisplayX - Left.getRectangle().Width,
                   Left.YPos);

            GameSprites.Add(Shoot);

            Start = new Sprite(Content.Load<Texture2D>("images/start"),
                 0.166f,
                minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   Right.XPos + ((Shoot.XPos - Right.XPos) / 2),
                   Right.YPos + (Right.getRectangle().Height / 2 * (float)1.5));
            EndSprites.Add(Start);
            GameSprites.Add(Start);

            Gauge = new Sprite(Content.Load<Texture2D>("images/gauge"),
                 0.266f,
                minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   Right.XPos + Right.getRectangle().Width + Right.getRectangle().Width / 4,
                   Up.YPos + Up.getRectangle().Height / 2);
            EndSprites.Add(Gauge);
            GameSprites.Add(Gauge);

            Pointer = new Pointer(Content.Load<Texture2D>("images/pointer"),
                 0.009f,
                 5000,
                minDisplayX, maxDisplayX, minDisplayY, maxDisplayY,
                   Gauge.XPos + Gauge.getRectangle().Width,
                   Gauge.YPos);

            GameSprites.Add(Pointer);

            ship1Texture = Content.Load<Texture2D>("images/shipLeft");
            ship1RTexture = Content.Load<Texture2D>("images/shipRight");
            ship2Texture = Content.Load<Texture2D>("images/shipLeft");
            ship2RTexture = Content.Load<Texture2D>("images/shipRight");
            heliLTexture = Content.Load<Texture2D>("images/heliL");
            heliL2Texture = Content.Load<Texture2D>("images/heliL2");

            soundEffects.Add(Content.Load<SoundEffect>("sound/engineSound"));
            soundEffects.Add(Content.Load<SoundEffect>("sound/fuelSound"));
            soundEffects.Add(Content.Load<SoundEffect>("sound/hitSound"));
            soundEffects.Add(Content.Load<SoundEffect>("sound/shootSound"));

            soundEffects[0].Play();
            var instance = soundEffects[0].CreateInstance();
            instance.IsLooped = true;
            instance.Play();
            
            SoundEffect.MasterVolume = 0.0f;


            StartGame();
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            TouchCollection touches = TouchPanel.GetState();
            if (start)
                currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (state)
            {
                case GameState.GamePaused:
                    Stopped(touches);

                    break;

                case GameState.PlayingGame:
                    Stopped(touches);

                    foreach (BaseSprite sprite in GameSprites)
                    {
                        sprite.Update(this, touches);
                    }

                    if (jetLeftPosition.X > width + jetLeftTexture.Width)
                    {
                        jetLeftX = random.Next(-500, -200);
                        jetLeftY = random.Next(0, height / 3);
                        jetLeftPosition = new Rectangle(jetLeftX, jetLeftY, (80 * width) / 1080, (45 * height) / 1920);
                    }

                    if (jetRightPosition.X < 0 - jetRightTexture.Width)
                    {
                        jetRightX = random.Next(width, width + 500);
                        jetRightY = random.Next(0, height / 3);
                        jetRightPosition = new Rectangle(jetRightX, jetRightY, (80 * width) / 1080, (45 * height) / 1920);
                    }

                    if (ship1Position.Y > panel.getRectangle().Y)
                    {
                        ship1X = width / 2 - ship1Texture.Width;
                        ship1Y = random.Next(-40 - ship1Texture.Height, -20 - ship1Texture.Height);
                        ship1Position = new Rectangle(ship1X, ship1Y, (150 * width) / 1080, (75 * height) / 1920);
                        ship1RPosition = new Rectangle(ship1X, ship1Y, (150 * width) / 1080, (75 * height) / 1920);
                    }
                    if (ship1Position.Intersects(edgeLeft2.getRectangle()) || ship1Position.Intersects(edgeRight2.getRectangle()) || ship1Position.Intersects(edgeRight.getRectangle()) || ship1Position.Intersects(edgeLeft.getRectangle()) || ship1Position.Intersects(edgeLeft3.getRectangle()) || ship1Position.Intersects(edgeRight3.getRectangle()))
                    {
                        ship1LtR = !ship1LtR;
                    }
                    if (ship2Position.Y > panel.getRectangle().Y)
                    {
                        ship2X = width / 2 - ship2Texture.Width;
                        ship2Y = random.Next(-40 - ship2Texture.Height, -20 - ship2Texture.Height);
                        ship2Position = new Rectangle(ship2X, ship2Y, (150 * width) / 1080, (75 * height) / 1920);
                        ship2RPosition = new Rectangle(ship2X, ship2Y, (150 * width) / 1080, (75 * height) / 1920);
                    }
                    if (ship2Position.Intersects(edgeLeft2.getRectangle()) || ship2Position.Intersects(edgeRight2.getRectangle()) || ship2Position.Intersects(edgeLeft.getRectangle()) || ship2Position.Intersects(edgeRight.getRectangle()) || ship2Position.Intersects(edgeLeft3.getRectangle()) || ship2Position.Intersects(edgeRight3.getRectangle()))
                    {
                        ship2LtR = !ship2LtR;
                    }

                    if (heliLPosition.Y > panel.getRectangle().Y)
                    {
                        heliLX = random.Next((int)edgeLeft2.XPos + edgeLeft2.getRectangle().Width * width / 1080, width / 3);
                        heliLY = random.Next(-500, -200);
                        heliLPosition = new Rectangle(heliLX, heliLY, (113 * width) / 1080, (64 * height) / 1920);
                        heliL2Position = new Rectangle(heliLX, heliLY, (113 * width) / 1080, (64 * height) / 1920);
                    }


                    jetLeftPosition.Y += downY;
                    jetLeftPosition.X += jetLeftMove;
                    jetRightPosition.Y += downY;
                    jetRightPosition.X -= jetRightMove;
                    ship1Position.Y += downY;
                    ship1RPosition.Y += downY;
                    ship2Position.Y += downY;
                    ship2RPosition.Y += downY;
                    heliLPosition.Y += downY;
                    heliL2Position.Y += downY;
                    
                    if (ship1LtR == true)
                    {
                        ship1Position.X += ship1Move;
                        ship1RPosition.X += ship1Move;
                    }
                    if (ship1LtR == false)
                    {
                        ship1Position.X -= ship1Move;
                        ship1RPosition.X -= ship1Move;
                    }
                    if (ship2LtR == true)
                    {
                        ship2Position.X += ship1Move;
                        ship2RPosition.X += ship1Move;
                    }
                    if (ship2LtR == false)
                    {
                        ship2Position.X -= ship1Move;
                        ship2RPosition.X -= ship1Move;
                    }
                    if (Raid.XPos > heliLPosition.X && heliLPosition.Y > height / 3)
                    {
                        heliLPosition.X += heliMove;
                        heliL2Position.X += heliMove;
                    }

                    Raid.Deathraider(this, ship1Position, countDuration1s);
                    Raid.Deathraider(this, ship1RPosition, countDuration1s);
                    Raid.Deathraider(this, ship2Position, countDuration1s);
                    Raid.Deathraider(this, ship2Position, countDuration1s);
                    Raid.Deathraider(this, heliLPosition, countDuration1s);
                    Raid.Deathraider(this,jetLeftPosition,0);
                    Raid.Deathraider(this, jetRightPosition,0);

                    if (bullet.BulletCol(this, ship1Position, 30) ||
                     bullet.BulletCol(this, ship1RPosition, 30))
                    {
                        explosionPosition = ship1Position;
                        zestrzelony[0] = true;
                        ship1X = width / 2 - ship1Texture.Width;
                        ship1Y = random.Next(-40 - ship1Texture.Height, -20 - ship1Texture.Height);
                        ship1Position = new Rectangle(ship1X, ship1Y, (150 * width) / 1080, (75 * height) / 1920);
                        ship1RPosition = new Rectangle(ship1X, ship1Y, (150 * width) / 1080, (75 * height) / 1920);

                    }
                    if (bullet.BulletCol(this, ship2Position, 30) ||
                    bullet.BulletCol(this, ship2Position, 30))
                    {
                        ship2X = width / 2 - ship2Texture.Width;
                        ship2Y = random.Next(-40 - ship2Texture.Height, -20 - ship2Texture.Height);
                        ship2Position = new Rectangle(ship2X, ship2Y, (150 * width) / 1080, (75 * height) / 1920);
                        ship2RPosition = new Rectangle(ship2X, ship2Y, (150 * width) / 1080, (75 * height) / 1920);
                    }
                    if (bullet.BulletCol(this, heliLPosition, 60))
                    {
                        heliLX = random.Next((int)edgeLeft2.XPos + edgeLeft2.getRectangle().Width * width / 1080, width / 3);
                        heliLY = random.Next(-500, -200);
                        heliLPosition = new Rectangle(heliLX, heliLY, (113 * width) / 1080, (64 * height) / 1920);
                        heliL2Position = new Rectangle(heliLX, heliLY, (113 * width) / 1080, (64 * height) / 1920);
                    }
                    if (bullet.BulletCol(this, jetLeftPosition, 100))
                    {
                        jetLeftX = random.Next(-500, -200);
                        jetLeftY = random.Next(0, height / 3);
                        jetLeftPosition = new Rectangle(jetLeftX, jetLeftY, (80 * width) / 1080, (45 * height) / 1920);
                    }
                    if (bullet.BulletCol(this, jetRightPosition, 100))
                    {
                        jetRightX = random.Next(width, width + 500);
                        jetRightY = random.Next(0, height / 3);
                        jetRightPosition = new Rectangle(jetRightX, jetRightY, (80 * width) / 1080, (45 * height) / 1920);
                    }
                    if (bullet.BulletCol(this, bridge.getRectangle(), 500))
                    {
                        bridge.XPos = minDisplayX + edgeLeft3.getRectangle().Width;
                        bridge.YPos = edgeLeft2.YPos - edgeLeft3.getRectangle().Height + edgeLeft3.getRectangle().Height / 100;


                    }

                    break;

                case GameState.GameOver:
                    Stopped(touches);
                    break;
            }


            base.Update(gameTime);
        }


        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);
            spriteBatch.Begin();

            // StartGame();
            switch (state)
            {
                case GameState.GamePaused:
                    foreach (BaseSprite sprite in GameSprites)
                    {
                        sprite.Draw(spriteBatch);

                        spriteBatch.Draw(jetLeftTexture, jetLeftPosition, Color.White);
                        spriteBatch.Draw(jetRightTexture, jetRightPosition, Color.White);

                        if (ship1LtR == true)
                            spriteBatch.Draw(ship1Texture, ship1Position, Color.White);
                        if (ship1LtR == false)
                            spriteBatch.Draw(ship1RTexture, ship1RPosition, Color.White);


                        if (ship2LtR == true)
                            spriteBatch.Draw(ship2Texture, ship2Position, Color.White);
                        if (ship2LtR == false)
                            spriteBatch.Draw(ship2RTexture, ship2RPosition, Color.White);

                        //if (heliMoveEffect == true)
                        spriteBatch.Draw(heliLTexture, heliLPosition, Color.White);
                        //if (heliMoveEffect == false)
                        //   spriteBatch.Draw(heliL2Texture, heliL2Position, Color.White);
                    }
                    break;
                case GameState.PlayingGame:
                    foreach (BaseSprite sprite in GameSprites)
                    {
                        sprite.Draw(spriteBatch);
                    }
                    spriteBatch.Draw(jetLeftTexture, jetLeftPosition, Color.White);
                    spriteBatch.Draw(jetRightTexture, jetRightPosition, Color.White);

                    if (ship1LtR == true)
                        spriteBatch.Draw(ship1Texture, ship1Position, Color.White);
                    if (ship1LtR == false)
                        spriteBatch.Draw(ship1RTexture, ship1RPosition, Color.White);


                    if (ship2LtR == true)
                        spriteBatch.Draw(ship2Texture, ship2Position, Color.White);
                    if (ship2LtR == false)
                        spriteBatch.Draw(ship2RTexture, ship2RPosition, Color.White);

                    if (heliMoveEffect == true)
                        spriteBatch.Draw(heliLTexture, heliLPosition, Color.White);
                    if (heliMoveEffect == false)
                        spriteBatch.Draw(heliL2Texture, heliL2Position, Color.White);

                      

                    break;

                case GameState.GameOver:
                    foreach (BaseSprite sprite in EndSprites)
                    {
                        sprite.Draw(spriteBatch);
                    }
                    break;
            }

            DrawText();

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
