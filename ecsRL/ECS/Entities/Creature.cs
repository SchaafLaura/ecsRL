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
            while(randomLocation.X < 10 ||
                randomLocation.Y < 10 ||
                randomLocation.X >= Program.MAP_WIDTH - 10 ||
                randomLocation.Y >= Program.MAP_HEIGHT - 10||
                !Program.map.tiles[randomLocation.X, randomLocation.Y].isPassable)
            {
                randomLocation = new Coord(position.X + rng.Next(0, 20), position.Y + rng.Next(0, 20));
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
                produceActions();
        }

        public override Action getAction()
        {
            if(actions.Count == 0)
                produceActions();
            return actions.Pop();
        }
        /*
        public override Action getAction()
        {
            // death by health being too low
            if(health <= 0)
                return new DeathAction(ID);

            // death by suffocation
            if(
                position.X - 1 >= 0 && 
                position.Y - 1 >= 0 && 
                position.X + 1 < Program.MAP_WIDTH && 
                position.Y + 1 < Program.MAP_HEIGHT &&
                !Program.map.tiles[position.X - 1, position.Y].isPassable &&
                !Program.map.tiles[position.X + 1, position.Y].isPassable &&
                !Program.map.tiles[position.X, position.Y - 1].isPassable &&
                !Program.map.tiles[position.X, position.Y - 1].isPassable)
                return new DeathAction(this.ID);

            // if actor didn't die: move in a random direction
            MovementAction ret = new MovementAction(this.ID);

            Random rng = new Random();
            int number = rng.Next(0, 100);

            if(number <= 25) ret.direction = MovementAction.N;
            else if(number <= 50) ret.direction = MovementAction.E;
            else if(number <= 75) ret.direction = MovementAction.S;
            else ret.direction = MovementAction.W;

            // if out of bounds, choose another direction
            if(position.X + ret.direction.X < 0 ||
                position.Y + ret.direction.Y < 0 ||
                position.X + ret.direction.X >= Program.MAP_WIDTH ||
                position.Y + ret.direction.Y >= Program.MAP_HEIGHT)
                return getAction();

            return ret;
        }
        */

        public override void die()
        {
            Program.map.actors.Remove(this);
            Program.ecs.deleteActor(ID);
        }
    }
}
