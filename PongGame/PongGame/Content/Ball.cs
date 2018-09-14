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
    public class Ball : Sprite
    {
        public Ball(Texture2D texture, Vector2 Position, int direction) : base(texture, Position) {
            Direction(direction);            
        }

        public void Move() {            
            if (Position.X > Game1.graphics.GraphicsDevice.Viewport.Width - Rectangle.Width || Position.X < 0)
                Velocity.X *= -1;
            if (Position.Y > Game1.graphics.GraphicsDevice.Viewport.Height - Rectangle.Height || Position.Y < 0)
                Velocity.Y *= -1;

            Position += Velocity * 3;
        }

        void Direction(int d) {           
            switch (d) {
                case 0:
                    Velocity = new Vector2(1, 1);
                    break;
                case 1:
                    Velocity = new Vector2(-1, 1);
                    break;
                case 2:
                    Velocity = new Vector2(1, -1);
                    break;
                case 3:
                    Velocity = new Vector2(-1, -1);
                    break;
            }
        }

    }
}
