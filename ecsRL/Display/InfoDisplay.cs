using System;
using System.Linq;
using SadConsole;
using SadRogue.Primitives;

namespace ecsRL
{
    public class InfoDisplay : ScreenObject
    {
        public ScreenSurface surface;       // surface for the border
        public ScreenSurface infoSurface;   // surface for displaying the debug info
        public Point infoLocation = new Point(-1, -1);  // if infoLocation is (-1,-1), no information gets displayed

        public InfoDisplay(int width, int height, Point position)
        {
            surface = new ScreenSurface(width, height);
            surface.Position = position;

            infoSurface = new ScreenSurface(width - 3, height - 3);
            infoSurface.Position = position + new Point(2, 2);

            drawBorder();

            Children.Add(surface);
            Children.Add(infoSurface);
        }

        private void display()
        {
            infoSurface.Surface.Clear();
            
            Point gameCoords = Program.rootScreen._mapDisplay.screenCoordsToGameCoords(infoLocation);

            // get relevant data from the tile, that the mouse is over
            var entities = Program.map.actors.GetItems(gameCoords.X, gameCoords.Y);
            var items = Program.map.items.GetItems(gameCoords.X, gameCoords.Y);

            // display information about the tile, that the mouse is over
            if(infoLocation.X != -1)
            {
                infoSurface.Surface.Print(0, 0, new ColoredString("Coordinates: ") + new ColoredString(gameCoords.ToString()));
                infoSurface.Surface.Print(0, 2, new ColoredString("Tile: ") + new ColoredString(Program.map.tiles[gameCoords.X, gameCoords.Y].glyph));
                infoSurface.Surface.Print(0, 4, new ColoredString("Entity Count: ") + new ColoredString(entities.Count().ToString()));
                if(entities.Count() != 0)
                {
                    Actor actor = entities.First();
                    infoSurface.Surface.Print(0, 6, "name: " + actor.name, Color.White);
                    infoSurface.Surface.Print(0, 8, "health: " + actor.health.ToString(), Color.Red);
                    infoSurface.Surface.Print(0, 10, "description: " + actor.description, Color.White);
                }

                infoSurface.Surface.Print(0, 12, "--------------------------");

                infoSurface.Surface.Print(0, 14, "ItemCount: " + items.Count().ToString());
                if(items.Count() != 0)
                {
                    foreach(Item item in items)
                    {
                        infoSurface.Surface.Print(0, 16, "name: " + item.name, Color.White);
                        infoSurface.Surface.Print(0, 18, "description: " + item.description, Color.White);

                    }
                }

            }
        }

        public void drawBorder()
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

            surface.Surface.Print(1, 0, "Info");
        }

        public override void Update(TimeSpan delta)
        {
            display();
            base.Update(delta);
        }

    }
}
