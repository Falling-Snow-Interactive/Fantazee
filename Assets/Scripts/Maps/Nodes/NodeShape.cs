using ProjectYahtzee.Maps.Nodes.Information;
using ProjectYahtzee.Maps.Settings;
using Shapes;
using UnityEngine;

namespace ProjectYahtzee.Maps.Nodes
{
    public class NodeShape : MonoBehaviour
    {
        [SerializeField]
        private Disc disc;

        [SerializeField]
        private Disc outline;
        
        [SerializeField]
        private SpriteRenderer sprite;
        
        public void SetNode(Node node)
        {
            UpdateVisuals(node);
        }

        private void UpdateVisuals(Node node)
        {
            if (MapSettings.Settings.NodeInformation.TryGetInformation(node.type, out NodeInformation info))
            {
                if (disc)
                {
                    disc.Radius = MapSettings.Settings.NodeRadius;
                    
                    {
                        disc.Color = info.Color;
                    }
                }

                if (outline)
                {
                    outline.Radius = MapSettings.Settings.NodeRadius;
                    outline.Thickness = MapSettings.Settings.NodeOutlineThickness;
                }
                    
                if (sprite)
                {
                    sprite.sprite = info.Sprite;
                }
            }
        }
    }
}