using System.Collections.Generic;
using Fantazhee.Maps.Nodes;
using UnityEngine;

namespace Fantazhee.Maps
{
    public class Map : MonoBehaviour
    {
        [SerializeField]
        private List<Node> nodes = new();
        public List<Node> Nodes => nodes;

        private void OnValidate()
        {
            nodes[0].NodeType = NodeType.Start;
            nodes[^1].NodeType = NodeType.Boss;
        }
    }
}
