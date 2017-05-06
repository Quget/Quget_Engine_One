using System;
using System.Collections.Generic;
using System.Text;

namespace Quget_Engine_One.CollisionMap
{
    class Tile
    {
        public float width { private set; get; }
        public float height { private set; get; }
        public enum Movement
        {
            NORMAL,
            SLOW,
            FAST,
            NO_MOVE
        }
        public Movement movement;
        public Tile(float width, float height, Movement movement)
        {
            this.width = width;
            this.height = height;
            this.movement = movement;
        }
    }
}
