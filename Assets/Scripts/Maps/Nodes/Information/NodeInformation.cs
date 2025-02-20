using System;
using fsi.settings.Informations;
using UnityEngine;

namespace Fantazhee.Maps.Nodes.Information
{
    [Serializable]
    public class NodeInformation : Information<NodeType>
    {
        [SerializeField]
        private Color color = Color.black;
        public Color Color => color;

        [SerializeField]
        private Color outline = Color.black;
        public Color Outline => outline;
        
        [SerializeField]
        private Sprite sprite;
        public Sprite Sprite => sprite;
    }
}