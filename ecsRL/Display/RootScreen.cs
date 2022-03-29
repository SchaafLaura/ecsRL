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
        private static uint _currentActor = 1;
        private static Actor currentActor = Program.ecs.getActor(_currentActor);

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
            if(currentActor.hasEnoughEnergy())
            {
                var action = currentActor.getAction();

                if(action == null) return;

                while(true)
                {
                    var result = action.perform();
                    if(!result.succeeded) return;
                    if(result.alternative == null) break;
                    action = result.alternative;
                }
            }
            else
            {
                currentActor.gainEnergy();
            }

            _currentActor = (_currentActor + 1) % Program.ecs.NumberOfActors;
            currentActor = Program.ecs.getActor(_currentActor);

            while(currentActor == null)
            {
                _currentActor = (_currentActor + 1) % Program.ecs.NumberOfActors;
                currentActor = Program.ecs.getActor(_currentActor);
            }
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

            gameLoop();
            while(_currentActor != 0)
                gameLoop();

            if(Game.Instance.Keyboard.HasKeysDown)
                ProcessKeyboard(Game.Instance.Keyboard);

            _mapDisplay.centerOnEntity(Program.player);
            base.Update(delta);
            Program.ecs.updateSystems();
        }

        public override bool ProcessKeyboard(Keyboard keyboard)
        {
            if(keyboard.HasKeysPressed && Program.player.actions.Count == 0)
            {
                MovementAction movementAction = new MovementAction(Program.player.ID);

                if(keyboard.IsKeyPressed(Keys.Up)) movementAction.direction = MovementAction.N;
                else if(keyboard.IsKeyPressed(Keys.Down)) movementAction.direction = MovementAction.S;
                else if(keyboard.IsKeyPressed(Keys.Right)) movementAction.direction = MovementAction.E;
                else if(keyboard.IsKeyPressed(Keys.Left)) movementAction.direction = MovementAction.W;

                Program.player.actions.Push(movementAction);
            }
            return base.ProcessKeyboard(keyboard);
        }

    }
}
