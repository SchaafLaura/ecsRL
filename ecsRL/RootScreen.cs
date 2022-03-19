using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using System;

namespace ecsRL
{
    public class RootScreen : ScreenObject
    {
        private ScreenSurface mapDisplay;
        private ScreenSurface logDisplay;
        private ScreenSurface playerInfoDisplay;

        public static int mapDisplayWidth = Program.SCREEN_WIDTH - 40 - 1;
        public static int mapDisplayHeight = Program.SCREEN_HEIGHT - 2;

        public static int logDisplayWidth = 40 - 2;
        public static int logDisplayHeight = Program.SCREEN_HEIGHT / 2 - 1;

        public static int playerInfoDisplayWidth = 40 - 2;
        public static int playerInfoDisplayHeight = Program.SCREEN_HEIGHT / 2 - 2;

        public RootScreen()
        {
            mapDisplay = new ScreenSurface(mapDisplayWidth, mapDisplayHeight);
            logDisplay = new ScreenSurface(logDisplayWidth, logDisplayHeight);
            playerInfoDisplay = new ScreenSurface(playerInfoDisplayWidth, playerInfoDisplayHeight);

            mapDisplay.Position = new Point(1, 1);
            logDisplay.Position = new Point(mapDisplayWidth + 2, 1);
            playerInfoDisplay.Position = new Point(mapDisplayWidth + 2, logDisplayHeight + 2);

            mapDisplay.Surface.Fill(Color.Transparent, Color.Gray);
            logDisplay.Surface.Fill(Color.Transparent, Color.Tomato);
            playerInfoDisplay.Surface.Fill(Color.Transparent, Color.Brown);

            Children.Add(mapDisplay);
            Children.Add(logDisplay);
            Children.Add(playerInfoDisplay);

        }

        public void drawGlyph(int x, int y, ColoredGlyph glyph)
        {
            mapDisplay.Surface.SetGlyph(x, y, glyph);
        }

        public override void Update(TimeSpan delta)
        {

            Program.ecs.updateSystems();
            base.Update(delta);
        }

        public override bool ProcessKeyboard(Keyboard keyboard)
        {
            return base.ProcessKeyboard(keyboard);
        }

    }
}
