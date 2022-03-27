using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecsRL
{
    public abstract class Actor : Entity
    {
        public int speed = 100;
        public int currentEnergy = 0;
        public void gainEnergy()
        {
            currentEnergy += speed;
        }
        public bool hasEnoughEnergy()
        {
            return currentEnergy >= 100;
        }

        public abstract Action getAction();
    }
}
