using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
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
        public Vector2 Origin;
        public float velocity;
        public Vector2 Direction;

        protected Sprite(Texture2D texture, Vector2 position, float velocity = 5f) {
            this.Texture = texture;

            Color[] data = new Color[Texture.Width * Texture.Height];

            for (int i = 0; i < data.Length; ++i)
                data[i] = Color.Black;
            Texture.SetData(data);

            this.Position = position;
            this.velocity = velocity;
            this.Origin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
        }

        protected Sprite(Vector2 size, Vector2 position, Color color, float velocity = 3f) {            
            Texture = new Texture2D(Game1.graphics.GraphicsDevice, (int)size.X, (int)size.Y);
            Color[] data = new Color[Texture.Width * Texture.Height];

            for (int i = 0; i < data.Length; ++i)
                data[i] = color;
            Texture.SetData(data);

            this.Position = position;
            this.velocity = velocity;
            this.Origin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
        }

        virtual public void Draw(SpriteBatch spriteBatch) {
            UpdateRectangle();
            spriteBatch.Draw(Texture, Rectangle, null, Color.White, rotation, Origin, SpriteEffects.None, 0f);
        }

        protected void UpdateRectangle() {
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public void CheckCollision(Sprite sprite) {
            if (Rectangle.Intersects(sprite.Rectangle)) {
                Direction = sprite.Direction;
            } 

        }

        protected void OnCollision() {

        }
    }
}
