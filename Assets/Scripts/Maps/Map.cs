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
    }
}
