using System.Collections.Generic;

namespace ecsRL
{
    public abstract class System<T> : SystemBase where T : Component
    {
        protected Dictionary<uint, T> components = new Dictionary<uint, T> ();

        public override void add(Component component)
        {
            components.Add(component.attachedToID, (T) component);
        }
        public override void remove(uint id)
        {
            components.Remove(id);
        }
    }
}
