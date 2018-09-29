using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using PongGame.Content;

namespace PongGame
{
    /// <summary
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random;
        public List<Player> ActivePlayers;
        List<Sprite> Sprites;
        Color bgColor = new Color(255, 255, 255);
        enum GameStatus { BootMenu, Options, PlayerSelect, GamePlay, GameOver };
        GameStatus gameStatus = GameStatus.BootMenu;
        Texture2D btn;
        public string PlayerID;
        public static string wantedPlayer { get; set; } = null;


        List<Player> Players;
        Ball Ball;

        float ballSpeed = 3f;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            #region GamePlay
            random = new Random();
            Sprites = new List<Sprite>();
            Ball = new Ball(
                Content.Load<Texture2D>("Sprites/Ball"),
                new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2),
                random.Next(0, 3),
                ballSpeed);
            Sprites.Add(Ball);

            #region Players
            Players = new List<Player>() {
                new Player(
                Content.Load<Texture2D>("Sprites/Peddle"),
                Content.Load<Texture2D>("Sprites/Arrow"),
                Content.Load<Texture2D>("Sprites/Heart"),
                new Input() {
                    Up = Keys.W,
                    Down = Keys.S,
                    Left = Keys.A,
                    Right = Keys.D,
                    Position = Input.SpritePosition.Left
                },
                "Player1"),

                new Player(
                Content.Load<Texture2D>("Sprites/Peddle"),
                Content.Load<Texture2D>("Sprites/Arrow"),
                Content.Load<Texture2D>("Sprites/Heart"),
                new Input {
                    Up = Keys.Up,
                    Down = Keys.Down,
                    Left = Keys.Left,
                    Right = Keys.Right,
                    Position = Input.SpritePosition.Right
                },
                "Player2"),
                new Player(
                Content.Load<Texture2D>("Sprites/Peddle"),
                Content.Load<Texture2D>("Sprites/Arrow"),
                Content.Load<Texture2D>("Sprites/Heart"),
                new Input {
                    Up = Keys.J,
                    Down = Keys.L,
                    Left = Keys.I,
                    Right = Keys.K,
                    Position = Input.SpritePosition.Up
                },
                "Player3"),
                new Player(
                Content.Load<Texture2D>("Sprites/Peddle"),
                Content.Load<Texture2D>("Sprites/Arrow"),
                Content.Load<Texture2D>("Sprites/Heart"),
                new Input {
                    Up = Keys.J,
                    Down = Keys.L,
                    Left = Keys.I,
                    Right = Keys.K,
                    Position = Input.SpritePosition.Down
                },
                "Player4")
            };
            ActivePlayers = new List<Player>();
            //Sprites.AddRange(Players);
            #endregion
            #endregion
            #region BootMenu
            btn = this.Content.Load<Texture2D>("Sprites/btn");
            #endregion

            base.Initialize();
        }

        protected override void LoadContent() {

            spriteBatch = new SpriteBatch(GraphicsDevice);


        }

        protected override void UnloadContent() {
        }

        public static void callMethod(int x)
        {
            switch(x) {
                case 1:
                    Game1.wantedPlayer = "Player1";
                    break;
                case 2:
                    Game1.wantedPlayer = "Player2";
                    break;
                case 3:
                    Game1.wantedPlayer = "Player3";
                    break;
                case 4:
                    Game1.wantedPlayer = "Player4";
                    break;
            }
        }

        public void LoseLives(string x)
        {
            foreach (Player player in ActivePlayers) {
                if (player.name == x)
                {
                    player.lives--;
                    if (player.lives == 0)
                    {
                        ActivePlayers.RemoveAll(y => y.name == x);
                        Sprites.AddRange(ActivePlayers);
                    }

                }
            }
        }
        public void kill(string x)
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameStatus)
            {
                case GameStatus.BootMenu:
                    this.IsMouseVisible = true;
                    #region StartBtn
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed 
                        & Mouse.GetState().X >= (graphics.GraphicsDevice.Viewport.Width - btn.Width) / 2
                        & Mouse.GetState().X <= (graphics.GraphicsDevice.Viewport.Width + btn.Width) / 2)
                    {
                        if (Mouse.GetState().Y >= (graphics.GraphicsDevice.Viewport.Height - btn.Height) / 5 * 3
                            & Mouse.GetState().Y <= (graphics.GraphicsDevice.Viewport.Height + btn.Height) / 5 * 3)

                        { ActivePlayers.AddRange(Players); gameStatus = GameStatus.PlayerSelect; }

                        if (Mouse.GetState().Y >= (graphics.GraphicsDevice.Viewport.Height - btn.Height) / 5 * 4
                            & Mouse.GetState().Y <= (graphics.GraphicsDevice.Viewport.Height + btn.Height) / 5 * 4)

                        {
                            //Options, Controls, Etc.
                        }
                    }
                    #endregion
                    break;

                case GameStatus.PlayerSelect:
                    #region SelectBtns
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed 
                        & Mouse.GetState().X >= (graphics.GraphicsDevice.Viewport.Width - btn.Width) / 2
                        & Mouse.GetState().X <= (graphics.GraphicsDevice.Viewport.Width + btn.Width) / 2)
                    {
                        if (Mouse.GetState().Y >= (graphics.GraphicsDevice.Viewport.Height - btn.Height) / 4
                            & Mouse.GetState().Y <= (graphics.GraphicsDevice.Viewport.Height + btn.Height) / 4)
                        {
                            ActivePlayers.RemoveRange(2, 2);
                            Sprites.AddRange(ActivePlayers);
                            gameStatus = GameStatus.GamePlay;
                        }
                        else if (Mouse.GetState().Y >= (graphics.GraphicsDevice.Viewport.Height - btn.Height) / 2
                            & Mouse.GetState().Y <= (graphics.GraphicsDevice.Viewport.Height + btn.Height) / 2)
                        {
                            ActivePlayers.RemoveRange(3, 1);
                            Sprites.AddRange(ActivePlayers);
                            gameStatus = GameStatus.GamePlay;
                        }
                        else if (Mouse.GetState().Y >= (graphics.GraphicsDevice.Viewport.Height - btn.Height) / 4 * 3
                            & Mouse.GetState().Y <= (graphics.GraphicsDevice.Viewport.Height + btn.Height) / 4 * 3)
                        {
                            Sprites.AddRange(ActivePlayers);
                            gameStatus = GameStatus.GamePlay;
                        }
                    }
                    #endregion
                    break;

                case GameStatus.GamePlay:
                    #region GamePlay
                    if (wantedPlayer != null)
                    {
                        LoseLives(wantedPlayer);
                        wantedPlayer = null;
                    }
                    this.IsMouseVisible = false;
                    for (int i = 0; i < Sprites.Count; i++)
                        if (Sprites[i] is Ball)
                            {
                                for (int j = 0; j < Sprites.Count; j++)
                                {
                                    if (i != j)
                                    {
                                        Ball.CheckCollision(Sprites[j], Ball);
                                    }
                                }
                            }
                    foreach (Player p in Players)
                    {
                        p.Move(gameTime);
                    }
                    Ball.Move(gameTime);
                    #endregion
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(bgColor);

            spriteBatch.Begin();
            switch (gameStatus)
            {
                case GameStatus.BootMenu:
                    #region Drwbtns
                    spriteBatch.Draw(btn, destinationRectangle: new Rectangle(
                        (graphics.GraphicsDevice.Viewport.Width - btn.Width)/2, 
                        (graphics.GraphicsDevice.Viewport.Height - btn.Height)/5*3, btn.Width, btn.Height));
                    spriteBatch.Draw(btn, destinationRectangle: new Rectangle(
                        (graphics.GraphicsDevice.Viewport.Width - btn.Width)/2,
                        (graphics.GraphicsDevice.Viewport.Height - btn.Height)/5*4, btn.Width, btn.Height));
                    #endregion
                    break;
                case GameStatus.PlayerSelect:
                    #region Drwbtns2
                    spriteBatch.Draw(btn, destinationRectangle: new Rectangle(
                        (graphics.GraphicsDevice.Viewport.Width - btn.Width) / 2,
                        (graphics.GraphicsDevice.Viewport.Height - btn.Height) / 4, btn.Width, btn.Height));
                    spriteBatch.Draw(btn, destinationRectangle: new Rectangle(
                        (graphics.GraphicsDevice.Viewport.Width - btn.Width) / 2,
                        (graphics.GraphicsDevice.Viewport.Height - btn.Height) / 4 *2, btn.Width, btn.Height));
                    spriteBatch.Draw(btn, destinationRectangle: new Rectangle(
                        (graphics.GraphicsDevice.Viewport.Width - btn.Width) / 2,
                        (graphics.GraphicsDevice.Viewport.Height - btn.Height) / 4 *3, btn.Width, btn.Height));
                    #endregion
                    break;

                case GameStatus.GamePlay:
                    foreach (Sprite sprite in Sprites) { sprite.Draw(spriteBatch); }
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
