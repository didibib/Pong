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
        List<Sprite> Sprites;
        Color bgColor = new Color(255, 255, 255);
        enum GameStatus { BootScreen, Options, PlayerSelect, GamePlay, GameOver };
        GameStatus gameStatus = GameStatus.BootScreen;


        List<Player> Players;
        Ball Ball;

        float ballSpeed = 3f;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
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
            #endregion
            Sprites.AddRange(Players);


            base.Initialize();
        }

        protected override void LoadContent() {

            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameStatus)
            {
                case GameStatus.GamePlay:
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
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(bgColor);

            spriteBatch.Begin();
            switch (gameStatus)
            {
                case GameStatus.GamePlay:
                    foreach (Sprite sprite in Sprites) { sprite.Draw(spriteBatch); }
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
