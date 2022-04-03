using System;
using System.Collections.Generic;

namespace ecsRL
{
    public class ECS
    {
        Dictionary<uint, Actor> actors;
        static Stack<uint> freeIDs;
        static uint runningID = 0;
        public uint NumberOfActors { get { return runningID; } }
        public IEnumerable<Actor> Actors { get { return actors.Values; } }

        public ECS()
        {
            freeIDs = new Stack<uint>();
            freeIDs.Push(runningID++);

            actors = new Dictionary<uint, Actor>();
        }

        public void deleteActor(uint id)
        {
            actors.Remove(id);
            freeIDs.Push(id);
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

            addComponentsToEntity(E, components);
        }
    }
}
