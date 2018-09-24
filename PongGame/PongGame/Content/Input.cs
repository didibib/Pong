using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PongGame.Content
{
    public struct Input
    {
        public enum SpritePosition { Up = 90, Down = -90, Left = 0, Right = 180}
        public Keys Up;
        public Keys Down;
        public Keys Left;
        public Keys Right;
        public SpritePosition Position;
    }
}
