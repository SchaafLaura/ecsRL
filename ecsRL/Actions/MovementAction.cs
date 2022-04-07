using SadRogue.Primitives;
using GoRogue;
using SadConsole.Input;

namespace ecsRL
{
    public class MovementAction : Action
    {
        // TODO: move these out of movementAction - loads of actions are gonna need these
        public static Point N = new Point(0, -1);
        public static Point S = new Point(0, 1);
        public static Point W = new Point(-1, 0);
        public static Point E = new Point(1, 0);
        public static Point O = new Point(0, 0);

        public Point direction;
        private bool directionIsSet = false;

        public MovementAction(uint performedById, Point direction) : base(performedById)
        {
            this.direction = direction;
            directionIsSet = true;
        }

        public MovementAction(uint performedByID) : base(performedByID) { }

        public override Action clone()
        {
            MovementAction clone = new MovementAction(this.performedByID);

            if(this.directionIsSet)
                clone.Direction = this.Direction;

            return clone;
        }

        public override bool tryTakeInput(Keys key)
        {
            return false; // movement Action gets created via only one input - no further ones will ever be required
        }

        public override bool isPerformable()
        {
            return directionIsSet;
        }

        public Point Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
                directionIsSet = true;
            }
        }

        public override int Cost
        {
            get
            {
                return 100;
            }
        }

        public override ActionResult perform()
        {
            Actor actor = Program.ecs.getActor(performedByID);
            Point point = actor.position + direction;

            // stay still
            if(direction.X == 0 && direction.Y == 0)
            {
                actor.currentEnergy -= Cost;
                return ActionResult.success;
            }

            // tile is passable and there is no actor -> just move
            if(Program.map.tiles[point.X, point.Y].isPassable && Program.map.actors.GetItem(new Coord(point.X, point.Y)) == null)
            {
                Program.map.moveActorToPoint(actor, point);
                actor.position = point;
                actor.currentEnergy -= Cost;
                return ActionResult.success;
            }
            // tile contains an actor -> hugAction as alternative
            else if(Program.map.actors.GetItem(new Coord(point.X, point.Y)) != null)
            {
                return new ActionResult(
                    new HugAction(performedByID)
                    {
                        Direction = direction
                    });
            }
            else
            {
                return ActionResult.failure;
            }
        }
    }
}