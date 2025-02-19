using Fantahzee.Maps.Nodes;
using Fantahzee.Maps.Settings;
using Shapes;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Fantahzee.Maps
{
    [ExecuteAlways] 
    public class MapShapes : ImmediateModeShapeDrawer
    {
        [SerializeField]
        private Map map;

        [SerializeField]
        private float nodeRadius = 1;

        [SerializeField]
        private float nodeOutline = 0.1f;

        [SerializeField]
        private float lineThickness = 1;
        
        [SerializeField]
        private float outlineThickness = 1;
        
        [SerializeField]
        private Vector2 spacing = Vector2.one;
        
        public override void DrawShapes(Camera cam)
        {
            if (map == null)
            {
                return;
            }
            
            using(Draw.Command(cam, RenderPassEvent.AfterRendering))
            {
                Draw.LineGeometry = LineGeometry.Flat2D;
                Draw.ThicknessSpace = ThicknessSpace.Pixels;
                Draw.Matrix = transform.localToWorldMatrix;
                
                Draw.Radius = nodeRadius;
                Draw.Thickness = nodeOutline;
                // Draw.Color = Color.black;
                foreach (Node node in map.Nodes)
                {
                    Draw.Ring(node.transform.position, Color.black);
                }
                
                foreach (Node node in map.Nodes)
                {
                    foreach (Node connection in node.Connections)
                    {
                        Draw.Thickness = lineThickness + outlineThickness;
                        // Draw.Color = Color.black;
                        Draw.Line(node.transform.position, connection.transform.position, Color.black);
                        
                        // Draw.Thickness = lineThickness;
                        // Draw.Color = Color.white;
                        // Draw.Line(node.transform.position, connection.transform.position);
                    }
                }

                Draw.Radius = nodeRadius;
                Draw.Radius = nodeRadius;
                // Draw.Color = Color.white;
                foreach (Node node in map.Nodes)
                {
                    if (MapSettings.Settings.NodeInformation.TryGetInformation(node.NodeType, out var info))
                    {
                        // Draw.Color = info.Color;
                        Draw.Disc(node.transform.position, info.Color);
                    }
                }
            }

        }

    }
}