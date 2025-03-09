using System.Collections.Generic;
using Fsi.Spline;
using Fsi.Spline.Vectors;
using Shapes;
using UnityEngine;

namespace Fantazee.Maps.Nodes
{
    public class ConnectionLine : MonoBehaviour
    {
        [SerializeField]
        private Line line;

        [SerializeField]
        private Line outline;

        [SerializeField]
        private Polyline polyline;

        public void SetLine(Vector3 localEnd)
        {
            if (line)
            {
                line.Start = Vector3.zero;
                line.End = localEnd;
            }

            if (outline)
            {
                outline.Start = Vector3.zero;
                outline.End = localEnd;
            }
        }

        public void SetLine(Node start, Node end)
        {
            Vector3 root = start.Point.value;

            Vector3Spline spline = new(start.Point, end.Point)
                                   {
                                       curveType = CurveType.Bezier
                                   };
            
            List<Vector3Point> points = spline.GetPoints(20);
            List<Vector3> positions = new();
            foreach (Vector3Point point in points)
            {
                positions.Add(point.value - root); // Need to offset it to local positions;
            }

            polyline.SetPoints(positions);
        }
    }
}