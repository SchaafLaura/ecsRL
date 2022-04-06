using SadConsole;
using SadRogue.Primitives;
using System;

namespace ecsRL
{
    public class RootScreen : ScreenObject
    {
        public LogDisplay _logDisplay;
        public MapDisplay _mapDisplay;
        public InfoDisplay _infoDisplay;
        public InputHandler _inputHandler;
        public AnimationDisplay _animationDisplay;

        private static uint _currentActor = 0;
        private static Actor currentActor = Program.ecs.getActor(_currentActor);

        public RootScreen(MapDisplay mapDisplay, LogDisplay logDisplay, InfoDisplay infoDisplay)
        {
            this._mapDisplay = mapDisplay;
            this._logDisplay = logDisplay;
            this._infoDisplay = infoDisplay;

            _animationDisplay = new AnimationDisplay();

            /*
            AnimatedScreenSurface test = new AnimatedScreenSurface("love", 1, 1);
            var frame = test.CreateFrame();
            CellSurfaceEditor.SetGlyph(frame, 0, 0, new ColoredGlyph(Color.Red, Color.Transparent, 3));
            test.AnimationDuration = 10;
            test.Repeat = false;
            test.Position = _mapDisplay.gameCoordsToSurfaceCoords(Program.player.position);
            test.Start();
            */

            Children.Add(_logDisplay);
            Children.Add(_mapDisplay);
            Children.Add(_infoDisplay);
            Children.Add(_animationDisplay);
            //Children.Add(test);
            

            _inputHandler = new InputHandler();
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
            Point mouseLocation = getMouseLocation();
            if(mouseIsOverMap(mouseLocation))
                _infoDisplay.infoLocation = new Point(mouseLocation.X, mouseLocation.Y);
            else
                _infoDisplay.infoLocation = new Point(-1, -1);

            gameLoop();
            while(_currentActor != 0)
                gameLoop();

            if(Game.Instance.Keyboard.HasKeysPressed)
                _inputHandler.handleInput(Game.Instance.Keyboard);

            _mapDisplay.centerOnEntity(Program.player);
            base.Update(delta);
        }

        public Point getMouseLocation()
        {

            return Game.Instance.Mouse.ScreenPosition.PixelLocationToSurface(12, 12);
        }

        public bool mouseIsOverMap(Point mouseLocation)
        {
            return mouseLocation.X > _mapDisplay.surface.Position.X && mouseLocation.X < _mapDisplay.surface.Position.X + _mapDisplay.viewWidth - 1 &&
                mouseLocation.Y > _mapDisplay.surface.Position.Y && mouseLocation.Y < _mapDisplay.surface.Position.Y + _mapDisplay.viewHeight - 1;
        }
    }
}
