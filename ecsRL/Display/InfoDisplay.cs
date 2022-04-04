using System;
using System.Linq;
using SadConsole;
using SadRogue.Primitives;

namespace ecsRL
{
    public class InfoDisplay : ScreenObject
    {
        public ScreenSurface surface;
        public ScreenSurface infoSurface;
        public Point infoLocation = new Point(-1, -1);

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
            /*
            Rectangle rectangle = new Rectangle(1, 1, surface.Surface.Width - 2, surface.Surface.Height - 2);
            surface.Surface.Fill(rectangle, Color.Black, Color.Black, ' ');
            */
            
            Point gameCoords = Program.rootScreen._mapDisplay.screenCoordsToGameCoords(infoLocation);
            var entities = Program.map.actors.GetItems(gameCoords.X, gameCoords.Y);

            if(infoLocation.X != -1)
            {
                infoSurface.Surface.Print(0, 0, new ColoredString("Coordinates: ") + new ColoredString(gameCoords.ToString()));
                infoSurface.Surface.Print(0, 2, new ColoredString("Tile: ") + new ColoredString(Program.map.tiles[gameCoords.X, gameCoords.Y].glyph));

                infoSurface.Surface.Print(0, 4, new ColoredString("Entity Count: ") + new ColoredString(entities.Count().ToString()));
                if(entities.Count() != 0)
                {
                    infoSurface.Surface.Print(0, 6, entities.First().name, Color.White);
                    infoSurface.Surface.Print(0, 8, entities.First().health.ToString(), Color.Red);
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
