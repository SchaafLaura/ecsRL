using SadRogue.Primitives;
using GoRogue;
using SadConsole;
using SadConsole.Input;

namespace ecsRL
{
    public class AttackAction : Action
    {
        public Point direction;
        private bool directionIsSet = false;

        public int damage;
        private bool damageIsSet = false;

        public AttackAction(uint performedByID) : base(performedByID) { }

        public AttackAction(uint performedByID, Point direction) : base(performedByID)
        {
            this.direction = direction;
            directionIsSet = true;
        }

        public override bool tryTakeInput(Keys key)
        {
            if(Equals(key, Keys.Left))
                Direction = MovementAction.W;
            else if(Equals(key, Keys.Right))
                Direction = MovementAction.E;
            else if(Equals(key, Keys.Up))
                Direction = MovementAction.N;
            else if(Equals(key, Keys.Down))
                Direction = MovementAction.S;
            else if((int)key >= 96 && (int) key <= 105)
                Damage = ((int)key - 96) * 10;
            else
                return false;

            return true;
        }

        public override Action clone()
        {
            AttackAction clone = new AttackAction(this.performedByID);

            if(directionIsSet)
                clone.Direction = this.Direction;

            return clone;
        }

        public override bool isPerformable()
        {
            return directionIsSet && damageIsSet;
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

        public int Damage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
                damageIsSet = true;
            }
        }

        public override int Cost
        {
            get
            {
                return 50;
            }
        }

        public override ActionResult perform()
        {
            Actor actor = Program.ecs.getActor(performedByID);
            Coord attackPosition = new Coord((actor.position + direction).X, (actor.position + direction).Y);
            Actor other = Program.map.actors.GetItem(attackPosition);

            if(other == null)
            {
                return ActionResult.failure;
            }
            else
            {
                other.takeDamage(Damage);
                Program.log.log(new ColoredString("You successfully attacked " + other.name + " causing " + damage + " damage!"));
                return ActionResult.success;
            }
        }
    }
}
