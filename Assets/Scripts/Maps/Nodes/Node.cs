using System.Collections.Generic;
using Fantazee.Maps.Nodes.Information;
using Fantazee.Maps.Settings;
using Fsi.Spline.Vectors;
using Shapes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Maps.Nodes
{
    public class Node : MonoBehaviour
    {
        [Header("Connections")]
        
        [FormerlySerializedAs("connections")]
        [SerializeField]
        private List<Node> next;
        public List<Node> Next => next;
        
        [SerializeField]
        private List<Node> previous;
        public List<Node> Previous => previous;

        [SerializeField]
        private NodeType nodeType;
        public NodeType NodeType 
        {
            get => nodeType; 
            set => nodeType = value; 
        }

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
        private List<ConnectionLine> connectionLines;
        
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

            int attempts = 50;
            while (connectionLines.Count < next.Count && attempts > 0)
            {
                attempts--;
                ConnectionLine connectionLine = PrefabUtility.InstantiatePrefab(connectionLinePrefab, connectionsContainer) as ConnectionLine;
                connectionLines.Add(connectionLine);
            }

            attempts = 50;
            while (connectionLines.Count > next.Count && attempts > 0)
            {
                attempts--;
                ConnectionLine connectionLine = connectionLines[^1];
                connectionLines.Remove(connectionLine);
                if (connectionLine)
                {
                    Destroy(connectionLine.gameObject);
                }
            }
            
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

                for (int i = 0; i < next.Count; i++)
                {
                    if (connectionLines.Count > i)
                    {
                        connectionLines[i].SetLine(this, next[i]);
                    }
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
            
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, point.tangentOut);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, point.tangentIn);
        }
    }
}