using System;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;

namespace ecsRL
{
    public class MapDisplay : ScreenObject
    {
        private ScreenSurface surface;

        public MapDisplay()
        {
            surface = new ScreenSurface(Program.SCREEN_WIDTH, Program.SCREEN_HEIGHT);
            surface.Surface.Fill(Color.Cyan, Color.Cyan);
            Children.Add(surface);
        }

        public void drawGlyph(int x, int y, ColoredGlyph glyph)
        {
            surface.Surface.SetGlyph(x, y, glyph);
        }

        public override void Update(TimeSpan delta)
        {
            
            Program.ecs.updateSystems();
            base.Update(delta);
        }
    }
}
