using System;
using System.Collections.Generic;
using System.Text;

namespace Quget_Engine_One.CollisionMap
{
    class Map
    {
        //private List<Tile> tiles = new List<Tile>();
        private Tile[] tiles;
        private int width;
        private int height;
        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            tiles = new Tile[width * height];
        }
        public void SetTile(int x, int y, Tile tile)
        {
            tiles[width * x + y] = tile;
        }
        public void UpdateTile(int x,int y,Tile.Movement movement)
        {
            tiles[width * x + y].movement = movement;
        }
        public Tile GetTile(int x, int y)
        {
            return tiles[width * x + y];
        }
        public void AddTile(float width, float height,int x, int y,Tile.Movement movement)
        {
            Tile tile = new Tile(width, height,movement);
            SetTile(x, y, tile);
        }
    }
}
