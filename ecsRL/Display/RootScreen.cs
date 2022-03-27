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
        private uint _currentActor = 0;

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


        private void gameLoop()
        {
            /*
            var action = Program.ecs.getEntity(_currentActor).getAction();
            if(action == null) return;

            while(true)
            {
                var result = action.perform();
                if(!result.succeeded) return;
                if(result.alternate == null) break;
                action = result.alternate;
            }
            
            _currentActor = (_currentActor + 1) % actors.length;
            */
        }

        public override void Update(TimeSpan delta)
        {
            gameLoop();

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
            MovementAction movementAction = new MovementAction(Program.player.ID);

            if(keyboard.IsKeyDown(Keys.Up)) movementAction.direction = MovementAction.N;
            else if(keyboard.IsKeyDown(Keys.Down)) movementAction.direction = MovementAction.S;
            else if(keyboard.IsKeyDown(Keys.Right)) movementAction.direction = MovementAction.E;
            else if(keyboard.IsKeyDown(Keys.Left)) movementAction.direction = MovementAction.W;

            Program.player.nextAction = movementAction;
            return base.ProcessKeyboard(keyboard);
        }

    }
}
