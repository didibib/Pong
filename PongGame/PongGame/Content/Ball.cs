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
        public float speed;
        private float minSpeed;
        private float friction = 0.01f;

        public string side;

        public Ball(Texture2D texture, Vector2 position, int direction, float startSpeed) : base(texture, startSpeed) {
            StartDirection(direction);
            speed = minSpeed = startSpeed;
            spriteSpeed = .2f;
            Position = position;
        }

        public void Move(GameTime gameTime) {
            if (Position.X >= graphics.Viewport.Width - Rectangle.Width / 2 || Position.X <= 0 + Rectangle.Width / 2)
                Direction.X *= -1;
            if (Position.Y >= graphics.Viewport.Height - Rectangle.Height / 2 || Position.Y <= 0 + Rectangle.Height / 2)
                Direction.Y *= -1;

            speed = MathHelper.Clamp(speed, minSpeed + 0.2f, 10f);
            Position += Direction * speed;
            speed -= friction;
        }

        public void AddSpeed(float increment) {
            speed += increment;            
        }       

        private void StartDirection(int d) {
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
