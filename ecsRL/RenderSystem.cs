namespace ecsRL
{
    public class RenderSystem : System
    {
        public RenderSystem()
        {
            type = ComponentType.RenderComponent;
        }
        public override void updateComponents()
        {
            // for now just update all the components
            // later only stuff on screen should draw
            // all drawing should be pooled and then the screen should redraw
            foreach(Component c in components)
            {
                c.update();
            }
        }
    }
}
