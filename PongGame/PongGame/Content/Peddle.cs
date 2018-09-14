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
    public class Peddle
    {
        private Texture2D rectangle;
        public Vector2 position;
        public Vector2 size;
        public Vector2 velocity;
        public Color color;
        public float speed;
        public Input input;

        public Peddle(Vector2 position, Vector2 size, Color color, float speed, GraphicsDeviceManager graphics) {
            this.position = position;
            this.size = size;
            this.color = color;
            this.speed = speed;

            rectangle = new Texture2D(graphics.GraphicsDevice, (int)size.X, (int)size.Y);
            Color[] data = new Color[rectangle.Width * rectangle.Height];

            for (int i = 0; i < data.Length; ++i)
                data[i] = Color.Black;
            rectangle.SetData(data);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(rectangle, position, color);
        }

        public void Move() {
            if (Keyboard.GetState().IsKeyDown(input.Up))
                position.Y -= speed;
            else if (Keyboard.GetState().IsKeyDown(input.Down))
                position.Y += speed;            
        }

        public void CheckBounce(Ball ball) {
            if(ball.position.X + ball.rectangle.Width >= position.X && ball.position.X <= position.X + rectangle.Width) {    
                if (ball.position.Y + ball.rectangle.Height >= position.Y && ball.position.Y <= position.Y + rectangle.Height) {
                    ball.velocity.X *= -1;
                } else {
                    // UPDATE SCORE: 
                }
            }
        }


    }
}
