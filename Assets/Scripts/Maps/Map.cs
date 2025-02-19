using System.Collections.Generic;
using Fantahzee.Maps.Nodes;
using UnityEngine;

namespace Fantahzee.Maps
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
