using SadConsole;
using SadRogue.Primitives;
using System;
using SadConsole.UI;
using SadConsole.UI.Controls;

namespace ecsRL
{
    public class RootScreen : ScreenObject
    {
        // subconsoles
        public LogDisplay _logDisplay;
        public MapDisplay _mapDisplay;
        public InfoDisplay _infoDisplay;
        public InputHandler _inputHandler;
        public AnimationDisplay _animationDisplay;

        // actor used, when the gameLoop runs
        private static uint _currentActor = 0;
        private static Actor currentActor = Program.ecs.getActor(_currentActor);

        public RootScreen(MapDisplay mapDisplay, LogDisplay logDisplay, InfoDisplay infoDisplay)
        {
            this._inputHandler = new InputHandler();

            this._mapDisplay = mapDisplay;
            this._logDisplay = logDisplay;
            this._infoDisplay = infoDisplay;
            this._animationDisplay = new AnimationDisplay();

            /*
            ControlsConsole testwindow = new ControlsConsole(50, 50);
            testwindow.Position = new Point(10, 10);
            testwindow.Print(1, 1, "hello, this is a test");

            SelectionButton btn1 = new SelectionButton(10, 2);
            btn1.Text = "test";
            btn1.Position = new Point(10, 10);
            btn1.Click += gameLoop;
            testwindow.Controls.Add(btn1);
            */
            
            Children.Add(_logDisplay);
            Children.Add(_mapDisplay);
            Children.Add(_infoDisplay);
            Children.Add(_animationDisplay);
            
            // Children.Add(testwindow);
        }

        private void gameLoop()
        {
            if(currentActor.hasEnoughEnergy())
            {
                var action = currentActor.getAction();

                if(action == null) return; // happens when player has not chosen an action yet

                while(true)
                {
                    var result = action.perform();
                    if(!result.succeeded) return; // player chose invalid action
                    if(result.alternative == null) break;
                    action = result.alternative;
                }
            }
            else
            {
                currentActor.gainEnergy();
            }

            // move to the next actor, skipping over ones, that might have been deleted in the ECS
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
            // update infoDisplay information
            Point mouseLocation = getMouseLocation();
            if(mouseIsOverMap(mouseLocation))
                _infoDisplay.infoLocation = new Point(mouseLocation.X, mouseLocation.Y);
            else
                _infoDisplay.infoLocation = new Point(-1, -1);

            // run the gameloop until it is the players turn
            gameLoop();
            while(_currentActor != 0)
                gameLoop();

            // input handling
            if(Game.Instance.Keyboard.HasKeysPressed)
                _inputHandler.handleInput(Game.Instance.Keyboard);

            _mapDisplay.centerOnEntity(Program.player);

            // display all the children
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
