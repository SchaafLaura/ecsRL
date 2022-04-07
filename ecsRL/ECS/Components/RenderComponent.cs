using SadConsole;

namespace ecsRL
{
    public class RenderComponent : Component
    {
        public ColoredGlyph glyph;

        public RenderComponent(ColoredGlyph glyph)
        {
            this.glyph = glyph;
        }

        // renderComponent might not need it, since every entity has one, so we can just access it that way
        // TODO: delete this.
        public override ComponentID ComponentID
        {
            get
            {
                return ComponentID.RENDER_COMPONENT;
            }
            set
            {

            }
        }
    }
}
