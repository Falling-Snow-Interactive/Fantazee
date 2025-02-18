using Shapes;
using UnityEngine;

namespace ProjectYahtzee.Maps.Nodes
{
    public class ConnectionLine : MonoBehaviour
    {
        [SerializeField]
        private Line line;

        [SerializeField]
        private Line outline;
        
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
    }
}