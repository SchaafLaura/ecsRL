using System;
namespace ecsRL
{
    public class AIComponent : Component
    {
        public static new int id = (int)ComponentID.AI_COMPONENT;
        public override int componentID()
        {
            return id;
        }
    }
}
