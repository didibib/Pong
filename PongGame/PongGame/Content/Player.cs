using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame.Content
{
    public class Player : Sprite
    {
        GraphicsDevice graphics = Game1.graphics.GraphicsDevice;

        public bool bCollided;
        public Input PlayerInput;
        public Peddle Peddle;
        private KeyboardState currentKState;
        private Texture2D heartTexture;
        private float speed;

        private int dir;
        private float maxVelocity = 6f;

        private List<Vector2> heartPos = new List<Vector2>();
        private Vector2 heartDir;
        private Vector2 heartSteps = new Vector2(20, 20);
        private Vector2 heartOrigin;
        private Vector2 center;
        public int lives { get; set; }
        public bool dead { get; set; } = false;

        public Player(Texture2D texture, Texture2D arrowTexture, Texture2D heartTexture, Input input, string name, int lives = 3) : base(texture, 0.24f, name) {
            this.name = name;
            this.PlayerInput = input;
            this.lives = lives;
            this.heartTexture = heartTexture;

            rotation = MathHelper.ToRadians((int)input.Position); // Op basis van de Input.StartPosition draaien we de speler de juiste kant op
            StartDirection(input.Position); // Hier wordt de Position en Acceleration bepaalt 

            heartDir = Acceleration; // Acceleration gaat maar in één richting en dat willen we ook voor de levens
            heartSteps *= heartDir; // Een vector2 wordt gebruikt voor de stappen in beide richtingen en vermenigvuldig dat met de heartDir zodat één as op nul wordt gezet... 
            // ... en de levens op één lijn komen te liggen.
            heartOrigin = new Vector2(heartTexture.Width / 2, heartTexture.Height / 2);
            center = new Vector2(heartTexture.Width / 2 * lives , heartTexture.Width / 2 * lives );
            center *= heartDir;

            for (int i = 0; i < 50; i++) { // Het is onduidelijk hoeveel levens een speler zal krijgen dus we creëen een lijst van veel posities
                heartPos.Add(new Vector2(Position.X + heartSteps.X * i - center.X, Position.Y + heartSteps.Y * i - center.Y));
            }
            
            Peddle = new Peddle(arrowTexture, PlayerInput, Position, name); // De peddle (de pijl) is een child van player
        }

        public void Move(GameTime gameTime) {
            Velocity.Y = MathHelper.Clamp(Velocity.Y, 0, maxVelocity);
            Velocity.X = MathHelper.Clamp(Velocity.X, 0, maxVelocity);
            if (Position.Y <= 0)
                Position.Y = graphics.Viewport.Height;
            else if (Position.Y >= graphics.Viewport.Height)
                Position.Y = 0;
            else if (Position.X <= 0)
                Position.X = graphics.Viewport.Width;
            else if (Position.X >= graphics.Viewport.Width) {
                Position.X = 0;
            }

            currentKState = Keyboard.GetState();
            if (currentKState.IsKeyDown(PlayerInput.Up)) {
                dir = 1;
                speed = .2f;
                Velocity += Acceleration * speed;
                Position -= Velocity;
            }
            else if (currentKState.IsKeyDown(PlayerInput.Down)) {
                dir = -1;
                speed = .2f;
                Velocity += Acceleration * speed;
                Position += Velocity;
            }
            else {
                speed = .1f;
                if (Velocity.Y != 0 || Velocity.X != 0) {
                    Velocity -= Acceleration * speed;
                    Position -= Velocity * dir;
                }

            }
            Direction = Peddle.Move(Position, PlayerInput, Velocity, gameTime); // De collisions gaan tussen Ball en Player, maar de informatie van de pijl (aka Peddle) staan in zijn eigen object.
                                                                                // Die bepaalt oa de direction, die we hier ophalen. Hier laten we de pijl ook met de speler mee bewegen

            spriteSpeed = Peddle.GetSpeed(); // spriteSpeed is wat we als "boost" aan de bal meegegeven

        }

        public override void Draw(SpriteBatch spriteBatch) {
            Peddle.Draw(spriteBatch, 0.3f, Peddle.GetScale()); // De peddle wordt hier getekend              

            // Eerder was gezegd dat we de sprites roteren op bases van de Input.SpritePosition, maar dat werkte niet lekker met de Rectangle, die doen we hier dus apart.
            if (PlayerInput.Position == Input.SpritePosition.Left || PlayerInput.Position == Input.SpritePosition.Right)
                base.Draw(spriteBatch);
            else {
                Rectangle = new Rectangle((int)(Position.X - Origin.Y), (int)(Position.Y - Origin.X), Texture.Height, Texture.Width);
                spriteBatch.Draw(Texture, Position, null, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);                
            }

            for (int i = 0; i < lives; i++) {
                spriteBatch.Draw(heartTexture, heartPos[i], null, Color.White * .8f, rotation, heartOrigin, 1f, SpriteEffects.None, 0f);
            }
        }


        private void StartDirection(Input.SpritePosition sp) {
            switch (sp) {
                case Input.SpritePosition.Up:
                    Position = new Vector2(graphics.Viewport.Width / 2, Texture.Width);
                    Acceleration = new Vector2(1, 0);
                    break;
                case Input.SpritePosition.Down:
                    Position = new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height - Texture.Width);
                    Acceleration = new Vector2(1, 0);
                    break;
                case Input.SpritePosition.Left:
                    Position = new Vector2(Texture.Width, graphics.Viewport.Height / 2);
                    Acceleration = new Vector2(0, 1);
                    break;
                case Input.SpritePosition.Right:
                    Position = new Vector2(graphics.Viewport.Width - Texture.Width, graphics.Viewport.Height / 2);
                    Acceleration = new Vector2(0, 1);
                    break;
            }
        }
    }
}
