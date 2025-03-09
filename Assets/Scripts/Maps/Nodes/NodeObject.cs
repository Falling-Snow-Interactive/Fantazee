using System;
using Fantazee.Maps.Nodes.Information;
using Fantazee.Maps.Settings;
using Shapes;
using UnityEngine;

namespace Fantazee.Maps.Nodes
{
    public class NodeObject : MonoBehaviour
    {
        public Node Node { get; private set; }
        private Action callback;
        
        [SerializeField] 
        private Disc disc;
        
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public void Initialize(Node node)
        {
            Node = node;

            transform.position = node.Point.value;

            if (MapSettings.Settings.NodeInformation.TryGetInformation(node.Type, out NodeInformation info))
            {
                disc.Color = info.Color;
                spriteRenderer.sprite = info.Sprite;
            }
        }
    }
}