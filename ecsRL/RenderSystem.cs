using SadConsole;
using SadRogue.Primitives;
namespace ecsRL
{
    public class RenderSystem : System<RenderComponent>
    {
        public RenderSystem()
        {

        }
        public override void update()
        {
            // for now just update all the components
            // later only stuff on screen should draw
            // all drawing should be pooled and then the screen should redraw

            for(uint i = 0; i < components.Count; i++)
            {
                RenderComponent component =  components[i];

                ColoredGlyph G = component.glyph;
                Point position = Program.ecs.getEntity(component.attachedToID).position;

                Program.rootScreen.drawGlyph(position.X, position.Y, G);
            }

        }
    }
}
