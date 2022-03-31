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

        public InputHandler _inputHandler;

        private static uint _currentActor = 0;
        private static Actor currentActor = Program.ecs.getActor(_currentActor);

        public RootScreen(MapDisplay mapDisplay, LogDisplay logDisplay, InfoDisplay infoDisplay)
        {
            this._mapDisplay = mapDisplay;
            this._logDisplay = logDisplay;
            this._infoDisplay = infoDisplay;
            Children.Add(_logDisplay);
            Children.Add(_mapDisplay);
            Children.Add(_infoDisplay);

            _inputHandler = new InputHandler();
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
            Point mouseLocation = getMouseLocation();
            if(mouseIsOverMap(mouseLocation))
                _infoDisplay.infoLocation = new Point(mouseLocation.X - 2, mouseLocation.Y - 2);
            else
                _infoDisplay.infoLocation = new Point(-1, -1);

            gameLoop();
            while(_currentActor != 0)
                gameLoop();

            if(Game.Instance.Keyboard.HasKeysPressed)
                _inputHandler.handleInput(Game.Instance.Keyboard);


            _mapDisplay.centerOnEntity(Program.player);
            base.Update(delta);
            Program.ecs.updateSystems();
        }

        public Point getMouseLocation()
        {
            return Game.Instance.Mouse.ScreenPosition.PixelLocationToSurface(12, 12);
        }

        public bool mouseIsOverMap(Point mouseLocation)
        {
            return mouseLocation.X > _mapDisplay.Position.X &&
                mouseLocation.X < _mapDisplay.Position.X + _mapDisplay.viewWidth &&
                mouseLocation.Y > _mapDisplay.Position.Y &&
                mouseLocation.Y < _mapDisplay.Position.Y + _mapDisplay.viewHeight;
        }

        public override bool ProcessKeyboard(Keyboard keyboard)
        {
            if(keyboard.HasKeysPressed && Program.player.actions.Count == 0)
            {
                if(keyboard.IsKeyPressed(Keys.H))
                {
                    if(keyboard.IsKeyPressed(Keys.Up))
                    {
                        Program.player.actions.Push(new HugAction(Program.player.ID, MovementAction.N));
                        return base.ProcessKeyboard(keyboard);
                    }
                    else if(keyboard.IsKeyPressed(Keys.Down))
                    {
                        Program.player.actions.Push(new HugAction(Program.player.ID, MovementAction.S));
                        return base.ProcessKeyboard(keyboard);
                    }
                    else if(keyboard.IsKeyPressed(Keys.Left))
                    {
                        Program.player.actions.Push(new HugAction(Program.player.ID, MovementAction.W));
                        return base.ProcessKeyboard(keyboard);
                    }
                    else if(keyboard.IsKeyPressed(Keys.Right))
                    {
                        Program.player.actions.Push(new HugAction(Program.player.ID, MovementAction.E));
                        return base.ProcessKeyboard(keyboard);
                    }
                    else
                    {
                        return base.ProcessKeyboard(keyboard);
                    }
                }
                else
                {
                    MovementAction movementAction = new MovementAction(Program.player.ID);
                    Point direction = new Point(999, 999);

                    if(keyboard.IsKeyPressed(Keys.Up))
                        direction = MovementAction.N;
                    else if(keyboard.IsKeyPressed(Keys.Down))
                        direction = MovementAction.S;
                    else if(keyboard.IsKeyPressed(Keys.Right))
                        direction = MovementAction.E;
                    else if(keyboard.IsKeyPressed(Keys.Left))
                        direction = MovementAction.W;

                    if(direction.X != 999 && direction.Y != 999)
                    {
                        movementAction.direction = direction;
                        Program.player.actions.Push(movementAction);
                        return base.ProcessKeyboard(keyboard);
                    }
                    else
                    {
                        return base.ProcessKeyboard(keyboard);
                    }

                }
            }

            string str = "";
            foreach(var key in keyboard.KeysPressed)
            {
                str += key.Key.ToString();
            }
            Program.log.log(new ColoredString(str));

            return base.ProcessKeyboard(keyboard);
        }

    }
}
