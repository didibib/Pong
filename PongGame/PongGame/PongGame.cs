using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using PongGame.Content;

namespace PongGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        private List<Sprite> Sprites;
        private Color bgColor = new Color(255, 255, 255);

        private List<Player> Players;
        public List<Ball> Balls;
        public PowerUps PowerUps;

        public Game1() {
            graphics = new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 720
            };
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            Sprites = new List<Sprite>();
            Balls = new List<Ball>() {
                new Ball(Content.Load<Texture2D>("Sprites/Ball"),
                new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2))
            };
            Sprites.AddRange(Balls);

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
            PowerUps = new PowerUps(2000,
                Content.Load<Texture2D>("Sprites/ExtraBall"),
                Content.Load<Texture2D>("Sprites/ExtraHeart")
                );

            base.Initialize();
        }

        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            PowerUps.Update(gameTime);

            for (int i = 0; i < Balls.Count; i++) {
                for (int j = 0; j < Players.Count; j++) {
                    Balls[i].CheckCollisionPlayer(Players[j]);

                }
            }
            PowerUps.CheckBallCollision(Balls, Sprites);

            foreach (Player p in Players)
                p.Move(gameTime);

            foreach (Ball b in Balls)
                b.Move(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(bgColor);

            spriteBatch.Begin();
            foreach (Sprite sprite in Sprites) {
                sprite.Draw(spriteBatch);
            }
            PowerUps.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
