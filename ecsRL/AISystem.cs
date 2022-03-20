namespace ecsRL
{
    public class AISystem : System<AIComponent>
    {
        public AISystem()
        {
        }
        public override void update()
        {
            // for now just update all the components
            for(uint i = 0; i < components.Count; i++)
            {
                // do something with the component
            }
        }
    }
}
