using System;
using SadConsole;
using SadRogue.Primitives;

namespace ecsRL
{
    public class LogDisplay : ScreenObject
    {
        Log log;
        ScreenSurface surface;

        public LogDisplay(Log log, int width, int height, Point position)
        {
            this.log = log;
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

            surface.Surface.Print(1, 0, "Log");

            Children.Add(surface);
        }

        public void display()
        {
            Rectangle rectangle = new Rectangle(5, 1, surface.Surface.Width - 7, surface.Surface.Height - 2);
            surface.Surface.Fill(rectangle, Color.Black, Color.Black, ' ');

            for(int i = log.numberOfItems - 1; i >= 0 ; i--)
            {
                ColoredString logMessage = log.get(i);
                logMessage.SetBackground(surface.Surface.GetBackground(0,0));
                int x = 3;
                int y = (log.numberOfItems - 1 - i) * 2 + 3;

                if(!(x >= surface.Surface.Width || y >= surface.Surface.Height))  
                surface.Surface.Print(
                    x, 
                    y, 
                    new ColoredString(
                        ((char) 7).ToString(),
                        surface.Surface[x, y].Foreground,
                        Color.Transparent));

                surface.Surface.Print(x + 2, y, logMessage);
            }
        }

        public override void Update(TimeSpan delta)
        {
            display();
            base.Update(delta);
        }
    }
}
