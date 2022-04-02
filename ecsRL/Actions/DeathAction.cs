using SadConsole.Input;

namespace ecsRL
{
    public class DeathAction : Action
    {
        public DeathAction(uint performedByID) : base(performedByID)
        {

        }

        public override bool isPerformable()
        {
            return true;
        }

        public override bool tryTakeInput(Keys key)
        {
            return false;
        }

        public override Action clone()
        {
            return new DeathAction(this.performedByID);
        }

        public override ActionResult perform()
        {
            Program.ecs.getActor(performedByID).die();
            return ActionResult.success;
        }

        public override int Cost
        {
            get
            {
                return 1000;
            }
        }
    }
}
