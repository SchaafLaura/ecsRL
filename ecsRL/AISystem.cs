namespace ecsRL
{
    public class AISystem : System
    {
        public AISystem()
        {
            type = ComponentType.AIComponent;
        }
        public override void updateComponents()
        {
            // for now just update all the components
            foreach(Component c in components)
            {
                c.update();
            }
        }
    }
}
