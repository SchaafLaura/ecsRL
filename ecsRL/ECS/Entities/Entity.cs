using SadRogue.Primitives;
using GoRogue;
using System.Collections.Generic;

namespace ecsRL
{
    public class Entity : IHasID
    {
        public uint ID { get; set; }
        public string name;
        public string description = "";
        public Point position;
        public Dictionary<int, Component> components;
        public RenderComponent renderComponent;

        public Entity()
        {
            components = new Dictionary<int, Component>();
        }
    }
}
