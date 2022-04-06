using SadRogue.Primitives;
using GoRogue;
using SadConsole;
using SadConsole.Input;

namespace ecsRL
{
    public class HugAction : Action
    {
        public static CellSurface[] hugAnimation;

        public Point direction;
        private bool directionIsSet = false;

        public HugAction(uint performedByID) : base(performedByID) { }

        public HugAction(uint performedByID, Point direction) : base(performedByID)
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
            else
                return false;

            return true;
        }

        public override Action clone()
        {
            HugAction clone = new HugAction(this.performedByID);

            if(directionIsSet)
                clone.Direction = this.Direction;

            return clone;
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
                return 50;
            }
        }

        public override ActionResult perform()
        {
            Actor actor = Program.ecs.getActor(performedByID);
            Coord hugPosition = new Coord((actor.position + direction).X, (actor.position + direction).Y);
            Actor other = Program.map.actors.GetItem(hugPosition);

            if(other == null)
            {
                return ActionResult.failure;
            }
            else
            {
                if(other.ID == 0)
                {
                    Program.log.log(new ColoredString(actor.name + " hugged you!"));
                }

                if(performedByID == 0)
                {
                    Program.log.log(new ColoredString("You hugged " + other.name));
                }

                Program.rootScreen._animationDisplay.tryAddAnimationAtGamePosition(hugAnimation, actor.position);

                return ActionResult.success;
            }
        }

        static HugAction()
        {
            hugAnimation = new CellSurface[4];

            hugAnimation[0] = new CellSurface(1, 1);
            hugAnimation[1] = new CellSurface(1, 1);
            hugAnimation[2] = new CellSurface(1, 1);
            hugAnimation[3] = new CellSurface(1, 1);

            CellSurfaceEditor.SetGlyph(hugAnimation[0], 0, 0, new ColoredGlyph(new Color(Color.Red, 1.0f), Color.Transparent, 3));
            CellSurfaceEditor.SetGlyph(hugAnimation[1], 0, 0, new ColoredGlyph(new Color(Color.Red, 0.8f), Color.Transparent, 3));
            CellSurfaceEditor.SetGlyph(hugAnimation[2], 0, 0, new ColoredGlyph(new Color(Color.Red, 0.5f), Color.Transparent, 3));
            CellSurfaceEditor.SetGlyph(hugAnimation[3], 0, 0, new ColoredGlyph(new Color(Color.Red, 0.3f), Color.Transparent, 3));
        }
    }
}