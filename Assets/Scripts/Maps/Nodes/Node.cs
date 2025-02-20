using System;
using System.Collections.Generic;
using Fantazhee.Maps.Nodes.Information;
using Fantazhee.Maps.Settings;
using Shapes;
using UnityEngine;

namespace Fantazhee.Maps.Nodes
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

        private void OnValidate()
        {
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

                for (int i = 0; i < connections.Count; i++)
                {
                    if (connectionLines.Count > i)
                    {
                        connectionLines[i].SetLine(connections[i].transform.position - transform.position);
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
            foreach (Node connection in connections)
            {
                Gizmos.DrawLine(transform.position, connection.transform.position);
            }
        }
    }
}