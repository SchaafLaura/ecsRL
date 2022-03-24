using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using System;

namespace ecsRL
{
    public class RootScreen : ScreenObject
    {
        public LogDisplay _logDisplay;
        public MapDisplay _mapDisplay;

        //private ScreenSurface mapDisplay;
        private ScreenSurface playerInfoDisplay;

        public static int mapDisplayWidth = Program.SCREEN_WIDTH - 40 - 1;
        public static int mapDisplayHeight = Program.SCREEN_HEIGHT - 2;

        public static int logDisplayWidth = 40 - 2;
        public static int logDisplayHeight = Program.SCREEN_HEIGHT / 2 - 1;

        public static int playerInfoDisplayWidth = 40 - 2;
        public static int playerInfoDisplayHeight = Program.SCREEN_HEIGHT / 2 - 1;

        public RootScreen(MapDisplay mapDisplay, LogDisplay logDisplay, ScreenSurface playerInfoDisplay)
        {
            this._mapDisplay = mapDisplay;
            this._logDisplay = logDisplay;
            this.playerInfoDisplay = playerInfoDisplay;

            Children.Add(playerInfoDisplay);
            Children.Add(_logDisplay);
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
