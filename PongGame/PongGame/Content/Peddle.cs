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

        public Peddle(Texture2D texture, Vector2 position, Vector2 size, Color color, float minSpeed, Input input) : base(texture, position, minSpeed) {
            this.Size = size;
            this.Color = color;
            rotMin = MathHelper.ToRadians(-30);
            rotMax = MathHelper.ToRadians(30);
            this.Input = input;
            offsetRot = MathHelper.ToRadians((int)Input.Position);

            //Texture2 = new Texture2D(Game1.graphics.GraphicsDevice, (int)size.X, (int)size.Y);
            //Color[] data = new Color[Texture2.Width * Texture2.Height];

            //for (int i = 0; i < data.Length; ++i)
            //    data[i] = color;
            //Texture2.SetData(data);
        }

        public void Move() {            
            rotation = MathHelper.Clamp(rotation, rotMin, rotMax);
            Direction = new Vector2((float)Math.Cos(offsetRot + rotation ), (float)Math.Sin(offsetRot + rotation));

            if (Keyboard.GetState().IsKeyDown(Input.Up))
                Position.Y -= minSpeed;
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
                Position.Y += minSpeed;
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
