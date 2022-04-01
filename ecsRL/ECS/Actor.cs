﻿namespace ecsRL
{
    public abstract class Actor : Entity
    {
        public int speed = 100;
        public int currentEnergy = 100;

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
