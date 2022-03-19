using System;
using System.Collections.Generic;

namespace ecsRL
{
    public class ECS
    {
        Dictionary<int, Entity> entities;
        System[] systems;

        static Stack<int> freeIDs;
        static int runningID = 0;

        public ECS()
        {
            freeIDs = new Stack<int>();
            freeIDs.Push(runningID++);

            entities = new Dictionary<int, Entity>();

            initSystems();
        }
        private void initSystems()
        {
            int numberOfSystems = Enum.GetValues(typeof(ComponentType)).Length;
            systems = new System[numberOfSystems];

            systems[(int) ComponentType.RenderComponent]    = new RenderSystem();
            systems[(int) ComponentType.AIComponent]        = new AISystem();
        }
        public void deleteEntity(int id)
        {
            entities.Remove(id); // maybe this is not needed? just push the id to the freeID stack

            // this is needed however, because systems will try to update all components 
            // regardless of weather or not the entity exists
            foreach(System system in systems)
            {
                system.remove(id);
            }
            freeIDs.Push(id);
        }
        public Entity getEntity(int id)
        {
            return entities[id];
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
            int id;
            if(freeIDs.Count != 0)
            {
                id = freeIDs.Pop();
            }
            else
            {
                id = runningID++;
                freeIDs.Push(runningID);
            }

            E.id = id;
            entities[id] = E;

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
