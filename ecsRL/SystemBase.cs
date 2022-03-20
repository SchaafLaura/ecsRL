namespace ecsRL
{
    public abstract class SystemBase
    {
        public abstract void update();
        public abstract void add(Component c);
        public abstract void remove(uint id);
    }
}
