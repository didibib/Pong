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
        private float rotMin, rotMax, offsetRot;
        private int maxAngle = 45;
        public float rotationVelocity = 1f;
        private float looseAngle = MathHelper.ToRadians(10);
        private float incrementedRot = 0;
        private float incMin, incMax;
        private float scale, scaleMax;

        public float incSpeed = 2f;
        public bool bCharged = false;
        private bool setTimer = false;
        private float maxReleaseTime = 100;
        public float chargeTimer;
        private float timer;

        private Input Input;
        private KeyboardState newKState;
        private KeyboardState oldKState;


        public Peddle(Texture2D texture, Input input, Vector2 position, string name) : base(texture, position, "Peddle " + name) {
            Position = position;
            offsetRot = MathHelper.ToRadians((int)input.Position);
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
                rotation -= MathHelper.ToRadians(rotationVelocity);
                incrementedRot -= MathHelper.ToRadians(rotationVelocity);
                scale = scaleMax;
                bCharged = true;
            }
            else if (newKState.IsKeyDown(Input.Right)) {
                rotation += MathHelper.ToRadians(rotationVelocity);
                incrementedRot += MathHelper.ToRadians(rotationVelocity);
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
                scale = Lerp(scale, 0, 0.8f);
                if (scale < .1f)
                    rotation = Lerp(rotation, offsetRot, 0.1f);
            }

            if (newKState.IsKeyUp(Input.Left) && oldKState.IsKeyDown(Input.Left) || Keyboard.GetState().IsKeyUp(Input.Right) && oldKState.IsKeyDown(Input.Right)) {
                setTimer = true;
            }

            if (setTimer) {
                chargeTimer -= timer;
            }

            if (chargeTimer <= 0 && setTimer) {
                chargeTimer = maxReleaseTime;
                bCharged = false;
                setTimer = false;
            }
            


            rotation = MathHelper.Clamp(rotation, rotMin, rotMax);
            incrementedRot = MathHelper.Clamp(incrementedRot, incMin, incMax);
            Direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));

            oldKState = newKState;

            return Direction;
        }

        public int GetScale() {
            return (int)scale;
        }

        public float GetSpeed() {
            if (bCharged && setTimer) {
                return incSpeed + Velocity.X + Velocity.Y;
            }
            return 0;
            
        }

        float Lerp(float start, float stop, float amt) { // https://stackoverflow.com/questions/4353525/floating-point-linear-interpolation
            return start + amt * (stop - start);
        }
    }
}
