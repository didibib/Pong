using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;

namespace PongGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random;
        Color bgColor = new Color(0, 255, 255);

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
            Ball = new Content.Ball(
                Content.Load<Texture2D>("Sprites/Ball"),
                new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2),
                random.Next(0, 3));
            Player1 = new Content.Peddle(
                new Vector2(20, graphics.GraphicsDevice.Viewport.Height / 2), 
                new Vector2(20, 80), 
                Color.Black, peddleSpeed) 
                {
                Input = new Content.Input {
                    Up = Keys.W,
                    Down = Keys.S
                }
            };
            Player2 = new Content.Peddle(
                new Vector2(graphics.GraphicsDevice.Viewport.Width - 40, graphics.GraphicsDevice.Viewport.Height / 2),
                new Vector2(20, 80),
                Color.Black, peddleSpeed) 
                {
                Input = new Content.Input {
                    Up = Keys.Up,
                    Down = Keys.Down
                }
            };


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
            Player1.CheckBounce(Ball);
            Player2.Move();
            Player2.CheckBounce(Ball);
            Ball.Move();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(bgColor);

            spriteBatch.Begin();
            Player1.Draw(spriteBatch);
            Player2.Draw(spriteBatch);
            Ball.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
