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
    public class PowerUps
    {
        GraphicsDevice graphics = Game1.graphics.GraphicsDevice;

        private Dictionary<Texture2D, string> ListPowerUps;
        private List<Sprite> ActivePowerUps;
        private float timer;
        private int interval;
        private Vector2 Position;
        private Random random = new Random();
        private int margin = 100;
        private int index;


        public PowerUps(int interval, Texture2D extraBall, Texture2D extraHeart) {
            ListPowerUps = new Dictionary<Texture2D, string>();
            ListPowerUps.Add(extraBall, "extraBall");
            ListPowerUps.Add(extraHeart, "extraHeart");
            ActivePowerUps = new List<Sprite>();
            this.interval = interval;
        }

        public void Update(GameTime gameTime) {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer >= interval) {
                SpawnPowerUp();
                timer = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Sprite sprite in ActivePowerUps) {
                sprite.Draw(spriteBatch);
            }
        }

        public void CheckBallCollision(List<Ball> balls, List<Sprite> sprites) {
            for (int i = 0; i < balls.Count; i++) {
                for (int j = 0; j < ActivePowerUps.Count; j++) {
                    if (balls[i].Rectangle.Intersects(ActivePowerUps[j].Rectangle)) {
                        string value = ListPowerUps[ActivePowerUps[j].Texture];
                        switch (value) {
                            case "extraBall":
                                Ball ball = new Ball(balls[0].Texture, new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2));
                                balls.Add(ball);
                                sprites.Add(ball);
                                ActivePowerUps.RemoveAt(j);
                                break;
                            case "extraHeart":
                                if (balls[i].ActivePlayer != null)
                                    balls[i].ActivePlayer.lives += 1;
                                ActivePowerUps.RemoveAt(j);
                                break;
                        }

                    }
                }
            }
        }

        private void SpawnPowerUp() {
            Position.X = random.Next(margin, graphics.Viewport.Width - margin);
            Position.Y = random.Next(margin, graphics.Viewport.Height - margin);
            index = random.Next(0, ListPowerUps.Count);
            Sprite newPowerUp = new Sprite(ListPowerUps.ElementAt(index).Key);
            newPowerUp.Position = Position;
            ActivePowerUps.Add(newPowerUp);
        }
    }
}
