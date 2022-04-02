using SadConsole;
using SadRogue.Primitives;
namespace ecsRL
{
    public class RenderSystem : System<RenderComponent>
    {
        public override void update()
        {

            foreach(Component component in components.Values)
            {
                ColoredGlyph G = ((RenderComponent)component).glyph;
                Point position = Program.ecs.getActor(component.attachedToID).position;

                Program.rootScreen.drawGlyphOnMap(position.X, position.Y, G);
            }
        }
    }
}
