namespace ecsRL
{
    public abstract class Actor : Entity
    {
        public int health = 100;
        public int speed = 100;
        public int currentEnergy = 100;

        public void gainEnergy()
        {
            currentEnergy += speed;
        }

        public bool hasEnoughEnergy()
        {
            return currentEnergy >= 100  || health <= 0;
        }

        public virtual void takeDamage(int damage)
        {
            this.health -= damage;
        }

        public abstract Action getAction();
        public abstract void die();
    }
}
