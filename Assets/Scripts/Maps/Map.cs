using System.Collections.Generic;
using System.Linq;
using Fantazee.Maps.Nodes;
using UnityEngine;

namespace Fantazee.Maps
{
    public class Map : MonoBehaviour
    {
        [SerializeField]
        private List<Node> nodes = new();
        public List<Node> Nodes => nodes;

        private void OnValidate()
        {
            Node[] array = gameObject.GetComponentsInChildren<Node>();
            List<Node> list = array.ToList();
            nodes = list;

            if (nodes.Count > 0)
            {
                nodes[0].NodeType = NodeType.Start;
            }

            if (nodes.Count > 1)
            {
                nodes[^1].NodeType = NodeType.Boss;
            }
        }
    }
}
