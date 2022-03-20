using System;
using System.Collections.Generic;

namespace ecsRL
{
    public class ECS
    {
        Dictionary<uint, Entity> entities;
        SystemBase[] systems;

        static Stack<uint> freeIDs;
        static uint runningID = 0;

        public ECS()
        {
            freeIDs = new Stack<uint>();
            freeIDs.Push(runningID++);

            entities = new Dictionary<uint, Entity>();

            initSystems();
        }
        private void initSystems()
        {
            systems = new SystemBase[Enum.GetValues(typeof(ComponentID)).Length];

            systems[(int) ComponentID.RENDER_COMPONENT] = new RenderSystem();
            systems[(int) ComponentID.AI_COMPONENT] = new AISystem();
        }
        public void deleteEntity(uint id)
        {
            entities.Remove(id); // maybe this is not needed? just push the id to the freeID stack

            // this is needed however, because systems will try to update all components 
            // regardless of weather or not the entity exists
            foreach(SystemBase system in systems)
            {
                system.remove(id);
            }
            freeIDs.Push(id);
        }
        public Entity getEntity(uint id)
        {
            return entities[id];
        }
        public void addComponentsToEntity(Entity E, params Component[] components)
        {
            uint id = E.ID;
            foreach(Component component in components)
            {
                component.attachedToID = id;
                int type = component.componentID();
                systems[type].add(component);
            }
        }
        public void addEntity(Entity E, params Component[] components)
        {
            uint id;
            if(freeIDs.Count != 0)
            {
                id = freeIDs.Pop();
            }
            else
            {
                id = runningID++;
                freeIDs.Push(runningID);
            }

            E.ID = id;
            entities[id] = E;

            foreach(Component component in components)
            {
                component.attachedToID = id;
                int type = component.componentID();
                systems[type].add(component);
            }
        }
        public void updateSystems()
        {
            foreach(SystemBase system in systems)
            {
                system.update();
            }
        }

    }
}
