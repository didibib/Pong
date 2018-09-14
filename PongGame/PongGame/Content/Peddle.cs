using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace PongGame.Content
{
    public class Peddle : Sprite
    {
        private Texture2D rectangle;
        public Vector2 size;
        public Color Color;
        public float speed;
        public Input Input;

        public Peddle(Vector2 position, Vector2 size, Color color, float speed) : base(null, position) {
            this.size = size;
            this.Color = color;
            this.speed = speed;

            rectangle = new Texture2D(Game1.graphics.GraphicsDevice, (int)size.X, (int)size.Y);
            Color[] data = new Color[rectangle.Width * rectangle.Height];

            for (int i = 0; i < data.Length; ++i)
                data[i] = Color.Black;
            rectangle.SetData(data);
        }

        override public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(rectangle, Position, Color);
        }

        public void Move() {
            if (Keyboard.GetState().IsKeyDown(Input.Up))
                Position.Y -= speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
                Position.Y += speed;
        }

        public void CheckBounce(Ball ball) {
            if (ball.Position.X + ball.Rectangle.Width >= Position.X && ball.Position.X <= Position.X + rectangle.Width) {
                if (ball.Position.Y + ball.Rectangle.Height >= Position.Y && ball.Position.Y <= Position.Y + rectangle.Height) {
                    ball.Velocity.X *= -1;
                }
                else {
                    // UPDATE SCORE: 
                }
            }
        }


    }
}
