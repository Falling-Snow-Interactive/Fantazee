using UnityEditor;

namespace Fantazee.Maps.Nodes
{
    [CustomEditor(typeof(Node))]
    public class NodeEditor : Editor
    {
        private void OnSceneGUI()
        {
            if (target is Node node)
            {
                if (node.Point.DrawTangentHandles(serializedObject))
                {
                    node.Refresh();
                    foreach (Node next in node.Next)
                    {
                        next.Refresh();
                    }

                    foreach (Node previous in node.Previous)
                    {
                        previous.Refresh();
                    }
                    
                    serializedObject.ApplyModifiedProperties();
                }
            }
        }
    }
}
