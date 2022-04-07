using System;
using System.Collections.Generic;

namespace ecsRL
{
    public class ECS
    {
        Dictionary<uint, Actor> actors;

        static uint runningID = 0;
        public uint NumberOfActors { get { return runningID; } }
        public IEnumerable<Actor> Actors { get { return actors.Values; } }

        public ECS()
        {
            actors = new Dictionary<uint, Actor>();
        }

        public void deleteActor(uint id)
        {
            actors.Remove(id);
        }

        public Actor getActor(uint id)
        {
            if(actors.ContainsKey(id))
                return actors[id]; 
            return null;
        }

        public void addComponentsToEntity(Entity E, params Component[] components)
        {
            foreach(Component component in components)
                E.components.Add((int) component.ComponentID, component);
        }

        public void addActor(Actor E, params Component[] components)
        {
            E.ID = runningID++;
            actors[E.ID] = E;
            addComponentsToEntity(E, components);
        }
    }
}
