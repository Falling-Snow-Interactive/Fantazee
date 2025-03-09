using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fantazee.Maps.Nodes
{
    // [CustomPropertyDrawer(typeof(Node), true)]
    public class NodePropertyDrawer : PropertyDrawer
    {
        private Node node;
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty name = property.FindPropertyRelative("name");
            SerializedProperty point = property.FindPropertyRelative("point");
            SerializedProperty type = property.FindPropertyRelative("type");
            SerializedProperty next = property.FindPropertyRelative("next");
            SerializedProperty previous = property.FindPropertyRelative("previous");

            Foldout root = new() { text = name.stringValue };

            PropertyField nameField = new(name);
            PropertyField pointField = new(point);
            PropertyField typeField = new(type);
            PropertyField nextField = new(next);
            PropertyField previousField = new(previous);
            
            root.Add(nameField);
            root.Add(pointField);
            root.Add(typeField);
            root.Add(nextField);
            root.Add(previousField);
            
            return root;
        }
    }
}
