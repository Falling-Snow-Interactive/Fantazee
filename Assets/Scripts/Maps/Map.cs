using System;
using System.Collections.Generic;
using Fantazee.Maps.Nodes;
using UnityEngine;

namespace Fantazee.Maps
{
    public class Map : MonoBehaviour
    {
        [SerializeField]
        private List<Node> nodes = new();
        public List<Node> Nodes => nodes;

        public bool TryGetNode(string name, out Node node)
        {
            foreach (Node n in nodes)
            {
                if (n.name == name)
                {
                    node = n;
                    return true;
                }
            }

            node = null;
            return false;
        }

        private void OnValidate()
        {
            foreach (Node node in nodes)
            {
                for (int i = 0; i < node.Next.Count; i++)
                {
                    if (node.Next[i] == null || string.IsNullOrWhiteSpace(node.Next[i]))
                    {
                        node.Next.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }
}
