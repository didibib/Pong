using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame.Content
{
    public class Peddle : Sprite
    {
        public Input Input;
        public Vector2 Size;
        public Color Color;
        public float rotationVelocity = 3f;
        private float rotMin, rotMax, offsetRot;

        public Peddle(Vector2 position, Vector2 size, Color color, float speed, Input input) : base(size, position, Color.Black) {
            this.Size = size;
            this.Color = color;
            rotMin = MathHelper.ToRadians(-30);
            rotMax = MathHelper.ToRadians(30);
            this.Input = input;
            offsetRot = MathHelper.ToRadians((int)Input.Position);
        }

        public void Move() {            
            rotation = MathHelper.Clamp(rotation, rotMin, rotMax);
            Direction = new Vector2((float)Math.Cos(offsetRot + rotation ), (float)Math.Sin(offsetRot + rotation));

            if (Keyboard.GetState().IsKeyDown(Input.Up))
                Position.Y -= velocity;
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
                Position.Y += velocity;
            else if (Keyboard.GetState().IsKeyDown(Input.Left))
                rotation -= MathHelper.ToRadians(rotationVelocity);
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
                rotation += MathHelper.ToRadians(rotationVelocity);            
        }

        public void CheckBounce(Ball ball) {
            if (ball.Position.X + ball.Rectangle.Width >= Position.X && ball.Position.X <= Position.X + Rectangle.Width) {
                if (ball.Position.Y + ball.Rectangle.Height >= Position.Y && ball.Position.Y <= Position.Y + Rectangle.Height) {
                    //ball.Direction.X *= -1;
                }
                else {
                    // UPDATE SCORE: 
                }
            }
        }


    }
}
