using ProjectYahtzee.Maps.Settings;
using Shapes;
using UnityEngine;

namespace ProjectYahtzee.Maps.Nodes
{
    public class NodeConnection : MonoBehaviour
    {
        public Node From { get; private set; }
        public Node To { get; private set; }
        
        [SerializeField]
        private Polyline line;
        
        [SerializeField]
        private Polyline outline;

        [SerializeField]
        private float outlineMod = 1.1f;

        public void SetLine(Node from, Node to)
        {
            From = from;
            To = to;
            
            Vector3 fromPosition = MapUtility.NodeToWorldPosition(from.position);
            Vector3 toPosition = MapUtility.NodeToWorldPosition(to.position);
            var points = new Vector2[] { fromPosition, toPosition };
            
            line.SetPoints(points);
            line.Thickness = MapSettings.Settings.ConnectionThickness;
            
            outline.SetPoints(points);
            outline.Thickness = MapSettings.Settings.ConnectionThickness * outlineMod;
        }
    }
}