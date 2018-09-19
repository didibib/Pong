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

        GraphicsDevice graphics = Game1.graphics.GraphicsDevice;

        public Ball(Texture2D texture, Vector2 Position, int direction) : base(texture, Position) {
            StartDirection(direction);            
        }

        public void Move() {            
            if (Position.X >= graphics.Viewport.Width - Rectangle.Width/2 || Position.X <= 0 - Rectangle.Width / 2)
                Direction.X *= -1;
            if (Position.Y >= graphics.Viewport.Height - Rectangle.Height/2 || Position.Y <= 0 - Rectangle.Height / 2)
                Direction.Y *= -1;

            Velocity += Acceleration;
            Position += Direction * minSpeed;            
        }

        void StartDirection(int d) {           
            switch (d) {
                case 0:
                    Direction = new Vector2(1, 1);
                    break;
                case 1:
                    Direction = new Vector2(-1, 1);
                    break;
                case 2:
                    Direction = new Vector2(1, -1);
                    break;
                case 3:
                    Direction = new Vector2(-1, -1);
                    break;
            }
        }

    }
}
