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
