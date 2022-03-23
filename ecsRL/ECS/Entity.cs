using SadRogue.Primitives;
using GoRogue;

namespace ecsRL
{
    public class Entity : IHasID
    {
        public uint ID { get; set; }
        public string name;
        public Point position;
    }
}
