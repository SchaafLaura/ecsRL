namespace ecsRL
{
    public abstract class Action
    {
        public Action(uint performedByID)
        {
            this.performedByID = performedByID;
        }
        public uint performedByID;
        public abstract ActionResult perform();
    }
}
