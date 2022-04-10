using System;
using System.Collections.Generic;
using GoRogue;
using GoRogue.Pathing;
using SadRogue.Primitives;

namespace ecsRL
{
    public class Creature : Actor
    {
        public Stack<Action> actions = new Stack<Action>();
        public Coord goalLocation = new Coord(-1, -1);

        public void produceActions()
        {
            //get current location
            Coord currentLocation = new Coord(position.X, position.Y);

            //get random location in some range
            Random rng = new Random();
            Coord randomLocation = new Coord(position.X + rng.Next(-20, 20), position.Y + rng.Next(-20, 20));
            int k = 0;
            while(randomLocation.X < 10 ||
                randomLocation.Y < 10 ||
                randomLocation.X >= Program.MAP_WIDTH - 10 ||
                randomLocation.Y >= Program.MAP_HEIGHT - 10 ||
                !Program.map.tiles[randomLocation.X, randomLocation.Y].isPassable ||
                (randomLocation.X == position.X && randomLocation.Y == position.Y) &&
                k < 10) 
            {
                randomLocation = new Coord(position.X + rng.Next(0, 20), position.Y + rng.Next(0, 20));
                k++;
            }
            if(k == 10)
            {
                actions.Push(new MovementAction(ID, new Point(0, 0)));
                return;
            }
            goalLocation = randomLocation;

            //run fastAStar to find path
            Path path = Program.fastAStar.ShortestPath(currentLocation, randomLocation, true);

            //convert path to actions and push actions to actionStack
            foreach(Coord coord in path.Steps)
            {
                Coord step = coord - currentLocation;

                actions.Push(
                    new MovementAction(
                        ID, 
                        new Point(step.X, step.Y)));
                
                currentLocation = coord;
            }

            if(actions.Count == 0)
                actions.Push(new MovementAction(ID, new Point(0, 0)));
        }

        public override Action getAction()
        {
            if(actions.Count == 0)
                produceActions();
            return actions.Pop();
        }

        public override void die()
        {
            Program.map.actors.Remove(this);
            Program.ecs.deleteActor(ID);
        }
    }
}
