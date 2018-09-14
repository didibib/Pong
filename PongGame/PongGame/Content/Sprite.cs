using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongGame.Content
{
    public class Sprite
    {
        protected Texture2D Texture;
        public Rectangle Rectangle;
        public Vector2 Position;
        public Vector2 Velocity;

        public Sprite(Texture2D texture, Vector2 Position) {
            this.Texture = texture;
            this.Position = Position;            
        }

        virtual public void Draw(SpriteBatch spriteBatch) {
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}
