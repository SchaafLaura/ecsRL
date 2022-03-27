using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecsRL
{
    public class Player : Actor
    {
        public Action nextAction;
        public override Action getAction()
        {
            return nextAction;
        }
    }
}
