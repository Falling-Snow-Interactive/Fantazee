using System.Collections.Generic;
using ProjectYahtzee.Maps.Nodes;
using UnityEngine;

namespace ProjectYahtzee.Maps
{
    public class Map : MonoBehaviour
    {
        [SerializeField]
        private List<Node> nodes = new();
        public List<Node> Nodes => nodes;
    }
}
