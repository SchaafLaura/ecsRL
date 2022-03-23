using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using System;

namespace ecsRL
{
    public class RootScreen : ScreenObject
    {
        private LogDisplay _logDisplay;
        private MapDisplay _mapDisplay;

        //private ScreenSurface mapDisplay;
        private ScreenSurface playerInfoDisplay;

        public static int mapDisplayWidth = Program.SCREEN_WIDTH - 40 - 1;
        public static int mapDisplayHeight = Program.SCREEN_HEIGHT - 2;

        public static int logDisplayWidth = 40 - 2;
        public static int logDisplayHeight = Program.SCREEN_HEIGHT / 2 - 1;

        public static int playerInfoDisplayWidth = 40 - 2;
        public static int playerInfoDisplayHeight = Program.SCREEN_HEIGHT / 2 - 1;

        public RootScreen()
        {
            playerInfoDisplay = new ScreenSurface(playerInfoDisplayWidth, playerInfoDisplayHeight);
            playerInfoDisplay.Position = new Point(mapDisplayWidth + 2, logDisplayHeight + 2);
            playerInfoDisplay.Surface.Fill(Color.Transparent, Color.Brown);
            Children.Add(playerInfoDisplay);


            _logDisplay = new LogDisplay(
                logDisplayWidth, 
                logDisplayHeight, 
                new Point(mapDisplayWidth + 2, 1));
            Children.Add(_logDisplay);

            _mapDisplay = new MapDisplay(
                1000, 
                1000, 
                mapDisplayWidth, 
                mapDisplayHeight, 
                new Point(0, 0), 
                new Point(1, 1));
            _mapDisplay.centerOnEntity(Program.ecs.getEntity(0));
            
            Children.Add(_mapDisplay);


        }

        public void drawGlyphOnMap(int x, int y, ColoredGlyph glyph)
        {
            _mapDisplay.drawGlyph(x, y, glyph);
        }

        public override void Update(TimeSpan delta)
        {
            base.Update(delta);
            Program.ecs.updateSystems();
        }

        public override bool ProcessKeyboard(Keyboard keyboard)
        {
            base.Update(new TimeSpan(0));
            return base.ProcessKeyboard(keyboard);
        }

    }
}
