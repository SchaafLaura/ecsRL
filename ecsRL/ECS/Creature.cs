﻿using System;
using SadRogue.Primitives;

namespace ecsRL
{
    public class Creature : Actor
    {
        public override Action getAction()
        {
            if(health <= 0)
                return new DeathAction(ID);

            MovementAction ret = new MovementAction(this.ID);
            Random rng = new Random();

            int number = rng.Next(0, 100);

            if(number <= 25) ret.Direction = MovementAction.N;
            else if(number <= 50) ret.Direction = MovementAction.E;
            else if(number <= 75) ret.Direction = MovementAction.S;
            else ret.Direction = MovementAction.W;

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
