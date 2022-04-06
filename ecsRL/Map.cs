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

        public Tile() {}

        public Tile(ColoredGlyph glyph, bool isPassable)
        {
            this.glyph = glyph;
            this.isPassable = isPassable;
        }
    }

    [Serializable()]
    public class Map
    {
        public SpatialMap<Actor> actors;
        public MultiSpatialMap<Entity> items;
        public Tile[,] tiles;
        public int width;
        public int height;

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            actors = new SpatialMap<Actor>();
            items = new MultiSpatialMap<Entity>();
            tiles = new Tile[width, height];
            init();
        }

        public void moveActorToPoint(Actor actor, Point point)
        {
            actors.Move(actor, new Coord(point.X, point.Y));
        }

        public void init()
        {
            Random rng = new Random();
            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    int rand = rng.Next(0, 100);
                    Tile tile;

                    if(rand < 80)
                        tile = groundTile;
                    else if(rand < 95)
                        tile = grassTile;
                    else if(rand < 99)
                        tile = shrubTile;
                    else
                        tile = waterTile;

                    tiles[i, j] = tile;
                }
            }
            
            for(int i = 1; i < width; i++)
            {
                for(int j = 1; j < height; j++)
                {
                    if(tiles[i,j] == waterTile)
                    {
                        tiles[i - 1, j] = waterTile;
                        tiles[i - 1, j - 1] = waterTile;
                        tiles[i, j - 1] = waterTile;
                    }
                }
            }

            for(int i = 1; i < width; i++)
            {
                for(int j = 1; j < height; j++)
                {
                    if(tiles[i, j] == waterTile)
                    {
                        tiles[i - 1, j] = waterTile;
                        tiles[i - 1, j - 1] = waterTile;
                        tiles[i, j - 1] = waterTile;
                    }
                }
            }

            for(int i = 1; i < width; i++)
            {
                for(int j = 1; j < height; j++)
                {
                    if(tiles[i, j] == waterTile)
                    {
                        tiles[i - 1, j] = waterTile;
                        tiles[i - 1, j - 1] = waterTile;
                        tiles[i, j - 1] = waterTile;
                    }
                }
            }

            for(int i = 1; i < width - 1; i++)
            {
                for(int j = 1; j < height - 1; j++)
                {
                    int watercount = 0;
                    for(int x = i - 1; x <= i + 1; x++)
                    {
                        for(int y = j - 1; y <= j + 1; y++)
                        {
                            watercount += tiles[x, y] == waterTile ? 1 : 0;
                        }
                    }

                    if(tiles[i, j] == waterTile && watercount < 5)
                        tiles[i, j] = groundTile;
                    else if(tiles[i, j] != waterTile && watercount >= 4)
                        tiles[i, j] = waterTile;
                }
            }
        }

        Tile debugTile = new Tile(
            new ColoredGlyph(
                Color.Violet,
                Color.Violet,
                '%'),
            true);

        Tile groundTile = new Tile(
            new ColoredGlyph(
                Color.Transparent,
                Color.Transparent,
                ' '),
            true);

        Tile grassTile = new Tile(
            new ColoredGlyph(
                Color.Green,
                Color.Transparent,
                '.'),
            true);

        Tile shrubTile = new Tile(
            new ColoredGlyph(
                Color.Green,
                Color.Transparent,
                'v'),
            true);

        Tile waterTile = new Tile(
            new ColoredGlyph(
                Color.DarkBlue,
                Color.Transparent,
                '~'),
            false);
    }
}
