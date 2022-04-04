using System;
using SadConsole;
using SadRogue.Primitives;

namespace ecsRL
{
    public class LogDisplay : ScreenObject
    {
        Log log;
        ScreenSurface surface;
        ScreenSurface logSurface;
        public LogDisplay(Log log, int width, int height, Point position)
        {
            this.log = log;
            surface = new ScreenSurface(width, height);
            surface.Position = position;

            logSurface = new ScreenSurface(width - 3, height - 3);
            logSurface.Position = position + new Point(2, 2);

            drawBorder();

            Children.Add(surface);
            Children.Add(logSurface);
        }

        public void display()
        {
            logSurface.Surface.Clear();

            for(int i = log.numberOfItems - 1; i >= 0 ; i--)
            {
                ColoredString logMessage = log.get(i);
                int x = 0;
                int y = (log.numberOfItems - 1 - i) * 2;

                if(!(x >= surface.Surface.Width || y >= surface.Surface.Height))  
                    logSurface.Surface.Print(
                        x, 
                        y, 
                        new ColoredString(
                            ((char) 7).ToString(),
                            surface.Surface[x, y].Foreground,
                            Color.Transparent));

                logSurface.Surface.Print(x + 2, y, logMessage);
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

            surface.Surface.Print(1, 0, "Log");
        }

        public override void Update(TimeSpan delta)
        {
            display();
            base.Update(delta);
        }
    }
}
