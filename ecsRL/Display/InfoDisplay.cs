using System;
using System.Linq;
using SadConsole;
using SadRogue.Primitives;

namespace ecsRL
{
    public class InfoDisplay : ScreenObject
    {
        public ScreenSurface surface;
        public Point infoLocation = new Point(-1, -1);

        public InfoDisplay(int width, int height, Point position)
        {
            surface = new ScreenSurface(width, height);
            surface.Position = position;

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

            Children.Add(surface);
        }

        private void display()
        {
            Rectangle rectangle = new Rectangle(1, 1, surface.Surface.Width - 2, surface.Surface.Height - 2);
            surface.Surface.Fill(rectangle, Color.Black, Color.Black, ' ');
            
            Point gameCoords = Program.rootScreen._mapDisplay.screenCoordsToGameCoords(infoLocation);
            var entities = Program.map.actors.GetItems(gameCoords.X, gameCoords.Y);

            if(infoLocation.X != -1)
            {
                surface.Surface.Print(1, 3, new ColoredString("Coordinates: ") + new ColoredString(gameCoords.ToString()));
                surface.Surface.Print(1, 5, new ColoredString("Tile: ") + new ColoredString(Program.map.tiles[gameCoords.X, gameCoords.Y].glyph));

                surface.Surface.Print(1, 7, new ColoredString("Entity Count: ") + new ColoredString(entities.Count().ToString()));
                if(entities.Count() != 0)
                {
                    surface.Surface.Print(1, 9, entities.First().name, Color.White);
                    surface.Surface.Print(1, 11, entities.First().health.ToString(), Color.Red);
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
