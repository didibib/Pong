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
        private Random random = new Random();
        public Player ActivePlayer;


        public Ball(Texture2D texture, Vector2 position, float startSpeed = 3f) : base(texture, startSpeed) {
            StartDirection();
            speed = minSpeed = startSpeed;
            spriteSpeed = .2f;
            Position = position;
        }
        public void resetPotition()
        {
            //Resets the ball to the middle of the screen
            Position = new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);
            speed = minSpeed;
        }

        public void Move(GameTime gameTime)
        {
            //Hit Detection with walls, calls callPlayer() method for each wall
            if (Position.X <= 0 + Rectangle.Width / 2) // Player 1
            { Direction.X *= -1; Game1.callPlayer(1); }
            if (Position.X >= graphics.Viewport.Width - Rectangle.Width / 2) // Player 2
            { Direction.X *= -1; Game1.callPlayer(2); }
            if (Position.Y <= 0 + Rectangle.Height / 2) // Player 3
            { Direction.Y *= -1; Game1.callPlayer(3); }
            if (Position.Y >= graphics.Viewport.Height - Rectangle.Height / 2) // Player 4
            { Direction.Y *= -1; Game1.callPlayer(4); }



            speed = MathHelper.Clamp(speed, minSpeed + 0.2f, 10f);
            Position += Direction * speed;
            speed -= friction;
        }

        public void AddSpeed(float increment) {
            speed += increment;            
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

        private void StartDirection() {
            int d = random.Next(0, 3);
            switch (d) {
                case 0:
                    Direction = new Vector2(.5f, .5f);
                    break;
                case 1:
                    Direction = new Vector2(-.5f, .5f);
                    break;
                case 2:
                    Direction = new Vector2(.5f, -.5f);
                    break;
                case 3:
                    Direction = new Vector2(-.5f, -.5f);
                    break;
            }
        }
    }
}
