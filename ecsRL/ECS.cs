using System;
using System.Collections.Generic;

namespace ecsRL
{
    public class ECS
    {
        HoleyList<Entity> entities = new HoleyList<Entity>();
        System[] systems;

        public ECS()
        {
            entities = new HoleyList<Entity>();
            initSystems();
        }

        private void initSystems()
        {
            int numberOfSystems = Enum.GetValues(typeof(ComponentType)).Length;
            systems = new System[numberOfSystems];

            systems[(int) ComponentType.RenderComponent]    = new RenderSystem();
            systems[(int) ComponentType.AIComponent]        = new AISystem();
        }

        public Entity getEntity(int id)
        {
            return entities.get(id);
        }

        public void addComponentsToEntity(Entity E, params Component[] components)
        {
            int id = E.id;
            foreach(Component component in components)
            {
                component.attachedToID = id;
                int type = (int)component.type;
                systems[type].add(component);
            }
        }

        public void addEntity(Entity E, params Component[] components)
        {
            int id = entities.add(E);

            E.id = id;

            foreach(Component component in components)
            {
                component.attachedToID = id;
                int type = (int)component.type;
                systems[type].add(component);
            }
        }

        public void updateSystems()
        {
            foreach(System system in systems)
            {
                system.updateComponents();
            }
        }

    }
}
