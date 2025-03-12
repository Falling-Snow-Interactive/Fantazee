using System.Collections.Generic;
using Fantazee.Maps.Nodes.Information;
using Fantazee.Maps.Settings;
using Shapes;
using UnityEngine;
using UnityEngine.Splines;

namespace Fantazee.Maps.Nodes
{
    public class Node : MonoBehaviour
    {
        [SerializeField]
        private NodeType type;
        public NodeType Type 
        {
            get => type; 
            set => type = value; 
        }
        
        [SerializeField]
        private List<Node> next;
        public List<Node> Next => next;
        
        [SerializeField]
        private SplineContainer splineContainer;
        
        [SerializeField] 
        private Disc disc;
        
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        public void OnValidate()
        {
            if (MapSettings.Settings.NodeInformation.TryGetInformation(Type, out NodeInformation info))
            {
                disc.Color = info.Color;
                spriteRenderer.sprite = info.Sprite;
            }
        }
        
        public override string ToString()
        {
            return $"{name} - {transform.position}";
        }
    }
}