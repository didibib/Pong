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
        Color bgColor = new Color(255, 255, 255);
        enum GameStatus { BootMenu, Options, PlayerSelect, GamePlay, GameOver };
        GameStatus gameStatus = GameStatus.BootMenu;
        Texture2D TwoPlayersBtn, ThreePlayersBtn, FourPlayersBtn, ControlsBtn, ResetBtn, StartBtn;
        public static string wantedPlayer = null;
        SpriteFont font;


        List<Player> Players;
        public List<Ball> Balls;
        public PowerUps PowerUps;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            #region GamePlay
            random = new Random();
            Balls = new List<Ball>() {
                new Ball(Content.Load<Texture2D>("Sprites/Ball"),
                new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2))
            };

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
            PowerUps = new PowerUps(10,
                Content.Load<Texture2D>("Sprites/ExtraBall"),
                Content.Load<Texture2D>("Sprites/ExtraHeart")
                );
            base.Initialize();
        }

        protected override void LoadContent() {
            TwoPlayersBtn = this.Content.Load<Texture2D>("Sprites/Buttons/2PlayersBtn");
            ThreePlayersBtn = this.Content.Load<Texture2D>("Sprites/Buttons/3PlayersBtn");
            FourPlayersBtn = this.Content.Load<Texture2D>("Sprites/Buttons/4PlayersBtn");
            ControlsBtn = this.Content.Load<Texture2D>("Sprites/Buttons/ControlsBtn");
            ResetBtn = this.Content.Load<Texture2D>("Sprites/Buttons/ResetBtn");
            StartBtn = this.Content.Load<Texture2D>("Sprites/Buttons/StartBtn");
            font = Content.Load<SpriteFont>("Arial");
            font = Content.Load<SpriteFont>("Arial");
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent() {
            StartBtn.Dispose();
        }

        public static void callPlayer(int x) {
            switch (x) {
                case 1:
                    wantedPlayer = "Player1";
                    break;
                case 2:
                    wantedPlayer = "Player2";
                    break;
                case 3:
                    wantedPlayer = "Player3";
                    break;
                case 4:
                    wantedPlayer = "Player4";
                    break;
            }
        }

        public void LoseLives(string x) {
            // Tijdens het itereren over een lijst verwijderen we eventueel objecten, daarom doen dat in omgekeerde richting
            // https://stackoverflow.com/questions/1582285/how-to-remove-elements-from-a-generic-list-while-iterating-over-it
            for (int i = ActivePlayers.Count; i-- > 0;) {
                if (ActivePlayers[i].name == x) {
                    ActivePlayers[i].lives--;
                    if (ActivePlayers[i].lives <= 0) {
                        ActivePlayers.RemoveAt(i);
                    }
                    Balls.RemoveRange(1, Balls.Count-1);
                    Balls[0].resetPotition();
                }
            }

            if (ActivePlayers.Count == 1) {
                gameStatus = GameStatus.GameOver;
            }
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameStatus) {
                case GameStatus.BootMenu:
                    while (this.IsMouseVisible == false) { this.IsMouseVisible = true; }
                    #region StartBtn
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed
                        & Mouse.GetState().X >= (graphics.GraphicsDevice.Viewport.Width - StartBtn.Width) / 2
                        & Mouse.GetState().X <= (graphics.GraphicsDevice.Viewport.Width + StartBtn.Width) / 2) {
                        if (Mouse.GetState().Y >= (graphics.GraphicsDevice.Viewport.Height - StartBtn.Height) / 5 * 3
                            & Mouse.GetState().Y <= (graphics.GraphicsDevice.Viewport.Height + StartBtn.Height) / 5 * 3) { ActivePlayers.AddRange(Players); gameStatus = GameStatus.PlayerSelect; }

                        if (Mouse.GetState().Y >= (graphics.GraphicsDevice.Viewport.Height - StartBtn.Height) / 5 * 4
                            & Mouse.GetState().Y <= (graphics.GraphicsDevice.Viewport.Height + StartBtn.Height) / 5 * 4) {
                            //Controls
                        }
                    }
                    #endregion
                    break;

                case GameStatus.PlayerSelect:
                    #region SelectBtns
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed
                        & Mouse.GetState().X >= (graphics.GraphicsDevice.Viewport.Width - StartBtn.Width) / 2
                        & Mouse.GetState().X <= (graphics.GraphicsDevice.Viewport.Width + StartBtn.Width) / 2) {
                        if (Mouse.GetState().Y >= (graphics.GraphicsDevice.Viewport.Height - StartBtn.Height) / 4
                            & Mouse.GetState().Y <= (graphics.GraphicsDevice.Viewport.Height + StartBtn.Height) / 4) {
                            ActivePlayers.RemoveRange(2, 2);
                            gameStatus = GameStatus.GamePlay;
                        }
                        else if (Mouse.GetState().Y >= (graphics.GraphicsDevice.Viewport.Height - StartBtn.Height) / 2
                            & Mouse.GetState().Y <= (graphics.GraphicsDevice.Viewport.Height + StartBtn.Height) / 2) {
                            ActivePlayers.RemoveRange(3, 1);
                            gameStatus = GameStatus.GamePlay;
                        }
                        else if (Mouse.GetState().Y >= (graphics.GraphicsDevice.Viewport.Height - StartBtn.Height) / 4 * 3
                            & Mouse.GetState().Y <= (graphics.GraphicsDevice.Viewport.Height + StartBtn.Height) / 4 * 3) {
                            gameStatus = GameStatus.GamePlay;
                        }
                    }
                    #endregion
                    break;

                case GameStatus.GamePlay:
                    #region GamePlay
                    if (wantedPlayer != null) {
                        LoseLives(wantedPlayer);
                        wantedPlayer = null;
                    }
                    this.IsMouseVisible = false;
                    for (int i = 0; i < Balls.Count; i++)
                        for (int j = 0; j < ActivePlayers.Count; j++) {
                            Balls[i].CheckCollisionPlayer(ActivePlayers[j]);
                        }
                    foreach (Player p in Players) {
                        p.Move(gameTime);
                    }
                    foreach (Ball b in Balls)
                        b.Move(gameTime);
                    PowerUps.Update(gameTime);
                    PowerUps.CheckBallCollision(Balls);
                    
                    #endregion
                    break;
                case GameStatus.GameOver:
                    #region GameOver
                    while (this.IsMouseVisible == false) { this.IsMouseVisible = true; }
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed
                        & Mouse.GetState().X >= (graphics.GraphicsDevice.Viewport.Width - StartBtn.Width) / 2
                        & Mouse.GetState().X <= (graphics.GraphicsDevice.Viewport.Width + StartBtn.Width) / 2
                        & Mouse.GetState().Y >= (graphics.GraphicsDevice.Viewport.Height - StartBtn.Height) / 2
                        & Mouse.GetState().Y <= (graphics.GraphicsDevice.Viewport.Height + StartBtn.Height) / 2) {
                        ActivePlayers.AddRange(Players);
                        for (int i = ActivePlayers.Count; i-- > 0;) { ActivePlayers[i].lives = 3; }
                        gameStatus = GameStatus.BootMenu;
                    }
                    break;
                    #endregion
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(bgColor);

            spriteBatch.Begin();
            switch (gameStatus) {
                case GameStatus.BootMenu:
                    #region DrwStartBtns
                    spriteBatch.Draw(StartBtn, destinationRectangle: new Rectangle(
                        (graphics.GraphicsDevice.Viewport.Width - StartBtn.Width) / 2,
                        (graphics.GraphicsDevice.Viewport.Height - StartBtn.Height) / 5 * 3, StartBtn.Width, StartBtn.Height));
                    spriteBatch.Draw(ControlsBtn, destinationRectangle: new Rectangle(
                        (graphics.GraphicsDevice.Viewport.Width - StartBtn.Width) / 2,
                        (graphics.GraphicsDevice.Viewport.Height - StartBtn.Height) / 5 * 4, StartBtn.Width, StartBtn.Height));
                    #endregion
                    break;
                case GameStatus.PlayerSelect:
                    #region DrwStartBtns2
                    spriteBatch.Draw(TwoPlayersBtn, destinationRectangle: new Rectangle(
                        (graphics.GraphicsDevice.Viewport.Width - StartBtn.Width) / 2,
                        (graphics.GraphicsDevice.Viewport.Height - StartBtn.Height) / 4, StartBtn.Width, StartBtn.Height));
                    spriteBatch.Draw(ThreePlayersBtn, destinationRectangle: new Rectangle(
                        (graphics.GraphicsDevice.Viewport.Width - StartBtn.Width) / 2,
                        (graphics.GraphicsDevice.Viewport.Height - StartBtn.Height) / 4 * 2, StartBtn.Width, StartBtn.Height));
                    spriteBatch.Draw(FourPlayersBtn, destinationRectangle: new Rectangle(
                        (graphics.GraphicsDevice.Viewport.Width - StartBtn.Width) / 2,
                        (graphics.GraphicsDevice.Viewport.Height - StartBtn.Height) / 4 * 3, StartBtn.Width, StartBtn.Height));
                    #endregion
                    break;

                case GameStatus.GamePlay:
                    foreach (Sprite sprite in ActivePlayers) { sprite.Draw(spriteBatch); }
                    foreach(Sprite sprite in Balls) { sprite.Draw(spriteBatch); }
                    PowerUps.Draw(spriteBatch);
                    break;
                case GameStatus.GameOver:
                    spriteBatch.DrawString(font, ActivePlayers[0].name.ToString() + " has won!", new Vector2(30, graphics.GraphicsDevice.Viewport.Height - 50), Color.Black);
                    spriteBatch.Draw(ResetBtn, destinationRectangle: new Rectangle(
                        (graphics.GraphicsDevice.Viewport.Width - StartBtn.Width) / 2,
                        (graphics.GraphicsDevice.Viewport.Height - StartBtn.Height) / 2, StartBtn.Width, StartBtn.Height));
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
