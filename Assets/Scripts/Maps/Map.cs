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

        private void OnValidate()
        {
            Node[] children = GetComponentsInChildren<Node>();
            nodes = new List<Node>(children);
            
            foreach (Node node in nodes)
            {
                if (node && node.Next != null)
                {
                    for (int i = 0; i < node.Next.Count; i++)
                    {
                        if (!node.Next[i])
                        {
                            node.Next.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }
        }
    }
}
