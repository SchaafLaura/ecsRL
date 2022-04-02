using System;
using System.Collections.Generic;

namespace ecsRL
{
    public class ECS
    {
        SystemBase[] systems;

        Dictionary<uint, Actor> actors;
        static Stack<uint> freeIDs;
        static uint runningID = 0;
        //public uint NumberOfActors{ get{ return (uint) actors.Count;} }
        public uint NumberOfActors { get { return runningID; } }
        public IEnumerable<Actor> Actors { get { return actors.Values; } }

        public ECS()
        {
            freeIDs = new Stack<uint>();
            freeIDs.Push(runningID++);

            actors = new Dictionary<uint, Actor>();

            initSystems();
        }

        private void initSystems()
        {
            systems = new SystemBase[Enum.GetValues(typeof(ComponentID)).Length];

            systems[(int) ComponentID.RENDER_COMPONENT] = new RenderSystem();
            systems[(int) ComponentID.AI_COMPONENT] = new AISystem();
        }

        public void deleteActor(uint id)
        {
            foreach(SystemBase system in systems)
                system.remove(id);

            actors.Remove(id);

            freeIDs.Push(id);
        }

        public Actor getActor(uint id)
        {
            if(actors.ContainsKey(id))
                return actors[id]; 
            return null;
        }

        // adds components to an entity, that is already in the ecs
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

        public void addActor(Actor E, params Component[] components)
        {
            uint id;
            if(freeIDs.Count != 0)
            {
                id = freeIDs.Pop();
            }
            else
            {
                id = runningID;
                freeIDs.Push(++runningID);
                runningID++;
            }

            E.ID = id;
            actors[id] = E;

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
