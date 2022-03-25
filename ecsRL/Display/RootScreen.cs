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
        public InfoDisplay _infoDisplay;

        public RootScreen(MapDisplay mapDisplay, LogDisplay logDisplay, InfoDisplay infoDisplay)
        {
            this._mapDisplay = mapDisplay;
            this._logDisplay = logDisplay;
            this._infoDisplay = infoDisplay;

            Children.Add(_logDisplay);
            Children.Add(_mapDisplay);
            Children.Add(_infoDisplay);
        }

        public void drawGlyphOnMap(int x, int y, ColoredGlyph glyph)
        {
            _mapDisplay.drawGlyph(x, y, glyph);
        }

        public override void Update(TimeSpan delta)
        {
            Point mouseLocation = Game.Instance.Mouse.ScreenPosition.PixelLocationToSurface(12, 12);
            

            if(mouseLocation.X > _mapDisplay.Position.X &&
                mouseLocation.X < _mapDisplay.Position.X + _mapDisplay.viewWidth &&
                mouseLocation.Y > _mapDisplay.Position.Y &&
                mouseLocation.Y < _mapDisplay.Position.Y + _mapDisplay.viewHeight)
            {
                Point point = new Point(mouseLocation.X - 2, mouseLocation.Y - 2);
                _infoDisplay.infoLocation = point;
            }
            else
            {
                _infoDisplay.infoLocation = new Point(-1, -1);

            }

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
