using System.Collections.Generic;
using Fantazee.Maps.Nodes.Information;
using Fantazee.Maps.Settings;
using Fsi.Spline.Vectors;
using Shapes;
using UnityEngine;

namespace Fantazee.Maps.Nodes
{
    public class Node : MonoBehaviour
    {
        [SerializeField]
        private NodeType nodeType;
        public NodeType NodeType 
        {
            get => nodeType; 
            set => nodeType = value; 
        }
        
        [Header("Connections")]
        
        [SerializeField]
        private List<Node> next;
        public List<Node> Next => next;
        
        [SerializeField]
        private List<Node> previous;
        public List<Node> Previous => previous;
        
        [Header("Visuals")]

        [SerializeField]
        private float radius;

        [SerializeField]
        private float thickness;

        [SerializeField]
        private Disc disc;

        [SerializeField]
        private Disc outline;
        
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        [SerializeField]
        private Transform connectionsContainer;
        
        [SerializeField]
        private ConnectionLine connectionLinePrefab;

        [SerializeField]
        private Vector3Point point;
        public Vector3Point Point => point;

        private void OnValidate()
        {
            foreach (Node n in next)
            {
                if (!n.Previous.Contains(this))
                {
                    n.Previous.Add(this);
                }
            }

            foreach (Node p in previous)
            {
                if (!p.Next.Contains(this))
                {
                    p.Next.Add(this);
                }
            }
            Refresh();
        }

        public void Refresh()
        {
            NodeType = nodeType;
            point.value = transform.position;
            
            if (MapSettings.Settings.NodeInformation.TryGetInformation(NodeType, out NodeInformation info))
            {
                if (disc)
                {
                    disc.Color = info.Color;
                    disc.Radius = radius;
                }

                if (outline)
                {
                    outline.Color = info.Outline;
                    outline.Thickness = thickness/2f;
                    outline.Radius = radius + thickness/4f;
                }

                if (spriteRenderer)
                {
                    spriteRenderer.sprite = info.Sprite;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (Node connection in next)
            {
                Gizmos.DrawLine(transform.position, connection.transform.position);
            }
        }
    }
}