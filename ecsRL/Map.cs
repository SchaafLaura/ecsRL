using System;
using GoRogue;
using SadConsole;
using SadRogue.Primitives;

namespace ecsRL
{
    public class Tile
    {
        public bool isPassable;
        public ColoredGlyph glyph = new ColoredGlyph(Color.Transparent, Color.Transparent);
    }

    public class Map
    {
        public SpatialMap<Entity> entities;
        public MultiSpatialMap<Entity> items;
        public Tile[,] tiles;
        public int width;
        public int height;

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            entities = new SpatialMap<Entity>();
            items = new MultiSpatialMap<Entity>();
            tiles = new Tile[width, height];
            init();
        }

        public void init()
        {
            Random rng = new Random();
            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    Tile tile = new Tile();
                    if(rng.Next(0, 100) > 95)
                    {
                        tile.isPassable = false;
                        tile.glyph = new ColoredGlyph(Color.DarkOliveGreen, Color.Transparent, '#');
                    }
                    
                    tiles[i, j] = tile;
                }
            }

            Tile T = new Tile();
            T.glyph = new ColoredGlyph(Color.DarkOrchid, Color.Transparent, '*');

            for(int i = 450; i < 500; i++)
            {
                for(int j = 500; j < 530; j++)
                {
                    tiles[i, j] = T;
                }
            }

        }
    }
}
