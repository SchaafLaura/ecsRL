﻿using System;

namespace ecsRL
{
    public class Creature : Actor
    {
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

        public override void die()
        {
            Program.map.actors.Remove(this);
            Program.ecs.deleteActor(ID);
        }
    }
}
