using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongGame.Content
{
    public class Sprite
    {
        protected Texture2D Texture;
        protected Rectangle Rectangle;
        public string name;

        public Vector2 Position;
        public float rotation;
        public Vector2 Origin;
        public Vector2 Velocity;
        public Vector2 Acceleration;
        protected float spriteSpeed;
        public Vector2 Direction;

        protected Sprite(Texture2D texture, float acceleration) {
            this.Texture = texture;

            this.Velocity = new Vector2(0, 0);

            this.Origin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
        }

        protected Sprite(Texture2D texture, float acceleration, string name) {
            this.name = name;
            this.Texture = texture;

            this.Velocity = new Vector2(0, 0);

            this.Origin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
        }

        protected Sprite(Texture2D texture, Vector2 position, string name) {
            this.name = name;
            this.Texture = texture;

            this.Position = position;
            this.Origin = new Vector2(0, this.Texture.Height / 2);
        }

        virtual public void Draw(SpriteBatch spriteBatch) {
            Rectangle = new Rectangle((int)(Position.X - Origin.X), (int)(Position.Y - Origin.Y), Texture.Width, Texture.Height);
            spriteBatch.Draw(Texture, Position, null, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
        }

        virtual public void Draw(SpriteBatch spriteBatch, float alpha) {
            Rectangle = new Rectangle((int)(Position.X - Origin.X), (int)(Position.Y - Origin.Y), Texture.Width, Texture.Height);
            spriteBatch.Draw(Texture, Position, null, Color.White * alpha, rotation, Origin, 1f, SpriteEffects.None, 0f);
        }

        virtual public void Draw(SpriteBatch spriteBatch, float alpha, int scale) {
            Rectangle = new Rectangle((int)(Position.X), (int)(Position.Y), Texture.Width - scale, Texture.Height);
            spriteBatch.Draw(Texture, Rectangle, null, Color.White * alpha, rotation, Origin, SpriteEffects.None, 0f);
        }

        public void CheckCollision(Sprite target, Ball ball) {
            if (Rectangle.Intersects(target.Rectangle)) {
                if (Rectangle.Bottom > target.Rectangle.Top && Rectangle.Bottom < target.Rectangle.Bottom ||
                    Rectangle.Top > target.Rectangle.Top && Rectangle.Top < target.Rectangle.Top ||
                    Rectangle.Right < target.Rectangle.Right && Rectangle.Right > target.Rectangle.Left) {
                    Direction = target.Direction;
                    ball.AddSpeed(target.spriteSpeed);
                }
                else if (Rectangle.Bottom > target.Rectangle.Bottom || Rectangle.Top < target.Rectangle.Top)
                    Direction.X *= -1;
                else if (Rectangle.Right < target.Rectangle.Left || Rectangle.Left > target.Rectangle.Right)
                    Direction.Y *= -1;

            }
        }
    }
}
