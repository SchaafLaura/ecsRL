using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadRogue.Primitives;

namespace ecsRL
{
    public class MovementAction : Action
    {
        public static Point N = new Point(0, -1);
        public static Point S = new Point(0, 1);
        public static Point W = new Point(-1, 0);
        public static Point E = new Point(1, 0);

        public Point direction;

        public MovementAction(uint performedByID) : base(performedByID)
        {

        }

        public override int Cost { 
            get
            {
                return 100;
            }
        }

        public override ActionResult perform()
        {
            Actor actor = Program.ecs.getActor(performedByID);
            Point point = actor.position + direction;

            if(Program.map.tiles[point.X, point.Y].isPassable)
            {
                // TODO: check if there is another actor and construct alternative attack action
                Program.map.moveActorToPoint(actor, point);
                actor.position = point;
                actor.currentEnergy -= Cost;
                return ActionResult.success;
            }
            else
            {
                return ActionResult.failure;
            }
        }
    }
}
