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
    // Elk object zal getekend moeten worden, daarom is het handig om een sprite base class te hebben
    public class Sprite
    {
        public Texture2D Texture;
        public Rectangle Rectangle;
        public string name;

        public Vector2 Position;
        public float rotation;
        public Vector2 Origin;
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public float spriteSpeed;
        public Vector2 Direction;

        // Deze is basic
        public Sprite(Texture2D texture) {
            this.Texture = texture;
            this.Origin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
        }

        // Deze is voor de ball
        protected Sprite(Texture2D texture, float acceleration) {
            this.Texture = texture;

            this.Velocity = new Vector2(0, 0);

            this.Origin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
        }

        // Deze is voor de speler
        protected Sprite(Texture2D texture, float acceleration, string name) {
            this.name = name;
            this.Texture = texture;

            this.Velocity = new Vector2(0, 0);

            this.Origin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
        }

        // Deze constructor is specifiek voor de Peddle (de pijl)
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

        // Met deze methode kunnen we de pijl groter en kleiner maken
        // Let op: de scale wordt niet geset, maar van de Texture.Width afgehaald
        virtual public void Draw(SpriteBatch spriteBatch, float alpha, int scale) {
            Rectangle = new Rectangle((int)(Position.X), (int)(Position.Y), Texture.Width - scale, Texture.Height);
            spriteBatch.Draw(Texture, Rectangle, null, Color.White * alpha, rotation, Origin, SpriteEffects.None, 0f);
        }
    }
}
