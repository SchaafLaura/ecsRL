using SadRogue.Primitives;
using GoRogue;
using SadConsole;
using SadConsole.Input;

namespace ecsRL
{
    public class AttackAction : Action
    {
        public static CellSurface[] attackAnimation;

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

        // direction keys specify direction, NUM keys define damage (for now)
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
            else if((int)key >= 96 && (int)key <= 105)
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

            // no one there to attack :(
            if(other == null)
            {
                return ActionResult.failure;
            }
            // someone to hit, yay :)
            else
            {
                other.takeDamage(Damage);
                if(performedByID == 0)
                    Program.log.log(new ColoredString(Program.player.name + " successfully attacked " + other.name + ", causing " + damage + " damage!"));
                else if(other.ID == 0)
                    Program.log.log(new ColoredString(actor.name + " successfully attacked you, causing " + damage + " damage!"));

                Program.rootScreen._animationDisplay.tryAddAnimationAtGamePosition(attackAnimation, actor.position);

                return ActionResult.success;
            }
        }

        // setup static attackAnimation frames
        static AttackAction()
        {
            attackAnimation = new CellSurface[4];

            attackAnimation[0] = new CellSurface(1, 1);
            attackAnimation[1] = new CellSurface(1, 1);
            attackAnimation[2] = new CellSurface(1, 1);
            attackAnimation[3] = new CellSurface(1, 1);

            CellSurfaceEditor.SetGlyph(attackAnimation[0], 0, 0, new ColoredGlyph(new Color(Color.Red, 1.0f), Color.Transparent, '!'));
            CellSurfaceEditor.SetGlyph(attackAnimation[1], 0, 0, new ColoredGlyph(new Color(Color.Red, 0.8f), Color.Transparent, '!'));
            CellSurfaceEditor.SetGlyph(attackAnimation[2], 0, 0, new ColoredGlyph(new Color(Color.Red, 0.5f), Color.Transparent, '!'));
            CellSurfaceEditor.SetGlyph(attackAnimation[3], 0, 0, new ColoredGlyph(new Color(Color.Red, 0.3f), Color.Transparent, '!'));
        }
    }
}