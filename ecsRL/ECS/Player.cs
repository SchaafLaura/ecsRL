using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
