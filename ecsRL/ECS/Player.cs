using System.Collections.Generic;

namespace ecsRL
{
    public class Player : Actor
    {
        public Stack<Action> actions = new Stack<Action>();
        public override Action getAction()
        {
            return actions.Count == 0 ? null : actions.Pop();
        }
    }
}
