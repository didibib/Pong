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
        public Player ActivePlayer;

        public string side;
        private Random random = new Random();

        public Ball(Texture2D texture, Vector2 position, float startSpeed = 3f) : base(texture, startSpeed) {
            StartDirection();
            speed = minSpeed = startSpeed;
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

        private void StartDirection() {
            int d = random.Next(0, 3);
            switch (d) {
                case 0:
                    Direction = new Vector2(1, 0);
                    break;
                case 1:
                    Direction = new Vector2(-1, 0);
                    break;
                case 2:
                    Direction = new Vector2(0, 1);
                    break;
                case 3:
                    Direction = new Vector2(0, -1);
                    break;
            }
        }

        public void CheckCollisionPlayer(Player target) {
            if (Rectangle.Intersects(target.Rectangle)) {
                if (Rectangle.Bottom > target.Rectangle.Top && Rectangle.Bottom < target.Rectangle.Bottom ||
                    Rectangle.Top > target.Rectangle.Top && Rectangle.Top < target.Rectangle.Top ||
                    Rectangle.Right < target.Rectangle.Right && Rectangle.Right > target.Rectangle.Left) {
                    Direction = target.Direction;
                    AddSpeed(target.spriteSpeed);
                    SetPlayer(target);
                }
                else if (Rectangle.Bottom > target.Rectangle.Bottom || Rectangle.Top < target.Rectangle.Top)
                    Direction.X *= -1;
                else if (Rectangle.Right < target.Rectangle.Left || Rectangle.Left > target.Rectangle.Right)
                    Direction.Y *= -1;
            }
        }

        public void SetPlayer(Player player) {
            ActivePlayer = player;
        }
    }
}
