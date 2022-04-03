namespace ecsRL
{
    public enum ComponentID
    {
        AI_COMPONENT = 0,
        RENDER_COMPONENT = 1,
    }

    public abstract class Component
    {
        public abstract ComponentID ComponentID { get; set; }

    }
}
 