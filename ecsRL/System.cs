using System.Collections.Generic;

namespace ecsRL
{
    public abstract class System
    {
        public ComponentType type;
        protected Dictionary<int, Component> components = new Dictionary<int, Component> ();
        public abstract void updateComponents();

        public void add(Component c)
        {
            components.Add(c.attachedToID, c);
        }
        public void remove(int id)
        {
            components.Remove(id);
        }
    }
}
