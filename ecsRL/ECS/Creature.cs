using System;

namespace ecsRL
{
    public class Creature : Actor
    {
        public override Action getAction()
        {
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
    }
}
