using SadConsole;
using SadRogue.Primitives;
namespace ecsRL
{
    public class RenderSystem : System<RenderComponent>
    {
        public override void update()
        {
            for(uint i = 0; i < components.Count; i++)
            {
                RenderComponent component =  components[i];

                ColoredGlyph G = component.glyph;
                Point position = Program.ecs.getActor(component.attachedToID).position;

                Program.rootScreen.drawGlyphOnMap(position.X, position.Y, G);
            }
        }
    }
}
