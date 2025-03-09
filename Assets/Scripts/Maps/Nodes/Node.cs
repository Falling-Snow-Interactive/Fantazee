using System;
using System.Collections.Generic;
using Fsi.Spline.Vectors;
using UnityEngine;

namespace Fantazee.Maps.Nodes
{
    [Serializable]
    public class Node
    {
        public string name;
        
        [SerializeField]
        private Vector3Point point;
        public Vector3Point Point => point;
        
        [SerializeField]
        private NodeType type;
        public NodeType Type 
        {
            get => type; 
            set => type = value; 
        }
        
        [SerializeReference]
        private List<int> next;
        public List<int> Next => next;
        
        [SerializeField]
        private List<int> previous;
        public List<int> Previous => previous;

        public override string ToString()
        {
            string s = $"{point.value} - Type: {type}";
            return name;
        }
    }
}