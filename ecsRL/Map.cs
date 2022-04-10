using System;
using GoRogue;
using GoRogue.MapViews;
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
    public class Map : IMapView <bool>
    {
        public SpatialMap<Actor> actors;        // NPCs and player
        public MultiSpatialMap<Item> items;     // pickups/consumables/etc
        public Tile[,] tiles;                   // the actual map tile information
        public int width;
        public int height;

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            actors = new SpatialMap<Actor>();
            items = new MultiSpatialMap<Item>();
            tiles = new Tile[width, height];
            init();
        }

        bool IMapView<bool>.this[int index1D]
        {
            get
            {
                int x = index1D % width;
                int y = index1D / width;
                return tiles[x, y].isPassable;
            }
        }

        bool IMapView<bool>.this[Coord pos]
        {
            get
            {
                return tiles[pos.X, pos.Y].isPassable;
            }
        }

        bool IMapView<bool>.this[int x, int y]
        {
            get
            {
                return tiles[x, y].isPassable;
            }
        }

        public void moveActorToPoint(Actor actor, Point point)
        {
            actors.Move(actor, new Coord(point.X, point.Y));
        }

        // randomly fills the tiles array with predefined tiles
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
        }

        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        

        // some predefined tiles
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
                244),
            true);

        Tile waterTile = new Tile(
            new ColoredGlyph(
                Color.DarkBlue,
                Color.Transparent,
                '~'),
            false);
    }
}
