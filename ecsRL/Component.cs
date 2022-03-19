public enum ComponentType
{
    RenderComponent = 0,
    AIComponent = 1,
}

namespace ecsRL
{
    public abstract class Component
    {
        public ComponentType type;
        public int attachedToID; // ID of the entity this is attached to
        public abstract void update();
    }
}
 