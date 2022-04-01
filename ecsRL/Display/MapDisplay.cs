using System;
using SadConsole;
using SadRogue.Primitives;

namespace ecsRL
{
    public class MapDisplay : ScreenObject
    {
        public Map map;
        public ScreenSurface surface;
        Point mapViewPosition;
        public int viewWidth;
        public int viewHeight;

        public MapDisplay(int mapWidth, int mapHeight, int viewWidth, int viewHeight, Point mapViewPosition, Point screenPosition)
        {
            map = new Map(mapWidth, mapHeight);
            this.mapViewPosition = mapViewPosition;
            this.viewWidth = viewWidth;
            this.viewHeight = viewHeight;
            surface = new ScreenSurface(viewWidth, viewHeight);
            surface.Position = screenPosition;
            init();
            
        }
        public MapDisplay(Map map, int viewWidth, int viewHeight, Point screenPosition)
        {
            this.map = map;
            this.viewWidth = viewWidth;
            this.viewHeight = viewHeight;
            surface = new ScreenSurface(viewWidth, viewHeight);
            surface.Position = screenPosition;
            init();
        }

        public Point gameCoordsToScreenCoords(Point gameCoords)
        {
            return new Point(gameCoords.X - (mapViewPosition.X - viewWidth/2) , gameCoords.Y - (mapViewPosition.Y - viewHeight / 2));
        }

        public Point screenCoordsToGameCoords(Point screenCoords)
        {
            return new Point(screenCoords.X + (mapViewPosition.X - viewWidth/2) , screenCoords.Y + (mapViewPosition.Y - viewHeight / 2));
        }

        public void centerOnEntity(Entity entity)
        {
            mapViewPosition = new Point(
                entity.position.X,
                entity.position.Y);
        }

        private void init()
        {
            Color[] colors = new[] { Color.Turquoise, Color.HotPink };
            float[] colorStops = new[] { 0f, 1f };

            Algorithms.GradientFill(surface.FontSize,
                                    surface.Surface.Area.Center,
                                    surface.Surface.Height / 3,
                                    0,
                                    surface.Surface.Area,
                                    new Gradient(colors, colorStops),
                                    (x, y, color) => surface.Surface[x, y].Foreground = color);

            surface.Surface.DrawBox(
                surface.Surface.Area,
                ShapeParameters.CreateStyledBox(
                    ICellSurface.ConnectedLineThin,
                    new ColoredGlyph(),
                    true,
                    true,
                    true));

            surface.Surface.Print(1, 0, "Map");

            Children.Add(surface);
        }

        public void display()
        {
            displayTiles();
        }

        public void drawGlyph(int x, int y, ColoredGlyph glyph)
        {
            Point screenPos = gameCoordsToScreenCoords(new Point(x, y));
            if(screenPos.X < 0 || screenPos.Y < 0 || screenPos.X > viewWidth - 3 || screenPos.Y > viewHeight - 3)
                return;
            surface.Surface.SetCellAppearance(screenPos.X + 1, screenPos.Y + 1, glyph);
        }

        private void displayTiles()
        {
            for(int i = 0; i < viewWidth - 2; i++)
            {
                for(int j = 0; j < viewHeight - 2; j++)
                {
                    Point gamePos = screenCoordsToGameCoords(new Point(i, j));
                    Tile tile = map.tiles[gamePos.X, gamePos.Y];
                    surface.Surface.SetCellAppearance(i + 1, j + 1, tile.glyph);
                }
            }
        }

        public override void Update(TimeSpan delta)
        {
            display();
            base.Update(delta);
        }
    }
}
