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
        public enum SpritePosition { Up = 90, Down = -90, Left = 0, Right = 180 } // Op basis van de positie draaien we de speler en alle objecten die bij de speler horen de juiste kant op
        // Het enige probleem met deze keys is dat we Up en Down expliciet voor het bewgen van de Players gebruiken en Left en Right voor de Peddle (de pijl)
        // Bij de horizontale input moet daarop gelet worden
        public Keys Up;
        public Keys Down;
        public Keys Left;
        public Keys Right;
        public SpritePosition Position;
    }
}
