﻿using SadConsole;
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
        }

        public Point getMouseLocation()
        {
            return Game.Instance.Mouse.ScreenPosition.PixelLocationToSurface(12, 12);
        }

        public bool mouseIsOverMap(Point mouseLocation)
        {
            return mouseLocation.X > _mapDisplay.Position.X && mouseLocation.X < _mapDisplay.Position.X + _mapDisplay.viewWidth &&
                mouseLocation.Y > _mapDisplay.Position.Y && mouseLocation.Y < _mapDisplay.Position.Y + _mapDisplay.viewHeight;
        }
    }
}
