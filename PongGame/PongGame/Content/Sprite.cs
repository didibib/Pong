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
        public Rectangle Rectangle;
        public Vector2 Position;
        public float rotation;
        public float minSpeed;
        public Vector2 Origin;
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public Vector2 Direction;

        protected Sprite(Texture2D texture, Vector2 position, float minSpeed = 5f) {
            this.Texture = texture;

            //Color[] data = new Color[Texture.Width * Texture.Height];

            //for (int i = 0; i < data.Length; ++i)
            //    data[i] = Color.Black;
            //Texture.SetData(data);

            this.Position = position;
            this.minSpeed = minSpeed;
            this.Origin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
            Position += Origin;
        }

        virtual public void Draw(SpriteBatch spriteBatch) {
            Rectangle = new Rectangle((int)(Position.X - Origin.X), (int)(Position.Y - Origin.Y), Texture.Width, Texture.Height);
            spriteBatch.Draw(Texture, Position, null, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
        }

        public void CheckCollision(Sprite sprite) {
            if (Rectangle.Intersects(sprite.Rectangle)) {
                Direction = sprite.Direction;
            } 

        }
    }
}
