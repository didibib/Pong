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

    // Deze class wordt gebuikt voor de pijl bij de speler
    // Beetje ongelukkige naam maar oké
    public class Peddle : Sprite
    {
        private float rotMin, rotMax, offsetRot;
        private int maxAngle = 45;
        public float rotationVelocity = 1f;
        private float looseAngle = MathHelper.ToRadians(10); // Als de speler wordt bewogen krijgt de pijl wat "swing"
        private float incMin, incMax;
        private float scale, scaleMax;

        public float incSpeed = 2f;
        public bool bCharged = false;
        private bool setTimer = false;
        private float maxReleaseTime = 100;  //We gebruiken een een time frame voor de speler om succesvol de bal te "slaan", 
        //anders heeft hij maar één frame oid. In dit geval geven we hem 0.1 seconde
        public float chargeTimer;
        private float timer;

        private Input Input;
        private KeyboardState newKState;
        private KeyboardState oldKState;


        public Peddle(Texture2D texture, Input input, Vector2 position, string name) : base(texture, position, "Peddle " + name) {
            Position = position;
            offsetRot = MathHelper.ToRadians((int)input.Position); // Als spelers een andere kant op kijken moet hun richting ook meedraaien
            // Anders is de richting Direction en van de pijl van alle spelers naar rechts
            rotation = MathHelper.ToRadians((int)input.Position);
            rotMin = MathHelper.ToRadians((int)input.Position - maxAngle);
            rotMax = MathHelper.ToRadians((int)input.Position + maxAngle);
            incMin = MathHelper.ToRadians(-maxAngle);
            incMax = MathHelper.ToRadians(maxAngle);
            scaleMax = Texture.Width / 2;
            chargeTimer = maxReleaseTime;
        }

        public Vector2 Move(Vector2 position, Input input, Vector2 velocity, GameTime gameTime) {
            Input = input;
            Velocity = velocity;
            Position = position;
            timer = gameTime.ElapsedGameTime.Milliseconds;

            newKState = Keyboard.GetState();
            if (newKState.IsKeyDown(Input.Left)) {
                rotation -= MathHelper.ToRadians(rotationVelocity); // De pijl draait de ene kant op
                scale = scaleMax;
                bCharged = true; // Het roteren van de pijl en de charge zitten in een knop ipv een aparte knop voor charge
                // Dan moet de speler timen
            }
            else if (newKState.IsKeyDown(Input.Right)) {
                rotation += MathHelper.ToRadians(rotationVelocity); // De pijl draait de andere kant op
                scale = scaleMax;
                bCharged = true;
            }
            else if (newKState.IsKeyDown(Input.Up)) {
                rotation = Lerp(rotation, -looseAngle + offsetRot, .1f);
            }
            else if (newKState.IsKeyDown(Input.Down)) {
                rotation = Lerp(rotation, looseAngle + offsetRot, .1f);
            }
            else {
                scale = Lerp(scale, 0, 0.8f); // De pijl krijgt langzaam zijn lengte terug
                if (scale < .1f) // Om de pijl een push effect te geven wachten we eerst tot de pijl zijn originele lengte heeft ...
                    rotation = Lerp(rotation, offsetRot, 0.1f); // ... voordat de pijl weer loodrecht op de player gaat staan
            }

            if (newKState.IsKeyUp(Input.Left) && oldKState.IsKeyDown(Input.Left) || Keyboard.GetState().IsKeyUp(Input.Right) && oldKState.IsKeyDown(Input.Right)) { // Wanneer de desbetreffende key gereleased wordt start de time frame
                setTimer = true; // We gebruiken een een time frame voor de speler om succesvol de bal te "slaan", anders heeft hij maar één frame oid.
            }

            if (setTimer) {
                chargeTimer -= timer;
            }

            if (chargeTimer <= 0 && setTimer) {
                chargeTimer = maxReleaseTime;
                bCharged = false;
                setTimer = false;
            }
            


            rotation = MathHelper.Clamp(rotation, rotMin, rotMax); // De pijl mag niet 360 graden rond kunnen draaien
            Direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));

            oldKState = newKState;

            return Direction;
        }

        public int GetScale() {
            return (int)scale;
        }

        public float GetSpeed() {
            if (bCharged && setTimer) {
                return incSpeed + Velocity.X + Velocity.Y; // Er zit een kleine bug in het spel dat hier tot een feature gemaakt wordt
                // De bug is namelijk dat de velocity van de speler niet afremd wanneer hij tegengestelde richting op gaat, 
                // dus wanneer je constant op-en-neer of heen-en-weer gaat dan behoud je je velocity.
                // Dat maakt de controle soms lastig, daarom wanneer je de bal succesvol pushed terwijl je keihard aan het strafen bent,
                // dan wordt de velocity bij de de minimale snelheid, die aan de bal wordt toegevoegd, opgeteld.
                // De velocity gaat maar in één richting, dus een van de assen zal altijd nul zijn.
            }
            return 0;
            
        }

        float Lerp(float start, float stop, float amt) { // https://stackoverflow.com/questions/4353525/floating-point-linear-interpolation
            return start + amt * (stop - start);
        }
    }
}
