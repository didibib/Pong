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
        SpriteBatch spriteBatch;
        Random random;
        List<Content.Sprite> Sprites;
        Color bgColor = new Color(255, 255, 255);

        Content.Peddle Player1;
        Content.Peddle Player2;
        Content.Ball Ball;

        float peddleSpeed = 5;

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
                random.Next(0, 3));
            Sprites.Add(Ball);

            Player1 = new Peddle(
                new Vector2(20, graphics.GraphicsDevice.Viewport.Height / 2),
                new Vector2(20, 80),
                Color.Black, peddleSpeed, new Input {
                    Up = Keys.W,
                    Down = Keys.S,
                    Left = Keys.A,
                    Right = Keys.D,
                    Position = Input.SpritePosition.Left
                });
            Sprites.Add(Player1);
            Player2 = new Peddle(
                new Vector2(graphics.GraphicsDevice.Viewport.Width - 40, graphics.GraphicsDevice.Viewport.Height / 2),
                new Vector2(20, 80),
                Color.Black, peddleSpeed, new Input {
                    Up = Keys.Up,
                    Down = Keys.Down,
                    Left = Keys.Left,
                    Right = Keys.Right,
                    Position = Input.SpritePosition.Right
                });
            Sprites.Add(Player2);

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

            Player1.Move();
            //Player1.CheckBounce(Ball);
            Player2.Move();
            //Player2.CheckBounce(Ball);
            Ball.Move();

            for (int i = 0; i < Sprites.Count; i++) {
                for (int j = 0; j < Sprites.Count; j++) {
                    if(i != j) {
                        Sprites[i].CheckCollision(Sprites[j]);
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(bgColor);

            spriteBatch.Begin();
            foreach(Content.Sprite sprite in Sprites) {
                sprite.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
