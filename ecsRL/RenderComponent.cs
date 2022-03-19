using SadConsole;
using SadRogue.Primitives;

namespace ecsRL
{
    public class RenderComponent : Component
    {
        ColoredGlyph glyph;

        public RenderComponent(ColoredGlyph glyph)
        {
            this.glyph = glyph;
            this.type = ComponentType.RenderComponent;
        }

        public override void update()
        {
            Point position = Program.ecs.getEntity(attachedToID).position;

            Program.rootScreen.drawGlyph(position.X, position.Y, glyph);
        }
    }
}
