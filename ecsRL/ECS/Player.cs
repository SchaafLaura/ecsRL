using System.Collections.Generic;

namespace ecsRL
{
    public class Player : Actor
    {
        public Stack<Action> actions = new Stack<Action>();
        public override Action getAction()
        {
            if(health <= 0)
                return new DeathAction(ID);
            return actions.Count == 0 ? null : actions.Pop();
        }

        public override void die()
        {
            Program.log.log(new SadConsole.ColoredString("you ded"));
        }
    }
}
