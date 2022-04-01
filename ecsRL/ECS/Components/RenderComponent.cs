using SadConsole;

namespace ecsRL
{
    public class RenderComponent : Component
    {
        public static new int id = (int)ComponentID.RENDER_COMPONENT;
        public ColoredGlyph glyph;

        public RenderComponent(ColoredGlyph glyph)
        {
            this.glyph = glyph;
        }

        public override int componentID()
        {
            return id;
        }

    }
}
