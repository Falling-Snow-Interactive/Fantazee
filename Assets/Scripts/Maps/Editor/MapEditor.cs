
using Fantazee.Maps.Nodes;
using UnityEditor;

namespace Fantazee.Maps
{
    [CustomEditor(typeof(Map))]
    public class MapEditor : Editor
    {
        private void OnSceneGUI()
        {
            if (target is Map map)
            {
                foreach (Node node in map.Nodes)
                {
                    node.Point.DrawPointHandles(serializedObject);
                }
            }
        }
    }
}