using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static miniRPG.Tiles;

namespace miniRPG
{
    public class Map
    {
        public List<CollisionTiles> CollisionTiles { get; } = new List<CollisionTiles>();
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Map() { }

        public void Generate(int[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if (number > 0)
                    {
                        CollisionTiles.Add(new CollisionTiles(number, new Vector2(x * size, y * size)));
                        Width = (x + 1) * size;
                        Height = (y + 1) * size;
                    }

                }
            {
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CollisionTiles tile in CollisionTiles)
                tile.Draw(spriteBatch);
        }
    }
}
