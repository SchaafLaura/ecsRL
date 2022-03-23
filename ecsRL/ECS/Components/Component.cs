namespace ecsRL
{
    public enum ComponentID
    {
        AI_COMPONENT = 0,
        RENDER_COMPONENT = 1,
    }
    public abstract class Component
    {
        public uint attachedToID; // ID of the entity this is attached to
        public static int id;
        public abstract int componentID();

    }
}
 