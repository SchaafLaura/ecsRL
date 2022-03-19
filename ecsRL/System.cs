using System;
using System.Collections.Generic;

namespace ecsRL
{
    public abstract class System
    {
        public ComponentType type;
        protected List<Component> components = new List<Component>();
        public abstract void updateComponents();

        public void add(Component c)
        {
            components.Add(c);
        }
    }
}
