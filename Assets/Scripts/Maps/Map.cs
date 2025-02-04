using Fsi.NodeMap;
using ProjectYahtzee.Maps.Nodes;

namespace ProjectYahtzee.Maps
{
    public class Map : Map<NodeType, Node>
    {
        public Map(MapProperties<NodeType> properties, uint seed) : base(properties, seed)
        {
        }
    }
}
