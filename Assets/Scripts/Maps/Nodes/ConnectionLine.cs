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

        public void SetLine(Vector3Point start, Vector3Point end)
        {
            Vector3Spline spline = new(start, end)
                                   {
                                       curveType = CurveType.Bezier
                                   };
            
            List<Vector3Point> points = spline.GetPoints(20);
            polyline.points.Clear();
            foreach (Vector3Point point in points)
            {
                polyline.AddPoint(point.value);
            }
        }
    }
}