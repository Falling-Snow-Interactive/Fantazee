using System.Collections.Generic;
using UnityEngine;

namespace ProjectYahtzee.Maps.Nodes
{
    public class Node : MonoBehaviour
    {
        [SerializeField]
        private List<Node> connections;
        public List<Node> Connections { get => connections; set => connections = value; }

        [SerializeField]
        private NodeType nodeType;
        public NodeType NodeType 
        {
            get => nodeType; 
            set => nodeType = value; 
        }
    }
}