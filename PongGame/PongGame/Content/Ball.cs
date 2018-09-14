using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace PongGame.Content
{
    public class Ball
    {
        GraphicsDeviceManager graphics;

        private Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 acceleration;
        public Rectangle rectangle;

        public Ball(Vector2 position, int direction, Texture2D texture, GraphicsDeviceManager graphics) {
            this.position = position;
            Direction(direction);
            Debug.WriteLine("direction " + direction);
            this.texture = texture;
            this.graphics = graphics;
            
        }

        public void Move() {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width * 0.5), (int)(texture.Height * 0.5));
            if (position.X > graphics.GraphicsDevice.Viewport.Width - rectangle.Width || position.X < 0)
                velocity.X *= -1;
            if (position.Y > graphics.GraphicsDevice.Viewport.Height - rectangle.Height || position.Y < 0)
                velocity.Y *= -1;

            position += velocity * 3;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        void Direction(int d) {           
            switch (d) {
                case 0:
                    velocity = new Vector2(1, 1);
                    break;
                case 1:
                    velocity = new Vector2(-1, 1);
                    break;
                case 2:
                    velocity = new Vector2(1, -1);
                    break;
                case 3:
                    velocity = new Vector2(-1, -1);
                    break;
            }
        }

    }
}
