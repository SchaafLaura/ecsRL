using SadConsole.Input;

namespace ecsRL
{
    public abstract class Action
    {
        public Action(uint performedByID)
        {
            this.performedByID = performedByID;
        }

        public uint performedByID;
        public abstract Action clone();
        public abstract bool tryTakeInput(Keys key);
        public abstract ActionResult perform();
        public abstract bool isPerformable();
        public abstract int Cost { get;}
    }
}
